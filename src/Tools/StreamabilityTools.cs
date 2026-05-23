using System.ComponentModel;
using System.Text.Json;
using System.Xml.Linq;
using ModelContextProtocol.Server;
using PhoenixmlDb.Xslt;
using PhoenixmlDb.Xslt.Engine;
using XsltMcpServer.Models;

namespace XsltMcpServer.Tools;

[McpServerToolType]
public static class StreamabilityTools
{
    private static readonly XNamespace XsltNs = "http://www.w3.org/1999/XSL/Transform";

    [McpServerTool(Name = "xslt_explain_streamability"), Description(
        "Analyze a stylesheet and report which xsl:mode and xsl:template are streamable. " +
        "Returns { ok, modes: [{ name, streamable, reason?, templates: [...] }] }. " +
        "NOTE: Detection is based on the engine's conservative XTSE3430 checks, not a full " +
        "spec-compliant posture/sweep analysis per XSLT 3.0 §19. False negatives are possible " +
        "for patterns the engine does not yet enforce (e.g., subtle context-dependent crawls). " +
        "Use this to catch obvious streamability mistakes early; final validation must still be " +
        "the actual transform. " +
        "Mode name '#default' means the unnamed default mode.")]
    public static async Task<string> Explain(
        [Description("XSLT stylesheet (complete XML)")] string stylesheet)
    {
        // Phase 1: First try to load the whole stylesheet. If it fails with a non-streamability
        // error (e.g. parse error, invalid XSLT), return that error immediately.
        // If it succeeds, we know all templates are streamability-compliant.
        // If it fails with XTSE3430, we fall through to per-template probing.
        XDocument xdoc;
        try
        {
            xdoc = XDocument.Parse(stylesheet);
        }
        catch (System.Xml.XmlException ex)
        {
            return JsonSerializer.Serialize(
                TransformResult.Failure(XsltErrorMapper.ToErrors(ex)),
                XsltErrorMapper.JsonOpts);
        }

        // Enumerate modes declared in the stylesheet
        var declaredModes = ParseDeclaredModes(xdoc);
        // Enumerate templates and assign them to modes
        var templatesByMode = ParseTemplatesByMode(xdoc, declaredModes);

        // Phase 2: Try loading the full stylesheet to catch non-streamability XSLT errors.
        // If it fails with something other than XTSE3430, surface it immediately.
        // XTSE3430 errors are surfaced per-template via per-template probing below.
        try
        {
            var t = new XsltTransformer();
            await t.LoadStylesheetAsync(stylesheet);
        }
        catch (XsltException ex) when (ex.Message.Contains("XTSE3430"))
        {
            // Swallow — the per-template probing will surface more specific reasons
        }
        catch (XsltException ex)
        {
            // Non-streamability XSLT error — surface it as a compile error
            return JsonSerializer.Serialize(
                TransformResult.Failure(XsltErrorMapper.ToErrors(ex)),
                XsltErrorMapper.JsonOpts);
        }
        catch (System.Xml.XmlException ex)
        {
            return JsonSerializer.Serialize(
                TransformResult.Failure(XsltErrorMapper.ToErrors(ex)),
                XsltErrorMapper.JsonOpts);
        }

        // Phase 3: Build per-mode results
        var modeResults = new List<object>();

        foreach (var (modeName, isStreamable) in declaredModes)
        {
            if (!isStreamable)
            {
                // Non-streamable mode — list each template as non-streamable without probing.
                // The mode-level reason explains why; per-template reasons are not needed.
                var nonStreamableTemplates = templatesByMode.TryGetValue(modeName, out var nsList) ? nsList : new List<XElement>();
                var nonStreamableTemplateResults = nonStreamableTemplates
                    .Select(t => (object)new
                    {
                        match = t.Attribute("match")?.Value ?? "(named template)",
                        streamable = false
                    })
                    .ToList();
                modeResults.Add(new
                {
                    name = modeName,
                    streamable = false,
                    reason = "Mode is not declared streamable=\"yes\"",
                    templates = nonStreamableTemplateResults
                });
                continue;
            }

            // Streamable mode — probe each template individually
            var templates = templatesByMode.TryGetValue(modeName, out var tmplList) ? tmplList : new List<XElement>();
            var templateResults = new List<object>();
            bool anyTemplateNonStreamable = false;

            foreach (var tmplEl in templates)
            {
                var matchAttr = tmplEl.Attribute("match")?.Value ?? "(named template)";
                var reason = await ProbeTemplateAsync(xdoc, modeName, tmplEl);
                if (reason == null)
                {
                    templateResults.Add(new { match = matchAttr, streamable = true });
                }
                else
                {
                    anyTemplateNonStreamable = true;
                    templateResults.Add(new { match = matchAttr, streamable = false, reason });
                }
            }

            bool modeStreamable = !anyTemplateNonStreamable;

            modeResults.Add(new
            {
                name = modeName,
                streamable = modeStreamable,
                templates = templateResults
            });
        }

        return JsonSerializer.Serialize(new { ok = true, modes = modeResults }, XsltErrorMapper.JsonOpts);
    }

    /// <summary>
    /// Parses xsl:mode declarations, returns list of (displayName, isStreamable).
    /// Includes the implicit default mode if templates exist without explicit modes.
    /// </summary>
    private static List<(string Name, bool Streamable)> ParseDeclaredModes(XDocument xdoc)
    {
        var result = new List<(string, bool)>();
        var seen = new HashSet<string>(StringComparer.Ordinal);

        var root = xdoc.Root;
        if (root == null) return result;

        // Explicit xsl:mode declarations
        foreach (var modeEl in root.Elements(XsltNs + "mode"))
        {
            var nameAttr = modeEl.Attribute("name")?.Value?.Trim();
            var displayName = string.IsNullOrEmpty(nameAttr) ? "#default" : nameAttr;
            var streamableAttr = modeEl.Attribute("streamable")?.Value?.Trim();
            var streamable = string.Equals(streamableAttr, "yes", StringComparison.OrdinalIgnoreCase)
                          || string.Equals(streamableAttr, "true", StringComparison.OrdinalIgnoreCase)
                          || string.Equals(streamableAttr, "1", StringComparison.OrdinalIgnoreCase);

            if (seen.Add(displayName))
                result.Add((displayName, streamable));
        }

        // Implicit default mode — if any template exists without a mode= attribute,
        // the default mode is used. Add it as non-streamable if not already declared.
        bool hasImplicitDefault = root.Elements(XsltNs + "template").Any(t =>
            t.Attribute("mode") == null);
        if (hasImplicitDefault && seen.Add("#default"))
        {
            result.Add(("#default", false));
        }

        return result;
    }

    /// <summary>
    /// Groups template XElements by mode display name.
    /// A template with no mode= goes into "#default".
    /// A template with mode="foo bar" goes into both "foo" and "bar".
    /// </summary>
    private static Dictionary<string, List<XElement>> ParseTemplatesByMode(
        XDocument xdoc,
        List<(string Name, bool Streamable)> modes)
    {
        var result = new Dictionary<string, List<XElement>>(StringComparer.Ordinal);
        var root = xdoc.Root;
        if (root == null) return result;

        foreach (var tmplEl in root.Elements(XsltNs + "template"))
        {
            var modeAttr = tmplEl.Attribute("mode")?.Value?.Trim();
            var templateModes = new List<string>();

            if (string.IsNullOrEmpty(modeAttr))
            {
                templateModes.Add("#default");
            }
            else
            {
                // mode can be "#all" or space-separated list of mode names
                if (modeAttr == "#all")
                {
                    // Participates in all modes
                    foreach (var (name, _) in modes)
                        templateModes.Add(name);
                }
                else
                {
                    foreach (var part in modeAttr.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                        templateModes.Add(part == "#default" ? "#default" : part);
                }
            }

            foreach (var modeName in templateModes)
            {
                if (!result.TryGetValue(modeName, out var list))
                    result[modeName] = list = new List<XElement>();
                list.Add(tmplEl);
            }
        }

        return result;
    }

    /// <summary>
    /// Probes a single template in a streamable mode by constructing a minimal stylesheet
    /// containing just that mode declaration and template, loading it, and catching XTSE3430.
    /// Returns null if streamable; returns the reason message if not.
    /// </summary>
    private static async Task<string?> ProbeTemplateAsync(
        XDocument originalDoc,
        string modeName,
        XElement templateEl)
    {
        // Build a minimal probe stylesheet using XDocument, preserving all namespace
        // declarations from the original root so element/attribute names in the template
        // body serialize correctly.
        var originalRoot = originalDoc.Root!;
        var version = originalRoot.Attribute("version")?.Value ?? "3.0";

        // Build a new root element, copying all namespace declarations from the original
        var probeRoot = new XElement(XsltNs + "stylesheet");
        probeRoot.SetAttributeValue("version", version);
        // Copy all namespace declarations from the original root
        foreach (var attr in originalRoot.Attributes())
        {
            if (attr.IsNamespaceDeclaration)
                probeRoot.Add(attr);
        }

        // Add the mode declaration
        var modeEl = new XElement(XsltNs + "mode");
        modeEl.SetAttributeValue("streamable", "yes");
        if (modeName != "#default")
            modeEl.SetAttributeValue("name", modeName);
        probeRoot.Add(modeEl);

        // Clone the template, setting the mode attribute correctly
        var clonedTemplate = new XElement(templateEl);
        if (modeName == "#default")
            clonedTemplate.SetAttributeValue("mode", null);
        else
            clonedTemplate.SetAttributeValue("mode", modeName);
        probeRoot.Add(clonedTemplate);

        var probeDoc = new XDocument(probeRoot);
        var probeXml = probeDoc.ToString(SaveOptions.DisableFormatting);

        try
        {
            var t = new XsltTransformer();
            await t.LoadStylesheetAsync(probeXml);
            return null; // streamable
        }
        catch (XsltException ex) when (ex.Message.Contains("XTSE3430"))
        {
            return FormatStreamabilityReason(ex.Message);
        }
        catch
        {
            // Any other error in the probe stylesheet — skip (it may be due to missing
            // global variables/functions that the isolated probe can't see)
            return null;
        }
    }

    /// <summary>
    /// Extracts a clean, human-readable reason from an XTSE3430 exception message.
    /// </summary>
    private static string FormatStreamabilityReason(string message)
    {
        // Messages look like:
        // "XTSE3430: The body of this template in a streamable mode is not guaranteed streamable: Free-ranging axis '//other' ..."
        // Extract just the human part after the colon following XTSE3430.
        const string prefix = "XTSE3430: ";
        if (message.StartsWith(prefix, StringComparison.Ordinal))
        {
            var body = message[prefix.Length..];
            // The body itself has the explanation — return it as-is
            return body;
        }
        return message;
    }
}
