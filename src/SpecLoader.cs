namespace XsltMcpServer;

/// <summary>
/// A single entry from the XSLT spec reference collection.
/// </summary>
public sealed class SpecEntry
{
    public string Name { get; init; } = "";
    public string Category { get; init; } = "";
    public string Since { get; init; } = "";
    public string SpecUrl { get; init; } = "";
    public string FilePath { get; init; } = "";
    public string Content { get; init; } = "";  // full markdown including frontmatter
    public string Body { get; init; } = "";      // markdown without frontmatter
}

/// <summary>
/// In-memory index of all XSLT spec Markdown files, supporting lookup by name,
/// category, and full-text search.
/// </summary>
public sealed class SpecIndex
{
    private readonly Dictionary<string, SpecEntry> _byName = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, List<SpecEntry>> _byCategory = new(StringComparer.OrdinalIgnoreCase);
    private readonly List<SpecEntry> _all = [];

    /// <summary>
    /// Loads all Markdown files from <paramref name="specDir"/> and its subdirectories,
    /// parsing YAML frontmatter for metadata.
    /// </summary>
    public static SpecIndex Load(string specDir)
    {
        var index = new SpecIndex();
        var dir = new DirectoryInfo(specDir);
        if (!dir.Exists)
            return index;

        foreach (var file in dir.EnumerateFiles("*.md", SearchOption.AllDirectories))
        {
            // Skip files inside the server directory itself
            if (file.FullName.Contains(Path.Combine("xslt-spec-mcp", "server"), StringComparison.OrdinalIgnoreCase))
                continue;

            var content = File.ReadAllText(file.FullName);
            var entry = ParseEntry(content, file.FullName);
            if (entry == null)
                continue;

            index._all.Add(entry);

            if (!string.IsNullOrEmpty(entry.Name))
                index._byName[entry.Name] = entry;

            if (!string.IsNullOrEmpty(entry.Category))
            {
                if (!index._byCategory.TryGetValue(entry.Category, out var list))
                {
                    list = [];
                    index._byCategory[entry.Category] = list;
                }
                list.Add(entry);
            }
        }

        return index;
    }

    /// <summary>
    /// Looks up a spec entry by its <c>name</c> frontmatter field (case-insensitive).
    /// </summary>
    public SpecEntry? Lookup(string name) => _byName.GetValueOrDefault(name);

    /// <summary>
    /// Returns all entries matching the given category.
    /// </summary>
    public IReadOnlyList<SpecEntry> GetByCategory(string category) =>
        _byCategory.GetValueOrDefault(category, []);

    /// <summary>
    /// Full-text search across entry names and body content. Returns entries where
    /// all query terms appear (case-insensitive).
    /// </summary>
    public IReadOnlyList<SpecEntry> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return [];

        var terms = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var results = new List<(SpecEntry Entry, int Score)>();

        foreach (var entry in _all)
        {
            var searchable = $"{entry.Name} {entry.Body}";
            var allMatch = true;
            var score = 0;

            foreach (var term in terms)
            {
                if (!searchable.Contains(term, StringComparison.OrdinalIgnoreCase))
                {
                    allMatch = false;
                    break;
                }

                // Boost score for name matches
                if (entry.Name.Contains(term, StringComparison.OrdinalIgnoreCase))
                    score += 10;
                else
                    score += 1;
            }

            if (allMatch)
                results.Add((entry, score));
        }

        return results
            .OrderByDescending(r => r.Score)
            .Select(r => r.Entry)
            .ToList();
    }

    /// <summary>
    /// Returns all loaded spec entries.
    /// </summary>
    public IReadOnlyList<SpecEntry> GetAll() => _all;

    /// <summary>
    /// Loads spec entries from embedded assembly resources.
    /// </summary>
    public static SpecIndex LoadFromAssembly(System.Reflection.Assembly assembly)
    {
        var index = new SpecIndex();

        foreach (var resourceName in assembly.GetManifestResourceNames())
        {
            if (!resourceName.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
                continue;

            using var stream = assembly.GetManifestResourceStream(resourceName)!;
            using var reader = new StreamReader(stream);
            var content = reader.ReadToEnd();
            var entry = ParseEntry(content, resourceName);
            if (entry != null)
            {
                index._all.Add(entry);

                if (!string.IsNullOrEmpty(entry.Name))
                    index._byName[entry.Name] = entry;

                if (!string.IsNullOrEmpty(entry.Category))
                {
                    if (!index._byCategory.TryGetValue(entry.Category, out var list))
                    {
                        list = [];
                        index._byCategory[entry.Category] = list;
                    }
                    list.Add(entry);
                }
            }
        }

        return index;
    }

    /// <summary>
    /// Parses a Markdown file with YAML frontmatter into a <see cref="SpecEntry"/>.
    /// </summary>
    private static SpecEntry? ParseEntry(string content, string filePath)
    {
        var name = "";
        var category = "";
        var since = "";
        var specUrl = "";
        var body = content;

        // Parse YAML frontmatter between --- delimiters
        if (content.StartsWith("---", StringComparison.Ordinal))
        {
            var endIndex = content.IndexOf("\n---", 3, StringComparison.Ordinal);
            if (endIndex > 0)
            {
                var yaml = content[3..endIndex].Trim();
                // Body starts after the closing ---
                var bodyStart = endIndex + 4; // skip \n---
                if (bodyStart < content.Length && content[bodyStart] == '\n')
                    bodyStart++;
                body = bodyStart < content.Length ? content[bodyStart..].TrimStart() : "";

                foreach (var line in yaml.Split('\n'))
                {
                    var colonIdx = line.IndexOf(':', StringComparison.Ordinal);
                    if (colonIdx <= 0)
                        continue;

                    var key = line[..colonIdx].Trim();
                    var value = line[(colonIdx + 1)..].Trim().Trim('"');

                    switch (key)
                    {
                        case "name":
                            name = value;
                            break;
                        case "category":
                            category = value;
                            break;
                        case "since":
                            since = value;
                            break;
                        case "spec_url":
                            specUrl = value;
                            break;
                    }
                }
            }
        }

        return new SpecEntry
        {
            Name = name,
            Category = category,
            Since = since,
            SpecUrl = specUrl,
            FilePath = filePath,
            Content = content,
            Body = body
        };
    }
}
