using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using ModelContextProtocol.Server;

namespace XsltMcpServer.Tools;

[McpServerToolType]
public static class UtilityTools
{
    [McpServerTool(Name = "xml_validate_schema"), Description(
        "Validate XML against an XSD schema. Returns validation errors or success.")]
    public static string ValidateSchema(
        [Description("XML document to validate")] string xml,
        [Description("XSD schema to validate against")] string xsd)
    {
        try
        {
            var schemas = new XmlSchemaSet();
            using var schemaReader = new StringReader(xsd);
            schemas.Add(XmlSchema.Read(schemaReader, null)!);
            schemas.Compile();

            var errors = new List<string>();
            var settings = new XmlReaderSettings
            {
                Schemas = schemas,
                ValidationType = ValidationType.Schema,
            };
            settings.ValidationEventHandler += (_, e) =>
            {
                var prefix = e.Severity == XmlSeverityType.Error ? "Error" : "Warning";
                errors.Add($"{prefix} at line {e.Exception.LineNumber}, col {e.Exception.LinePosition}: {e.Message}");
            };

            using var xmlReader = XmlReader.Create(new StringReader(xml), settings);
            while (xmlReader.Read()) { }

            return errors.Count == 0
                ? "Valid: XML conforms to the schema."
                : $"Validation found {errors.Count} issue(s):\n" + string.Join("\n", errors);
        }
        catch (Exception ex)
        {
            return $"Schema validation error: {ex.Message}";
        }
    }

    [McpServerTool(Name = "xml_format"), Description(
        "Pretty-print XML with indentation.")]
    public static string FormatXml(
        [Description("XML document to format")] string xml)
    {
        try
        {
            var doc = XDocument.Parse(xml);
            using var writer = new StringWriter();
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                OmitXmlDeclaration = doc.Declaration == null,
                NewLineOnAttributes = false,
            };
            using (var xmlWriter = XmlWriter.Create(writer, settings))
            {
                doc.WriteTo(xmlWriter);
            }
            return writer.ToString();
        }
        catch (Exception ex)
        {
            return $"Format error: {ex.Message}";
        }
    }
}
