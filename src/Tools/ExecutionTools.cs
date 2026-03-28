using System.ComponentModel;
using ModelContextProtocol.Server;
using PhoenixmlDb.Xslt;

namespace XsltMcpServer.Tools;

[McpServerToolType]
public static class ExecutionTools
{
    [McpServerTool(Name = "xslt_transform"), Description(
        "Execute an XSLT transformation. Provide the stylesheet and source XML. Returns the transformed output.")]
    public static async Task<string> Transform(
        [Description("XSLT stylesheet (complete XML)")] string stylesheet,
        [Description("Source XML document to transform")] string sourceXml)
    {
        try
        {
            var transformer = new XsltTransformer();
            await transformer.LoadStylesheetAsync(stylesheet);
            return await transformer.TransformAsync(sourceXml);
        }
        catch (Exception ex)
        {
            return $"Transform error: {ex.Message}";
        }
    }

    [McpServerTool(Name = "xslt_validate"), Description(
        "Validate an XSLT stylesheet. Compiles without executing. Returns errors with line numbers and spec references.")]
    public static async Task<string> Validate(
        [Description("XSLT stylesheet (complete XML) to validate")] string stylesheet)
    {
        try
        {
            var transformer = new XsltTransformer();
            await transformer.LoadStylesheetAsync(stylesheet);
            return "Valid: stylesheet compiled successfully.";
        }
        catch (Exception ex)
        {
            return $"Validation error: {ex.Message}";
        }
    }

    [McpServerTool(Name = "xslt_explain_error"), Description(
        "Explain an XSLT/XPath error code. Returns the spec definition, common causes, and fix suggestions.")]
    public static string ExplainError(
        SpecIndex index,
        [Description("Error code (e.g., 'XTSE0010', 'FODC0002')")] string errorCode)
    {
        var entry = index.Lookup(errorCode);
        if (entry != null)
            return entry.Body;

        // Try searching for the error code
        var results = index.Search(errorCode);
        if (results.Count > 0)
            return $"No exact match for '{errorCode}', but found related:\n\n{results[0].Body}";

        return $"Error code '{errorCode}' not found in the spec reference. " +
               "Common prefixes: XTSE (static errors), XTDE (dynamic errors), " +
               "XPDY (XPath dynamic), FODC (functions/operators).";
    }

    [McpServerTool(Name = "xpath_evaluate"), Description(
        "Evaluate an XPath expression against XML. Returns the result as text.")]
    public static async Task<string> EvaluateXPath(
        [Description("XPath expression to evaluate")] string xpath,
        [Description("Source XML document")] string xml)
    {
        // Wrap the XPath expression in a minimal XSLT stylesheet.
        // Escape single quotes in the xpath for the attribute value.
        var escapedXPath = xpath.Replace("\"", "&quot;");
        var stylesheet = $"""
            <xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
              <xsl:output method="text"/>
              <xsl:template match="/">
                <xsl:value-of select="{escapedXPath}"/>
              </xsl:template>
            </xsl:stylesheet>
            """;

        try
        {
            var transformer = new XsltTransformer();
            await transformer.LoadStylesheetAsync(stylesheet);
            return await transformer.TransformAsync(xml);
        }
        catch (Exception ex)
        {
            return $"XPath error: {ex.Message}";
        }
    }
}
