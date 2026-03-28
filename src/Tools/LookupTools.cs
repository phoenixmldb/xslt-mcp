using System.ComponentModel;
using System.Text;
using ModelContextProtocol.Server;

namespace XsltMcpServer.Tools;

[McpServerToolType]
public static class LookupTools
{
    [McpServerTool(Name = "xslt_lookup_instruction"), Description(
        "Look up an XSLT instruction by name (e.g., 'xsl:for-each-group'). " +
        "Returns complete spec reference with attributes, content model, examples, and error codes.")]
    public static string LookupInstruction(
        SpecIndex index,
        [Description("XSLT instruction name (e.g., 'xsl:for-each-group', 'xsl:template')")] string name)
    {
        var entry = index.Lookup(name);
        if (entry != null && string.Equals(entry.Category, "instruction", StringComparison.OrdinalIgnoreCase))
            return entry.Body;

        // Try with xsl: prefix if not found
        if (!name.StartsWith("xsl:", StringComparison.OrdinalIgnoreCase))
        {
            entry = index.Lookup("xsl:" + name);
            if (entry != null && string.Equals(entry.Category, "instruction", StringComparison.OrdinalIgnoreCase))
                return entry.Body;
        }

        return $"Instruction '{name}' not found. Use xslt_list_instructions to see all available instructions.";
    }

    [McpServerTool(Name = "xslt_lookup_function"), Description(
        "Look up an XPath/XSLT function by name (e.g., 'fn:tokenize', 'fn:string-join'). " +
        "Returns signature, parameters, return type, description, and examples.")]
    public static string LookupFunction(
        SpecIndex index,
        [Description("Function name (e.g., 'fn:tokenize', 'fn:count', 'fn:string-join')")] string name)
    {
        var entry = index.Lookup(name);
        if (entry != null && string.Equals(entry.Category, "function", StringComparison.OrdinalIgnoreCase))
            return entry.Body;

        // Try with fn: prefix if not found
        if (!name.StartsWith("fn:", StringComparison.OrdinalIgnoreCase))
        {
            entry = index.Lookup("fn:" + name);
            if (entry != null && string.Equals(entry.Category, "function", StringComparison.OrdinalIgnoreCase))
                return entry.Body;
        }

        return $"Function '{name}' not found. Use xslt_list_functions to see all available functions.";
    }

    [McpServerTool(Name = "xslt_lookup_output_method"), Description(
        "Look up an XSLT output method (xml, html, json, text, xhtml, csv, adaptive). " +
        "Returns all serialization parameters and behavior.")]
    public static string LookupOutputMethod(
        SpecIndex index,
        [Description("Output method name (e.g., 'xml', 'html', 'json', 'text')")] string name)
    {
        var entry = index.Lookup(name);
        if (entry != null && string.Equals(entry.Category, "output-method", StringComparison.OrdinalIgnoreCase))
            return entry.Body;

        return $"Output method '{name}' not found. Available methods: " +
               string.Join(", ", index.GetByCategory("output-method").Select(e => e.Name));
    }

    [McpServerTool(Name = "xslt_lookup_error_code"), Description(
        "Look up an XSLT/XPath error code (e.g., 'XTSE0010', 'FODC0002'). " +
        "Returns description, conditions, and fix suggestions.")]
    public static string LookupErrorCode(
        SpecIndex index,
        [Description("Error code (e.g., 'XTSE0010', 'FODC0002', 'XPDY0002')")] string code)
    {
        var entry = index.Lookup(code);
        if (entry != null && string.Equals(entry.Category, "error-code", StringComparison.OrdinalIgnoreCase))
            return entry.Body;

        return $"Error code '{code}' not found. Use xslt_search with the error code prefix (e.g., 'XTSE') to find related errors.";
    }

    [McpServerTool(Name = "xslt_search"), Description(
        "Search across all XSLT spec files by keyword. Returns matching entries with relevant excerpts.")]
    public static string Search(
        SpecIndex index,
        [Description("Search query — keywords or natural language")] string query,
        [Description("Maximum results to return (default 10)")] int limit = 10)
    {
        var results = index.Search(query);
        if (results.Count == 0)
            return $"No results found for '{query}'.";

        var sb = new StringBuilder();
        sb.AppendLine($"Found {results.Count} result(s) for '{query}':");
        sb.AppendLine();

        foreach (var entry in results.Take(limit))
        {
            sb.AppendLine($"## {entry.Name} ({entry.Category})");
            if (!string.IsNullOrEmpty(entry.Since))
                sb.AppendLine($"Since: XSLT {entry.Since}");

            // Show first ~200 chars of body as excerpt
            var excerpt = entry.Body.Length > 200 ? entry.Body[..200] + "..." : entry.Body;
            // Trim to last complete line
            var lastNewline = excerpt.LastIndexOf('\n');
            if (lastNewline > 100)
                excerpt = excerpt[..lastNewline] + "...";
            sb.AppendLine(excerpt);
            sb.AppendLine();
        }

        return sb.ToString();
    }

    [McpServerTool(Name = "xslt_list_instructions"), Description(
        "List all XSLT instructions with one-line descriptions.")]
    public static string ListInstructions(SpecIndex index)
    {
        var instructions = index.GetByCategory("instruction");
        if (instructions.Count == 0)
            return "No instructions found in the spec index.";

        var sb = new StringBuilder();
        sb.AppendLine($"# XSLT Instructions ({instructions.Count})");
        sb.AppendLine();

        foreach (var entry in instructions.OrderBy(e => e.Name, StringComparer.OrdinalIgnoreCase))
        {
            // Extract first non-heading line as description
            var desc = GetFirstDescription(entry.Body);
            sb.AppendLine($"- **{entry.Name}** — {desc}");
        }

        return sb.ToString();
    }

    [McpServerTool(Name = "xslt_list_functions"), Description(
        "List all XPath/XSLT functions organized by category.")]
    public static string ListFunctions(SpecIndex index)
    {
        var functions = index.GetByCategory("function");
        if (functions.Count == 0)
            return "No functions found in the spec index.";

        var sb = new StringBuilder();
        sb.AppendLine($"# XPath/XSLT Functions ({functions.Count})");
        sb.AppendLine();

        // Group by subdirectory (string, numeric, sequence, boolean)
        var grouped = functions
            .GroupBy(e => GetFunctionGroup(e.FilePath))
            .OrderBy(g => g.Key, StringComparer.OrdinalIgnoreCase);

        foreach (var group in grouped)
        {
            sb.AppendLine($"## {group.Key}");
            foreach (var entry in group.OrderBy(e => e.Name, StringComparer.OrdinalIgnoreCase))
            {
                var desc = GetFirstDescription(entry.Body);
                sb.AppendLine($"- **{entry.Name}** — {desc}");
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    /// <summary>
    /// Extracts the first non-heading, non-empty line from the body as a short description.
    /// </summary>
    private static string GetFirstDescription(string body)
    {
        foreach (var line in body.Split('\n'))
        {
            var trimmed = line.Trim();
            if (trimmed.Length == 0 || trimmed.StartsWith('#'))
                continue;
            // Truncate long descriptions
            return trimmed.Length > 120 ? trimmed[..120] + "..." : trimmed;
        }
        return "(no description)";
    }

    /// <summary>
    /// Infers the function group (string, numeric, etc.) from the file path.
    /// </summary>
    private static string GetFunctionGroup(string filePath)
    {
        var parts = filePath.Replace('\\', '/').Split('/');
        // Look for the directory name before the filename, under functions/
        for (var i = 0; i < parts.Length - 1; i++)
        {
            if (string.Equals(parts[i], "functions", StringComparison.OrdinalIgnoreCase) && i + 1 < parts.Length - 1)
                return char.ToUpperInvariant(parts[i + 1][0]) + parts[i + 1][1..];
        }
        return "Other";
    }
}
