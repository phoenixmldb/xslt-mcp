using System.Collections.Concurrent;
using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;
using PhoenixmlDb.Xslt;
using PhoenixmlDb.Xslt.Engine;
using XsltMcpServer.Models;

namespace XsltMcpServer.Tools;

[McpServerToolType]
public static class CompileTools
{
    private static readonly ConcurrentDictionary<string, XsltTransformer> Cache = new();
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> Locks = new();

    [McpServerTool(Name = "xslt_compile"), Description(
        "Compile an XSLT stylesheet and return a handle that can be reused via xslt_apply. The same stylesheet always produces the same handle (SHA256-based) — calling compile twice is idempotent. Cache is process-lifetime.")]
    public static async Task<string> Compile(
        [Description("XSLT stylesheet (complete XML)")] string stylesheet)
    {
        var handle = Convert.ToHexString(
            System.Security.Cryptography.SHA256.HashData(
                System.Text.Encoding.UTF8.GetBytes(stylesheet)))[..16];

        if (Cache.ContainsKey(handle))
            return JsonSerializer.Serialize(new { ok = true, handle }, XsltErrorMapper.JsonOpts);

        try
        {
            var t = new XsltTransformer();
            await t.LoadStylesheetAsync(stylesheet, XsltErrorMapper.SyntheticBaseUri);
            Cache[handle] = t;
            return JsonSerializer.Serialize(new { ok = true, handle }, XsltErrorMapper.JsonOpts);
        }
        catch (XsltException ex)
        {
            return JsonSerializer.Serialize(TransformResult.Failure(XsltErrorMapper.ToErrors(ex)), XsltErrorMapper.JsonOpts);
        }
        catch (System.Xml.XmlException ex)
        {
            return JsonSerializer.Serialize(TransformResult.Failure(XsltErrorMapper.ToErrors(ex)), XsltErrorMapper.JsonOpts);
        }
    }

    /// <summary>
    /// Apply a previously compiled stylesheet (from xslt_compile) to a source XML document.
    /// Returns the same TransformResult shape as xslt_transform.
    /// Note: because the cached XsltTransformer is reused across calls, parameters set in a
    /// prior Apply call with the same handle remain in effect unless overwritten by name in a
    /// subsequent call (no Clear method is available on the engine). To avoid parameter leakage
    /// across calls with different parameter sets, use xslt_transform instead.
    /// </summary>
    [McpServerTool(Name = "xslt_apply"), Description(
        "Apply a previously compiled stylesheet (from xslt_compile) to a source XML document. Returns the same TransformResult shape as xslt_transform. Note: parameters set in a prior call with the same handle persist unless overwritten by name — use xslt_transform if you need isolated parameter sets.")]
    public static async Task<string> Apply(
        [Description("Handle returned by xslt_compile")] string handle,
        [Description("Source XML document")] string sourceXml,
        [Description("Optional JSON object mapping parameter name → value, e.g. {\"name\": \"value\", \"count\": 42}. Strings bind as xs:string, numbers as xs:double, booleans as xs:boolean.")] string? parameters = null,
        [Description("Optional JSON object mapping URI → XML for doc()/document() resolution: {\"lookup.xml\": \"<lookup/>\"}. URIs are resolved relative to the stylesheet's base URI; pass absolute or bare names.")] string? documents = null)
    {
        if (!string.IsNullOrEmpty(parameters))
        {
            try { JsonDocument.Parse(parameters).Dispose(); }
            catch (JsonException ex)
            {
                var err = new TransformError("XMCP0002",
                    $"Invalid parameters JSON: {ex.Message}", null, null, null, null);
                return JsonSerializer.Serialize(TransformResult.Failure(new[] { err }), XsltErrorMapper.JsonOpts);
            }
        }

        if (!string.IsNullOrEmpty(documents))
        {
            try { JsonDocument.Parse(documents).Dispose(); }
            catch (JsonException ex)
            {
                var err = new TransformError("XMCP0003",
                    $"Invalid documents JSON: {ex.Message}", null, null, null, null);
                return JsonSerializer.Serialize(TransformResult.Failure(new[] { err }), XsltErrorMapper.JsonOpts);
            }
        }

        if (!Cache.TryGetValue(handle, out var t))
        {
            var err = new TransformError("XMCP0001",
                $"Unknown stylesheet handle '{handle}'. Call xslt_compile first.",
                null, null, null, null);
            return JsonSerializer.Serialize(TransformResult.Failure(new[] { err }), XsltErrorMapper.JsonOpts);
        }

        var lockObj = Locks.GetOrAdd(handle, _ => new SemaphoreSlim(1, 1));
        await lockObj.WaitAsync();
        try
        {
            XsltErrorMapper.ApplyParameters(t, parameters);

            var sw = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                PreloadedResources? prevPreloaded = t.PreloadedResources;
                try
                {
                    if (!string.IsNullOrEmpty(documents))
                        t.PreloadedResources = XsltErrorMapper.BuildPreloadedResources(documents);
                    var output = await t.TransformAsync(sourceXml);
                    sw.Stop();
                    return JsonSerializer.Serialize(
                        TransformResult.Success(output, outputMethod: null, sw.ElapsedMilliseconds), XsltErrorMapper.JsonOpts);
                }
                finally
                {
                    t.PreloadedResources = prevPreloaded;
                }
            }
            catch (ArgumentException ex) when (ex.Message.StartsWith("XMCP0003"))
            {
                var err = new TransformError("XMCP0003", ex.Message, null, null, null, null);
                return JsonSerializer.Serialize(TransformResult.Failure(new[] { err }), XsltErrorMapper.JsonOpts);
            }
            catch (XsltException ex)
            {
                return JsonSerializer.Serialize(TransformResult.Failure(XsltErrorMapper.ToErrors(ex)), XsltErrorMapper.JsonOpts);
            }
            catch (System.Xml.XmlException ex)
            {
                return JsonSerializer.Serialize(TransformResult.Failure(XsltErrorMapper.ToErrors(ex)), XsltErrorMapper.JsonOpts);
            }
        }
        finally
        {
            lockObj.Release();
        }
    }
}
