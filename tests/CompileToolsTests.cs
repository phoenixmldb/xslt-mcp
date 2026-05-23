using System.Text.Json;
using XsltMcpServer.Tools;
using Xunit;

namespace XsltMcpServer.Tests;

public class CompileToolsTests
{
    private const string IdentityStylesheet = """
        <xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
          <xsl:template match="/"><xsl:copy-of select="."/></xsl:template>
        </xsl:stylesheet>
        """;

    [Fact]
    public async Task Compile_Then_Apply_ReturnsTransformOutput()
    {
        var compileJson = await CompileTools.Compile(IdentityStylesheet);
        using var compileDoc = JsonDocument.Parse(compileJson);
        Assert.True(compileDoc.RootElement.GetProperty("ok").GetBoolean());
        var handle = compileDoc.RootElement.GetProperty("handle").GetString()!;

        var applyJson = await CompileTools.Apply(handle, "<r/>");
        using var applyDoc = JsonDocument.Parse(applyJson);
        Assert.True(applyDoc.RootElement.GetProperty("ok").GetBoolean());
    }

    [Fact]
    public async Task Apply_UnknownHandle_ReturnsError()
    {
        var json = await CompileTools.Apply("nonexistent", "<r/>");
        using var doc = JsonDocument.Parse(json);
        Assert.False(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.Equal("XMCP0001", doc.RootElement.GetProperty("errors")[0].GetProperty("code").GetString());
    }

    [Fact]
    public async Task Compile_SameStylesheet_ReturnsSameHandle()
    {
        var a = await CompileTools.Compile(IdentityStylesheet);
        var b = await CompileTools.Compile(IdentityStylesheet);
        using var da = JsonDocument.Parse(a);
        using var db = JsonDocument.Parse(b);
        Assert.Equal(
            da.RootElement.GetProperty("handle").GetString(),
            db.RootElement.GetProperty("handle").GetString());
    }

    [Fact]
    public async Task Apply_WithParameter_AppliesValue()
    {
        var ss = """
            <xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
              <xsl:param name="greeting" as="xs:string" xmlns:xs="http://www.w3.org/2001/XMLSchema"/>
              <xsl:template match="/"><out><xsl:value-of select="$greeting"/></out></xsl:template>
            </xsl:stylesheet>
            """;
        var compileJson = await CompileTools.Compile(ss);
        using var compileDoc = JsonDocument.Parse(compileJson);
        var handle = compileDoc.RootElement.GetProperty("handle").GetString()!;

        var applyJson = await CompileTools.Apply(handle, "<r/>",
            parameters: """{"greeting": "hi"}""");
        using var applyDoc = JsonDocument.Parse(applyJson);
        Assert.True(applyDoc.RootElement.GetProperty("ok").GetBoolean());
        Assert.Contains("hi", applyDoc.RootElement.GetProperty("value").GetString());
    }

    [Fact]
    public async Task Apply_WithSecondaryDoc_ResolvesDocCall()
    {
        // Use an absolute URI in doc() so resolution works regardless of the stylesheet's
        // base URI (the Compile tool compiles without a synthetic base URI).
        var ss = """
            <xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
              <xsl:output method="xml" indent="no"/>
              <xsl:template match="/">
                <out><xsl:value-of select="doc('http://xslt-mcp.internal/lookup.xml')/lookup/value"/></out>
              </xsl:template>
            </xsl:stylesheet>
            """;
        var compileJson = await CompileTools.Compile(ss);
        using var compileDoc = JsonDocument.Parse(compileJson);
        var handle = compileDoc.RootElement.GetProperty("handle").GetString()!;

        var applyJson = await CompileTools.Apply(handle, "<r/>",
            parameters: null,
            documents: """{"http://xslt-mcp.internal/lookup.xml": "<lookup><value>99</value></lookup>"}""");
        using var applyDoc = JsonDocument.Parse(applyJson);
        Assert.True(applyDoc.RootElement.GetProperty("ok").GetBoolean());
        Assert.Contains("99", applyDoc.RootElement.GetProperty("value").GetString());
    }

    [Fact]
    public async Task Apply_WithSecondaryDoc_RelativeUri_Resolves()
    {
        var ss = """
            <xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
              <xsl:template match="/">
                <out><xsl:value-of select="doc('lookup.xml')/lookup/value"/></out>
              </xsl:template>
            </xsl:stylesheet>
            """;
        var compileJson = await CompileTools.Compile(ss);
        using var compileDoc = JsonDocument.Parse(compileJson);
        var handle = compileDoc.RootElement.GetProperty("handle").GetString()!;

        var applyJson = await CompileTools.Apply(handle, "<r/>",
            parameters: null,
            documents: """{"lookup.xml": "<lookup><value>99</value></lookup>"}""");
        using var applyDoc = JsonDocument.Parse(applyJson);
        Assert.True(applyDoc.RootElement.GetProperty("ok").GetBoolean());
        Assert.Contains("99", applyDoc.RootElement.GetProperty("value").GetString());
    }

    [Fact]
    public async Task Apply_BadDocumentsJson_ReturnsStructuredError()
    {
        var compileJson = await CompileTools.Compile(IdentityStylesheet);
        using var compileDoc = JsonDocument.Parse(compileJson);
        var handle = compileDoc.RootElement.GetProperty("handle").GetString()!;

        var json = await CompileTools.Apply(handle, "<r/>",
            parameters: null,
            documents: "not-json");
        using var doc = JsonDocument.Parse(json);
        Assert.False(doc.RootElement.GetProperty("ok").GetBoolean());
        Assert.Equal("XMCP0003", doc.RootElement.GetProperty("errors")[0].GetProperty("code").GetString());
    }
}
