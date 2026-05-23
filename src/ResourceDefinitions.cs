using System.ComponentModel;
using System.Linq;
using System.Text;
using ModelContextProtocol.Server;

namespace XsltMcpServer;

[McpServerResourceType]
public static class ResourceDefinitions
{
    [McpServerResource(UriTemplate = "xslt://instructions/{name}"), Description(
        "Read a single XSLT instruction reference page as Markdown.")]
    public static string ReadInstruction(SpecIndex index, string name)
    {
        var entry = index.Lookup(name);
        return entry?.Body ?? $"# Not found\n\nNo entry for `{name}` in the XSLT spec corpus.";
    }

    [McpServerResource(UriTemplate = "xslt://index"), Description(
        "List every spec entry as a Markdown index, organized by category.")]
    public static string ReadIndex(SpecIndex index)
    {
        var sb = new StringBuilder("# XSLT Spec Corpus Index\n\n");
        var groups = index.GetAll()
            .GroupBy(e => e.Category ?? "uncategorized")
            .OrderBy(g => g.Key);
        foreach (var group in groups)
        {
            sb.AppendLine($"## {group.Key}");
            sb.AppendLine();
            foreach (var entry in group.OrderBy(e => e.Name))
                sb.AppendLine($"- [{entry.Name}](xslt://instructions/{entry.Name})");
            sb.AppendLine();
        }
        return sb.ToString();
    }
}
