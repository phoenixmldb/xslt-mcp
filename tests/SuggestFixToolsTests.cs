using System.Text.Json;
using XsltMcpServer.Tools;
using Xunit;

namespace XsltMcpServer.Tests;

public class SuggestFixToolsTests
{
    [Fact]
    public void Suggest_XTSE0010_OffersNamespaceHint()
    {
        var json = SuggestFixTools.Suggest("XTSE0010");
        using var doc = JsonDocument.Parse(json);
        Assert.True(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.Contains("namespace", doc.RootElement.GetProperty("suggestion").GetString(),
            StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Suggest_UnknownCode_ReturnsOkFalse()
    {
        var json = SuggestFixTools.Suggest("XTXX9999");
        using var doc = JsonDocument.Parse(json);
        Assert.False(doc.RootElement.GetProperty("ok").GetBoolean());
    }

    [Fact]
    public void Suggest_LowerCaseCode_NormalizesAndMatches()
    {
        var json = SuggestFixTools.Suggest("xtse0010");
        using var doc = JsonDocument.Parse(json);
        Assert.True(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.Equal("XTSE0010", doc.RootElement.GetProperty("code").GetString());
    }
}
