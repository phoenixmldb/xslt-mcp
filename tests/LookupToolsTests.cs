using System.Text.Json;
using XsltMcpServer;
using XsltMcpServer.Tools;
using Xunit;

namespace XsltMcpServer.Tests;

public class LookupToolsTests
{
    private static SpecIndex GetIndex() =>
        SpecIndex.LoadFromAssembly(typeof(SpecIndex).Assembly);

    [Fact]
    public void CompareVersions_XslMerge_ReturnsSince_3_0()
    {
        var json = LookupTools.CompareVersions(GetIndex(), "xsl:merge");
        using var doc = JsonDocument.Parse(json);
        Assert.True(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.Equal("xsl:merge", doc.RootElement.GetProperty("name").GetString());
        Assert.Equal("3.0", doc.RootElement.GetProperty("since").GetString());
        Assert.Equal("instruction", doc.RootElement.GetProperty("category").GetString());
    }

    [Fact]
    public void CompareVersions_UnknownName_ReturnsOkFalse()
    {
        var json = LookupTools.CompareVersions(GetIndex(), "xsl:does-not-exist");
        using var doc = JsonDocument.Parse(json);
        Assert.False(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.Equal("xsl:does-not-exist", doc.RootElement.GetProperty("name").GetString());
        Assert.True(doc.RootElement.GetProperty("message").GetString()!.Length > 0);
    }

    [Fact]
    public void FindExamples_XslMerge_ReturnsExample()
    {
        var json = LookupTools.FindExamples(GetIndex(), "xsl:merge");
        using var doc = JsonDocument.Parse(json);
        Assert.True(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.True(doc.RootElement.GetProperty("count").GetInt32() >= 1);
    }

    [Fact]
    public void FindExamples_NoMatch_ReturnsEmpty()
    {
        var json = LookupTools.FindExamples(GetIndex(), "totally-fake-feature-xyz");
        using var doc = JsonDocument.Parse(json);
        Assert.True(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.Equal(0, doc.RootElement.GetProperty("count").GetInt32());
    }
}
