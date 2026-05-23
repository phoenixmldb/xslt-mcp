using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;
using PhoenixmlDb.XQuery.Parser;
using PhoenixmlDb.Xslt;
using PhoenixmlDb.Xslt.Engine;
using XsltMcpServer.Models;

namespace XsltMcpServer.Tools;

[McpServerToolType]
public static class ExecutionTools
{
    [McpServerTool(Name = "xslt_transform"), Description(
        "Execute an XSLT transformation. Provide the stylesheet and source XML. Returns a JSON result with 'ok', 'value', 'elapsedMs', or 'errors' with code+location.")]
    public static async Task<string> Transform(
        [Description("XSLT stylesheet (complete XML)")] string stylesheet,
        [Description("Source XML document to transform")] string sourceXml,
        [Description("Optional JSON object mapping parameter name → value, e.g. {\"name\": \"value\", \"count\": 42}. Strings bind as xs:string, numbers as xs:double, booleans as xs:boolean.")] string? parameters = null,
        [Description("Optional JSON object mapping URI → XML for doc()/document() resolution: {\"lookup.xml\": \"<lookup/>\"}. URIs are resolved relative to the stylesheet's base URI; pass absolute or bare names.")] string? documents = null)
    {
        if (!string.IsNullOrEmpty(parameters))
        {
            try { JsonDocument.Parse(parameters).Dispose(); }
            catch (JsonException ex)
            {
                var err = new TransformError("XMCP0002",
                    $"Invalid parameters JSON: {ex.Message}", null, null, null, null);
                return JsonSerializer.Serialize(TransformResult.Failure(new[] { err }), XsltErrorMapper.JsonOpts);
            }
        }

        if (!string.IsNullOrEmpty(documents))
        {
            try { JsonDocument.Parse(documents).Dispose(); }
            catch (JsonException ex)
            {
                var err = new TransformError("XMCP0003",
                    $"Invalid documents JSON: {ex.Message}", null, null, null, null);
                return JsonSerializer.Serialize(TransformResult.Failure(new[] { err }), XsltErrorMapper.JsonOpts);
            }
        }

        var sw = System.Diagnostics.Stopwatch.StartNew();
        try
        {
            var preloaded = XsltErrorMapper.BuildPreloadedResources(documents);
            var transformer = new XsltTransformer { PreloadedResources = preloaded };
            // Use a synthetic base URI so relative doc() references (e.g. "lookup.xml") resolve
            // to http://xslt-mcp.internal/lookup.xml and hit the PreloadedResources cache.
            var baseUri = preloaded.Count > 0 ? XsltErrorMapper.SyntheticBaseUri : null;
            await transformer.LoadStylesheetAsync(stylesheet, baseUri);
            XsltErrorMapper.ApplyParameters(transformer, parameters);
            var output = await transformer.TransformAsync(sourceXml);
            sw.Stop();
            return JsonSerializer.Serialize(
                TransformResult.Success(output, outputMethod: null, sw.ElapsedMilliseconds), XsltErrorMapper.JsonOpts);
        }
        catch (ArgumentException ex) when (ex.Message.StartsWith("XMCP0003"))
        {
            var err = new TransformError("XMCP0003", ex.Message, null, null, null, null);
            return JsonSerializer.Serialize(TransformResult.Failure(new[] { err }), XsltErrorMapper.JsonOpts);
        }
        catch (XsltException ex)
        {
            return JsonSerializer.Serialize(TransformResult.Failure(XsltErrorMapper.ToErrors(ex)), XsltErrorMapper.JsonOpts);
        }
        catch (System.Xml.XmlException ex)
        {
            return JsonSerializer.Serialize(TransformResult.Failure(XsltErrorMapper.ToErrors(ex)), XsltErrorMapper.JsonOpts);
        }
    }

    [McpServerTool(Name = "xslt_validate"), Description(
        "Validate an XSLT stylesheet. Compiles without executing. Returns a JSON result with 'ok' true on success, or 'errors' with code/line/column on failure.")]
    public static async Task<string> Validate(
        [Description("XSLT stylesheet (complete XML) to validate")] string stylesheet)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        try
        {
            var transformer = new XsltTransformer();
            await transformer.LoadStylesheetAsync(stylesheet);
            sw.Stop();
            return JsonSerializer.Serialize(
                TransformResult.Success(value: null, outputMethod: null, sw.ElapsedMilliseconds), XsltErrorMapper.JsonOpts);
        }
        catch (XsltException ex)
        {
            return JsonSerializer.Serialize(TransformResult.Failure(XsltErrorMapper.ToErrors(ex)), XsltErrorMapper.JsonOpts);
        }
        catch (System.Xml.XmlException ex)
        {
            return JsonSerializer.Serialize(TransformResult.Failure(XsltErrorMapper.ToErrors(ex)), XsltErrorMapper.JsonOpts);
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

    [McpServerTool(Name = "xslt_test"), Description(
        "Apply a stylesheet to source XML and assert the output XML-equals expectedXml (canonicalized DeepEquals comparison, ignoring whitespace differences). " +
        "Returns { passed: bool, actual?: string, expected?: string, diff?: string }. " +
        "Use this for red/green TDD on XSLT — faster than running transform and parsing the result manually.")]
    public static async Task<string> Test(
        [Description("XSLT stylesheet (complete XML)")] string stylesheet,
        [Description("Source XML document")] string sourceXml,
        [Description("Expected output XML — compared via XDocument.DeepEquals after parsing")] string expectedXml,
        [Description("Optional JSON object of stylesheet parameters: {\"name\": \"value\"}")] string? parameters = null)
    {
        var transformJson = await Transform(stylesheet, sourceXml, parameters);
        using var doc = JsonDocument.Parse(transformJson);
        if (!doc.RootElement.GetProperty("ok").GetBoolean())
        {
            return JsonSerializer.Serialize(new
            {
                passed = false,
                error = transformJson
            }, XsltErrorMapper.JsonOpts);
        }

        var actual = doc.RootElement.GetProperty("value").GetString() ?? "";

        bool passed;
        string? diff = null;
        try
        {
            passed = XmlEquals(actual, expectedXml);
            if (!passed) diff = SimpleDiff(actual, expectedXml);
        }
        catch (System.Xml.XmlException ex)
        {
            passed = actual.Trim() == expectedXml.Trim();
            diff = passed ? null : $"XML parse error during DeepEquals ({ex.Message}); comparing as strings.\n" + SimpleDiff(actual, expectedXml);
        }

        return JsonSerializer.Serialize(new
        {
            passed,
            actual,
            expected = expectedXml,
            diff
        }, XsltErrorMapper.JsonOpts);
    }

    private static bool XmlEquals(string a, string b)
    {
        var da = System.Xml.Linq.XDocument.Parse(a, System.Xml.Linq.LoadOptions.None);
        var db = System.Xml.Linq.XDocument.Parse(b, System.Xml.Linq.LoadOptions.None);
        return System.Xml.Linq.XNode.DeepEquals(da, db);
    }

    private static string SimpleDiff(string a, string b)
    {
        var la = a.Split('\n');
        var lb = b.Split('\n');
        var sb = new System.Text.StringBuilder();
        var max = Math.Max(la.Length, lb.Length);
        for (var i = 0; i < max; i++)
        {
            var actualLine = i < la.Length ? la[i] : "";
            var expectedLine = i < lb.Length ? lb[i] : "";
            if (actualLine != expectedLine)
            {
                sb.AppendLine($"- {expectedLine}");
                sb.AppendLine($"+ {actualLine}");
            }
        }
        return sb.ToString();
    }

    [McpServerTool(Name = "xpath_evaluate"), Description(
        "Evaluate an XPath expression against XML. Returns a JSON result with 'ok', 'value', 'elapsedMs', or 'errors' with code+location.")]
    public static async Task<string> EvaluateXPath(
        [Description("XPath expression to evaluate")] string xpath,
        [Description("Source XML document")] string xml)
    {
        // Wrap the XPath expression in a minimal XSLT stylesheet.
        var escapedXPath = xpath.Replace("\"", "&quot;");
        var stylesheet = $"""
            <xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
              <xsl:output method="text"/>
              <xsl:template match="/">
                <xsl:value-of select="{escapedXPath}"/>
              </xsl:template>
            </xsl:stylesheet>
            """;

        var sw = System.Diagnostics.Stopwatch.StartNew();
        try
        {
            var transformer = new XsltTransformer();
            await transformer.LoadStylesheetAsync(stylesheet);
            var output = await transformer.TransformAsync(xml);
            sw.Stop();
            return JsonSerializer.Serialize(
                TransformResult.Success(output, outputMethod: null, sw.ElapsedMilliseconds), XsltErrorMapper.JsonOpts);
        }
        catch (XQueryParseException ex)
        {
            return JsonSerializer.Serialize(TransformResult.Failure(XsltErrorMapper.ToErrors(ex)), XsltErrorMapper.JsonOpts);
        }
        catch (XsltException ex)
        {
            return JsonSerializer.Serialize(TransformResult.Failure(XsltErrorMapper.ToErrors(ex)), XsltErrorMapper.JsonOpts);
        }
        catch (System.Xml.XmlException ex)
        {
            return JsonSerializer.Serialize(TransformResult.Failure(XsltErrorMapper.ToErrors(ex)), XsltErrorMapper.JsonOpts);
        }
    }
}
