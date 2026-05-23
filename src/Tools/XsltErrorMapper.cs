using System.Text.Json;
using System.Text.Json.Serialization;
using PhoenixmlDb.XQuery.Parser;
using PhoenixmlDb.Xslt;
using PhoenixmlDb.Xslt.Engine;
using XsltMcpServer.Models;

namespace XsltMcpServer.Tools;

internal static class XsltErrorMapper
{
    internal static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false,
    };

    internal static TransformError[] ToErrors(XQueryParseException ex)
    {
        return ex.Errors.Select(e =>
            new TransformError(ExtractErrorCode(e.Message, "XPST0003"), e.Message, e.Line, e.Column, null, null))
            .ToArray();
    }

    internal static TransformError[] ToErrors(XsltException ex)
    {
        var code = ExtractErrorCode(ex.Message, "XTSE0000");
        var loc = ex.Location;
        return new[]
        {
            new TransformError(code, ex.Message, loc?.Line, loc?.Column, null, null),
        };
    }

    internal static TransformError[] ToErrors(System.Xml.XmlException ex)
    {
        return new[]
        {
            new TransformError("XML", ex.Message, ex.LineNumber > 0 ? ex.LineNumber : null,
                ex.LinePosition > 0 ? ex.LinePosition : null, null, null),
        };
    }

    /// <summary>
    /// Parses <paramref name="parametersJson"/> (a JSON object) and binds each property to the
    /// given transformer via <see cref="XsltTransformer.SetParameter(string, object?)"/>.
    /// JSON strings → <c>xs:string</c>, numbers → <c>double</c>, booleans → <c>bool</c>,
    /// null → null, arrays/objects → raw JSON text.
    /// Throws <see cref="ArgumentException"/> if the JSON is not an object.
    /// </summary>
    internal static void ApplyParameters(XsltTransformer t, string? parametersJson)
    {
        if (string.IsNullOrWhiteSpace(parametersJson)) return;
        using var doc = JsonDocument.Parse(parametersJson);
        if (doc.RootElement.ValueKind != JsonValueKind.Object)
            throw new ArgumentException(
                $"parameters must be a JSON object, got {doc.RootElement.ValueKind}",
                nameof(parametersJson));
        foreach (var prop in doc.RootElement.EnumerateObject())
        {
            object? value = prop.Value.ValueKind switch
            {
                JsonValueKind.String => prop.Value.GetString()!,
                JsonValueKind.Number => prop.Value.GetDouble(),
                JsonValueKind.True or JsonValueKind.False => prop.Value.GetBoolean(),
                JsonValueKind.Null => null,
                _ => prop.Value.GetRawText()  // arrays/objects passed as JSON text
            };
            t.SetParameter(prop.Name, value);
        }
    }

    /// <summary>
    /// The synthetic base URI used when loading stylesheets that supply secondary documents.
    /// Relative document URIs (e.g. "lookup.xml") are resolved against this base so they
    /// become absolute HTTP URIs that <see cref="PreloadedResources"/> can intercept.
    /// </summary>
    internal static readonly Uri SyntheticBaseUri = new("http://xslt-mcp.internal/");

    /// <summary>
    /// Parses <paramref name="documentsJson"/> (a JSON object mapping URI → XML string) and
    /// registers each entry in a <see cref="PreloadedResources"/> instance, resolving bare
    /// or relative names against <see cref="SyntheticBaseUri"/>.
    /// Throws <see cref="ArgumentException"/> (XMCP0003) on malformed input.
    /// </summary>
    internal static PreloadedResources BuildPreloadedResources(string? documentsJson)
    {
        var preloaded = new PreloadedResources();
        if (string.IsNullOrWhiteSpace(documentsJson)) return preloaded;

        using var doc = JsonDocument.Parse(documentsJson);
        if (doc.RootElement.ValueKind != JsonValueKind.Object)
            throw new ArgumentException(
                $"XMCP0003: documents must be a JSON object, got {doc.RootElement.ValueKind}",
                nameof(documentsJson));

        foreach (var prop in doc.RootElement.EnumerateObject())
        {
            if (prop.Value.ValueKind != JsonValueKind.String)
                throw new ArgumentException(
                    $"XMCP0003: documents['{prop.Name}'] must be a string of XML, got {prop.Value.ValueKind}",
                    nameof(documentsJson));

            // Resolve relative/bare names against the synthetic base so the runtime's
            // doc() resolver (which only checks PreloadedResources for http(s) URIs) hits.
            var resolved = Uri.TryCreate(prop.Name, UriKind.Absolute, out var abs)
                ? abs
                : new Uri(SyntheticBaseUri, prop.Name);

            preloaded.Add(resolved, prop.Value.GetString()!);
        }
        return preloaded;
    }

    // Extract "XTSE0010" from messages like "XTSE0010: element xsl:doit is not allowed here"
    internal static string ExtractErrorCode(string message, string fallback)
    {
        if (message.Length >= 8 && message[0] == 'X')
        {
            var colonIdx = message.IndexOf(':');
            if (colonIdx is 8 or 9)
                return message[..colonIdx];
        }
        return fallback;
    }
}
