using XsltMcpServer.Models;
using Xunit;

namespace XsltMcpServer.Tests.Models;

public class TransformResultTests
{
    [Fact]
    public void Success_ShapeMatchesQueryResult()
    {
        var r = TransformResult.Success("<out/>", "xml", elapsedMs: 5);
        Assert.True(r.Ok);
        Assert.Equal("<out/>", r.Value);
        Assert.Equal("xml", r.OutputMethod);
    }

    [Fact]
    public void Failure_CarriesErrorList()
    {
        var err = new TransformError("XTSE0010", "unknown element xsl:doit", 3, 5, null, null);
        var r = TransformResult.Failure(new[] { err });
        Assert.False(r.Ok);
        Assert.Equal("XTSE0010", r.Errors![0].Code);
    }
}
