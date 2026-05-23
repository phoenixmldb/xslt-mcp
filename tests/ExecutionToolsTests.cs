using System.Text.Json;
using XsltMcpServer.Tools;
using Xunit;

namespace XsltMcpServer.Tests;

public class ExecutionToolsTests
{
    private const string IdentityStylesheet = """
        <xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
          <xsl:template match="/"><xsl:copy-of select="."/></xsl:template>
        </xsl:stylesheet>
        """;

    [Fact]
    public async Task Transform_Success_ReturnsStructured()
    {
        var json = await ExecutionTools.Transform(IdentityStylesheet, "<r/>");
        using var doc = JsonDocument.Parse(json);
        Assert.True(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.Contains("<r", doc.RootElement.GetProperty("value").GetString());
    }

    [Fact]
    public async Task Transform_BadStylesheet_ReturnsStructuredError()
    {
        var bad = IdentityStylesheet.Replace("xsl:template", "xsl:doit");
        var json = await ExecutionTools.Transform(bad, "<r/>");
        using var doc = JsonDocument.Parse(json);
        Assert.False(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.True(doc.RootElement.GetProperty("errors").GetArrayLength() >= 1);
    }

    [Fact]
    public async Task Validate_GoodStylesheet_ReturnsOkTrue()
    {
        var json = await ExecutionTools.Validate(IdentityStylesheet);
        using var doc = JsonDocument.Parse(json);
        Assert.True(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.False(doc.RootElement.TryGetProperty("value", out _));  // value omitted on validate-success
    }

    [Fact]
    public async Task Validate_BadStylesheet_ReturnsStructuredErrors()
    {
        var bad = IdentityStylesheet.Replace("xsl:template", "xsl:doit");
        var json = await ExecutionTools.Validate(bad);
        using var doc = JsonDocument.Parse(json);
        Assert.False(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.True(doc.RootElement.GetProperty("errors").GetArrayLength() >= 1);
    }

    [Fact]
    public async Task EvaluateXPath_Success_ReturnsStructured()
    {
        var json = await ExecutionTools.EvaluateXPath("count(/r/i)", "<r><i/><i/></r>");
        using var doc = JsonDocument.Parse(json);
        Assert.True(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.Equal("2", doc.RootElement.GetProperty("value").GetString());
    }

    [Fact]
    public async Task EvaluateXPath_SyntaxError_ReturnsStructuredErrors()
    {
        var json = await ExecutionTools.EvaluateXPath("count(", "<r/>");
        using var doc = JsonDocument.Parse(json);
        Assert.False(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.True(doc.RootElement.GetProperty("errors").GetArrayLength() >= 1);
    }

    [Fact]
    public async Task Transform_WithParameter_AppliesValue()
    {
        var ss = """
            <xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
              <xsl:param name="greeting" as="xs:string" xmlns:xs="http://www.w3.org/2001/XMLSchema"/>
              <xsl:template match="/"><out><xsl:value-of select="$greeting"/></out></xsl:template>
            </xsl:stylesheet>
            """;
        var json = await ExecutionTools.Transform(ss, "<r/>",
            parameters: """{"greeting": "hello"}""");
        using var doc = JsonDocument.Parse(json);
        Assert.True(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.Contains("hello", doc.RootElement.GetProperty("value").GetString());
    }

    [Fact]
    public async Task Transform_NumericParameter_BindsAsDouble()
    {
        var ss = """
            <xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
              <xsl:param name="n" as="xs:double" xmlns:xs="http://www.w3.org/2001/XMLSchema"/>
              <xsl:template match="/"><out><xsl:value-of select="$n * 2"/></out></xsl:template>
            </xsl:stylesheet>
            """;
        var json = await ExecutionTools.Transform(ss, "<r/>",
            parameters: """{"n": 21}""");
        using var doc = JsonDocument.Parse(json);
        Assert.True(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.Contains("42", doc.RootElement.GetProperty("value").GetString());
    }

    [Fact]
    public async Task Transform_BadParametersJson_ReturnsStructuredError()
    {
        var json = await ExecutionTools.Transform(IdentityStylesheet, "<r/>",
            parameters: "not-json");
        using var doc = JsonDocument.Parse(json);
        Assert.False(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.Equal("XMCP0002", doc.RootElement.GetProperty("errors")[0].GetProperty("code").GetString());
    }

    [Fact]
    public async Task Transform_WithSecondaryDoc_ResolvesDocCall()
    {
        var ss = """
            <xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
              <xsl:output method="xml" indent="no"/>
              <xsl:template match="/">
                <out><xsl:value-of select="doc('lookup.xml')/lookup/value"/></out>
              </xsl:template>
            </xsl:stylesheet>
            """;
        var json = await ExecutionTools.Transform(ss, "<r/>",
            parameters: null,
            documents: """{"lookup.xml": "<lookup><value>42</value></lookup>"}""");
        using var doc = JsonDocument.Parse(json);
        Assert.True(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.Contains("42", doc.RootElement.GetProperty("value").GetString());
    }

    [Fact]
    public async Task Transform_BadDocumentsJson_ReturnsStructuredError()
    {
        var json = await ExecutionTools.Transform(IdentityStylesheet, "<r/>",
            parameters: null,
            documents: "not-json");
        using var doc = JsonDocument.Parse(json);
        Assert.False(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.Equal("XMCP0003", doc.RootElement.GetProperty("errors")[0].GetProperty("code").GetString());
    }

    [Fact]
    public async Task Test_Pass_ReturnsPassedTrue()
    {
        var json = await ExecutionTools.Test(IdentityStylesheet, "<r/>", "<r/>");
        using var doc = JsonDocument.Parse(json);
        Assert.True(doc.RootElement.GetProperty("passed").GetBoolean());
    }

    [Fact]
    public async Task Test_Fail_ReturnsPassedFalseWithDiff()
    {
        var json = await ExecutionTools.Test(IdentityStylesheet, "<r><x/></r>", "<r/>");
        using var doc = JsonDocument.Parse(json);
        Assert.False(doc.RootElement.GetProperty("passed").GetBoolean());
        Assert.True(doc.RootElement.TryGetProperty("diff", out var d) && d.GetString()!.Length > 0);
    }

    [Fact]
    public async Task Test_TransformError_ReturnsPassedFalseWithError()
    {
        var bad = IdentityStylesheet.Replace("xsl:template", "xsl:doit");
        var json = await ExecutionTools.Test(bad, "<r/>", "<r/>");
        using var doc = JsonDocument.Parse(json);
        Assert.False(doc.RootElement.GetProperty("passed").GetBoolean());
        Assert.True(doc.RootElement.TryGetProperty("error", out _));
    }
}
