using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

namespace XsltMcpServer.Tools;

[McpServerToolType]
public static class SuggestFixTools
{
    [McpServerTool(Name = "xslt_suggest_fix"), Description(
        "Given an XSLT error code (and optionally the source snippet near the error), " +
        "return a spec-grounded suggestion for fixing it plus a link to the relevant spec section. " +
        "Backed by curated rules for the most common XSLT 3.0/4.0 errors. " +
        "Returns { ok, code, suggestion?, specUrl?, message? }.")]
    public static string Suggest(
        [Description("Error code (e.g., XTSE0010, XTDE0030, XTTE0505)")] string code,
        [Description("Optional source snippet near the error line")] string? snippet = null,
        [Description("Optional 1-based line number where the error occurred")] int? line = null,
        [Description("Optional 1-based column number")] int? column = null)
    {
        if (Rules.TryGetValue(code.ToUpperInvariant(), out var rule))
            return JsonSerializer.Serialize(new
            {
                ok = true,
                code = code.ToUpperInvariant(),
                suggestion = rule.Suggestion,
                specUrl = rule.SpecUrl
            }, XsltErrorMapper.JsonOpts);

        return JsonSerializer.Serialize(new
        {
            ok = false,
            code,
            message = $"No fix rule for '{code}'. Try xslt_explain_error for general guidance, or xslt_search for related spec context."
        }, XsltErrorMapper.JsonOpts);
    }

    private static readonly Dictionary<string, (string Suggestion, string SpecUrl)> Rules = new()
    {
        ["XTSE0010"] = (
            "Element must be in the http://www.w3.org/1999/XSL/Transform namespace. " +
            "Check the xmlns:xsl declaration on the root and the element prefix. " +
            "Common typo: 'xsl:templare' should be 'xsl:template'.",
            "https://www.w3.org/TR/xslt-30/#dt-xslt-namespace"),
        ["XTSE0090"] = (
            "Attribute is not allowed on this XSLT element. Check the spec section for " +
            "the parent element to see which attributes it accepts. Common cause: typo in attribute name.",
            "https://www.w3.org/TR/xslt-30/#syntax"),
        ["XTSE0265"] = (
            "Duplicate xsl:template with same match pattern, mode, and priority. " +
            "Either delete one, change one's mode/priority, or use xsl:next-match to chain them.",
            "https://www.w3.org/TR/xslt-30/#template-rule-overriding"),
        ["XTSE0530"] = (
            "Required attribute missing on an XSLT element. Most common case: xsl:template needs " +
            "either `match=` or `name=`. xsl:variable needs `name=`. Check the spec for the element's required attrs.",
            "https://www.w3.org/TR/xslt-30/#syntax"),
        ["XTDE0030"] = (
            "No template matched the source node. Either add an xsl:template match='<element-name>' " +
            "or rely on the built-in template rules (which by default copy text nodes and recurse). " +
            "If you explicitly want an error here, use <xsl:on-no-match select='fail'/> in xsl:mode.",
            "https://www.w3.org/TR/xslt-30/#built-in-rule-shallow-copy"),
        ["XTDE0040"] = (
            "Internal error result-document conflict — two xsl:result-document instructions wrote to the same URI. " +
            "Ensure each result-document has a unique href.",
            "https://www.w3.org/TR/xslt-30/#creating-result-trees"),
        ["XTTE0505"] = (
            "The result of evaluating a typed body did not match the declared type. " +
            "Check the `as=` attribute on the xsl:template / xsl:function / xsl:variable — the body must " +
            "produce a sequence matching that type. Often masks a serialization bug — inspect the actual output type.",
            "https://www.w3.org/TR/xslt-30/#sequence-types"),
        ["XTSE3430"] = (
            "Template body in a streamable mode is not guaranteed streamable. " +
            "Common causes: descendant axis ('//'), crawling xsl:apply-templates, multi-consumption of the context node, " +
            "or current-group() outside xsl:for-each-group. Run xslt_explain_streamability for per-template diagnosis.",
            "https://www.w3.org/TR/xslt-30/#streamability-analysis"),
        ["XTDE1280"] = (
            "fn:doc() or document() failed to resolve a URI. " +
            "Check the URI is well-formed and the document is reachable, or use xslt-mcp's `documents` JSON arg " +
            "to register the resource in-memory: pass { 'lookup.xml': '<xml/>' } in your xslt_transform call.",
            "https://www.w3.org/TR/xpath-functions-31/#func-doc"),
        ["XTSE0610"] = (
            "Multiple xsl:param or xsl:variable declarations with the same name in the same scope. " +
            "Rename one, or remove the duplicate. Global and local variables can share a name (local shadows global).",
            "https://www.w3.org/TR/xslt-30/#variable-values")
    };
}
