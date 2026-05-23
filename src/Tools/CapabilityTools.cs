using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

namespace XsltMcpServer.Tools;

[McpServerToolType]
public static class CapabilityTools
{
    [McpServerTool(Name = "server_capabilities"), Description(
        "Report what this MCP server can do: engine type and version, spec coverage stats, and supported feature flags. " +
        "Call this once at the start of a session to know which features (streaming, packages, schema-aware, etc.) you can rely on.")]
    public static string Get(SpecIndex index)
    {
        var engineAsm = typeof(PhoenixmlDb.Xslt.XsltTransformer).Assembly;
        return JsonSerializer.Serialize(new
        {
            ok = true,
            server = "xslt-mcp",
            serverVersion = typeof(CapabilityTools).Assembly.GetName().Version?.ToString(),
            engine = "PhoenixmlDb.Xslt",
            engineVersion = engineAsm.GetName().Version?.ToString(),
            specs = new[] { "XSLT 3.0", "XSLT 4.0 (draft)", "XPath 3.1", "XPath 4.0 (draft)" },
            specEntryCount = index.GetAll().Count,
            features = new
            {
                streaming = true,
                packages = true,
                schemaAware = false,
                xpath40 = true,
                xslt40Draft = true,
                fnTransform = true,
                xslMerge = true,
                xslFork = true,
                xslAccumulator = true
            },
            tools = new[]
            {
                "xslt_transform", "xslt_validate", "xslt_compile", "xslt_apply",
                "xslt_test", "xslt_explain_streamability",
                "xslt_lookup_instruction", "xslt_lookup_function", "xslt_lookup_output_method",
                "xslt_lookup_error_code", "xslt_search", "xslt_list_instructions", "xslt_list_functions",
                "xslt_compare_versions", "xslt_find_examples", "xslt_suggest_fix", "xslt_explain_error",
                "xpath_evaluate", "xml_validate_schema", "xml_format",
                "server_capabilities"
            }
        }, XsltErrorMapper.JsonOpts);
    }
}
