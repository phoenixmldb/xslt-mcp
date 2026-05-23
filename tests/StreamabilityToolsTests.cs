using System.Text.Json;
using XsltMcpServer.Tools;
using Xunit;

namespace XsltMcpServer.Tests;

public class StreamabilityToolsTests
{
    [Fact]
    public async Task Explain_StreamableMode_ReportsStreamable()
    {
        var ss = """
            <xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
              <xsl:mode name="stream" streamable="yes"/>
              <xsl:template match="item" mode="stream"><out/></xsl:template>
            </xsl:stylesheet>
            """;
        var json = await StreamabilityTools.Explain(ss);
        using var doc = JsonDocument.Parse(json);
        Assert.True(doc.RootElement.GetProperty("ok").GetBoolean());
        var modes = doc.RootElement.GetProperty("modes");
        Assert.True(modes.GetArrayLength() >= 1);
        // Find the "stream" mode
        var streamMode = FindMode(modes, "stream");
        Assert.NotNull(streamMode);
        Assert.True(streamMode!.Value.GetProperty("streamable").GetBoolean());
    }

    [Fact]
    public async Task Explain_NonStreamablePath_ReportsReason()
    {
        // xsl:for-each with a descendant-axis (crawling) select is XTSE3430 in a streamable mode
        var ss = """
            <xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
              <xsl:mode name="bad" streamable="yes"/>
              <xsl:template match="root" mode="bad">
                <out><xsl:for-each select="//item"><xsl:value-of select="."/></xsl:for-each></out>
              </xsl:template>
            </xsl:stylesheet>
            """;
        var json = await StreamabilityTools.Explain(ss);
        using var doc = JsonDocument.Parse(json);
        // The mode "bad" should be reported — either the full load fails, or per-template probe fails
        var modes = doc.RootElement.GetProperty("modes");
        var badMode = FindMode(modes, "bad");
        Assert.NotNull(badMode);

        // The template inside must report non-streamable with a reason
        var templates = badMode!.Value.GetProperty("templates");
        Assert.True(templates.GetArrayLength() >= 1);
        var firstTemplate = templates[0];
        Assert.False(firstTemplate.GetProperty("streamable").GetBoolean());
        // Must have a human-readable reason string
        Assert.True(firstTemplate.TryGetProperty("reason", out var reasonProp));
        var reason = reasonProp.GetString();
        Assert.NotNull(reason);
        Assert.NotEmpty(reason!);
        // Reason should mention the specific problem
        Assert.Contains("crawling", reason!, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Explain_NoExplicitMode_ReportsDefaultMode()
    {
        var ss = """
            <xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
              <xsl:template match="/"><out/></xsl:template>
            </xsl:stylesheet>
            """;
        var json = await StreamabilityTools.Explain(ss);
        using var doc = JsonDocument.Parse(json);
        Assert.True(doc.RootElement.GetProperty("ok").GetBoolean());
        var modes = doc.RootElement.GetProperty("modes");
        // Default mode should appear
        var defaultMode = FindMode(modes, "#default");
        Assert.NotNull(defaultMode);
        // Default mode is not streamable (no xsl:mode streamable="yes")
        Assert.False(defaultMode!.Value.GetProperty("streamable").GetBoolean());
    }

    [Fact]
    public async Task Explain_NonStreamableMode_ListsTemplates()
    {
        var ss = """
            <xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
              <xsl:template match="/"><out/></xsl:template>
              <xsl:template match="item"><i/></xsl:template>
            </xsl:stylesheet>
            """;
        var json = await StreamabilityTools.Explain(ss);
        using var doc = JsonDocument.Parse(json);
        var modes = doc.RootElement.GetProperty("modes");
        var defaultMode = modes.EnumerateArray().Single();
        Assert.False(defaultMode.GetProperty("streamable").GetBoolean());
        var templates = defaultMode.GetProperty("templates");
        Assert.Equal(2, templates.GetArrayLength());
        // All templates in a non-streamable mode are non-streamable
        foreach (var t in templates.EnumerateArray())
            Assert.False(t.GetProperty("streamable").GetBoolean());
    }

    // Helper: find a mode by name in the modes array
    private static JsonElement? FindMode(JsonElement modesArray, string name)
    {
        foreach (var mode in modesArray.EnumerateArray())
        {
            if (mode.GetProperty("name").GetString() == name)
                return mode;
        }
        return null;
    }
}
