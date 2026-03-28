---
name: Serialization
category: concept
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#serialization
---

# Serialization

Serialization is the process of converting the result tree (an XDM value) into a sequence of octets -- the actual output file. XSLT delegates serialization to the XSLT Serialization specification (a separate W3C document), but the stylesheet controls serialization through `xsl:output` declarations and per-result-document overrides. Understanding serialization is essential because it determines the exact bytes that appear in the output, including encoding, indentation, DOCTYPE declarations, and character escaping.

Serialization is distinct from result tree construction. A stylesheet constructs a result tree (or multiple result trees via xsl:result-document), and then the serializer converts each tree into a byte stream. Some XSLT use cases skip serialization entirely -- when the result is consumed by another process in the pipeline, only the XDM value matters.

## Serialization Methods

The `method` parameter specifies the output format. Each method has distinct behavior:

**xml** -- Produces well-formed XML. This is the default when the document element is not `html`. Supports all XML serialization parameters. Empty elements are serialized as self-closing tags (`<br/>`). Character references are used for characters not representable in the output encoding.

**html** -- Produces HTML-compatible output. Empty elements like `<br>`, `<hr>`, `<img>` are serialized without closing tags. Boolean attributes like `checked` are serialized without values. The `<script>` and `<style>` elements are not escaped. The `<meta>` charset declaration may be injected. This method targets HTML 4.x/5 browsers.

**xhtml** -- A hybrid of xml and html methods. Output is well-formed XML but follows XHTML conventions. Empty elements in the XHTML namespace use self-closing syntax. The method respects XHTML serialization rules for compatibility with HTML processors.

**text** -- Produces plain text output. Only text nodes in the result tree are output; all markup is discarded. Useful for generating source code, CSV, configuration files, or other non-XML formats.

**json** -- New in XSLT 3.0. Serializes XDM maps and arrays as JSON. The result must be a single map, array, or atomic value. Strings become JSON strings, numbers become JSON numbers, booleans become JSON booleans, and null maps to JSON null.

**adaptive** -- New in XSLT 3.0. A general-purpose method that can serialize any XDM value. Maps are output as `map{...}`, arrays as `[...]`, nodes as their XML serialization, and atomics as their string representation. Primarily useful for debugging and logging.

## Serialization Parameters

The full set of serialization parameters, specified via `xsl:output` attributes or xsl:result-document overrides:

- **method** -- Output method (xml, html, xhtml, text, json, adaptive, or a QName for implementation-defined methods).
- **version** -- Output version. For XML: "1.0" or "1.1". For HTML: "4.0", "5.0", etc.
- **encoding** -- Character encoding. Default is UTF-8. Common alternatives: UTF-16, ISO-8859-1, US-ASCII.
- **indent** -- Whether to add indentation whitespace ("yes" or "no"). Default depends on method (no for xml, yes for html).
- **omit-xml-declaration** -- Whether to suppress the XML declaration ("yes" or "no").
- **standalone** -- The standalone declaration in the XML prolog ("yes", "no", or "omit").
- **doctype-public** -- The public identifier for the DOCTYPE declaration.
- **doctype-system** -- The system identifier for the DOCTYPE declaration.
- **cdata-section-elements** -- Space-separated list of element names whose text content should be serialized as CDATA sections.
- **media-type** -- The MIME type of the output (e.g., "text/html", "application/xml").
- **byte-order-mark** -- Whether to output a byte order mark ("yes" or "no").
- **normalization-form** -- Unicode normalization form (NFC, NFD, NFKC, NFKD, or "none").
- **escape-uri-attributes** -- Whether to apply percent-encoding to URI attribute values in HTML output ("yes" or "no").
- **include-content-type** -- Whether to inject a `<meta>` content-type element in HTML output ("yes" or "no").
- **suppress-indentation** -- Elements within which indentation should be suppressed (XSLT 3.0).
- **use-character-maps** -- Names of character maps to apply during serialization.
- **item-separator** -- Separator between items in a sequence (XSLT 3.0, primarily for adaptive and text methods).
- **html-version** -- The HTML version for the html and xhtml methods (XSLT 3.0).
- **json-node-output-method** -- How nodes embedded in JSON should be serialized (XSLT 3.0).
- **build-tree** -- Whether to build a result tree or produce a raw sequence ("yes" or "no", XSLT 3.0).

## Character Maps

Character maps provide a mechanism to replace specific characters during serialization. They are defined with `xsl:character-map` and referenced from `xsl:output` or `xsl:result-document`:

```xml
<xsl:character-map name="disable-escaping">
  <xsl:output-character character="&#xa0;" string="&amp;nbsp;"/>
  <xsl:output-character character='"' string="&amp;quot;"/>
</xsl:character-map>

<xsl:output method="html" use-character-maps="disable-escaping"/>
```

Character maps are applied as the last step of serialization, after all other processing. They provide fine-grained control over the output bytes that is not achievable through other means.

## DOCTYPE Handling

The `doctype-public` and `doctype-system` parameters control the DOCTYPE declaration in the output:

```xml
<xsl:output method="html"
            doctype-system="about:legacy-compat"
            doctype-public=""/>
```

For HTML5, the minimal DOCTYPE `<!DOCTYPE html>` is produced by setting `html-version="5.0"` or by using `doctype-system="about:legacy-compat"`.

For XHTML, the full DOCTYPE with public and system identifiers can be specified. The serializer generates the `<!DOCTYPE ... >` declaration before the document element.

## Indentation

When `indent="yes"` is specified, the serializer adds whitespace between elements to make the output human-readable. The exact indentation algorithm is implementation-defined, but processors typically add newlines and spaces before child elements.

Indentation can introduce unwanted whitespace in mixed-content elements. The `suppress-indentation` parameter (XSLT 3.0) names elements within which indentation should not be added:

```xml
<xsl:output method="xml" indent="yes" suppress-indentation="pre code"/>
```

This is important for elements like `<pre>` or `<code>` where whitespace is significant.

## xsl:output vs. xsl:result-document

`xsl:output` declares the default serialization parameters for the principal output. `xsl:result-document` can override any of these parameters for secondary output documents:

```xml
<xsl:output method="html" indent="yes" encoding="UTF-8"/>

<!-- Principal output uses xsl:output settings -->
<xsl:template match="/">
  <html>...</html>

  <!-- Secondary output with different settings -->
  <xsl:result-document href="data.json" method="json">
    <xsl:sequence select="$json-data"/>
  </xsl:result-document>
</xsl:template>
```

Multiple `xsl:output` declarations can exist in a stylesheet; they are merged by attribute. If a conflict arises (same attribute with different values in declarations with the same import precedence), it is an error.

Named `xsl:output` declarations can be referenced by `xsl:result-document` via the `format` attribute, allowing multiple output configurations:

```xml
<xsl:output name="json-output" method="json" encoding="UTF-8"/>
<xsl:output name="html-output" method="html" indent="yes"/>

<xsl:result-document format="json-output" href="data.json">...</xsl:result-document>
```

## Encoding and Byte Order Marks

The `encoding` parameter determines the character encoding of the output. If a character in the result tree cannot be represented in the chosen encoding, the serializer must use a character reference (for XML/XHTML) or a numeric character reference (for HTML). For the text method, unrepresentable characters are an error.

The `byte-order-mark` parameter controls whether a BOM is written at the start of the output. BOMs are primarily relevant for UTF-16 and UTF-8 encodings. Setting `byte-order-mark="yes"` forces a BOM; setting it to `"no"` suppresses it.

## Examples

```xml
<!-- Standard HTML5 output -->
<xsl:output method="html" html-version="5.0" indent="yes" encoding="UTF-8"/>

<!-- XML with DOCTYPE -->
<xsl:output method="xml" version="1.0" encoding="UTF-8"
            indent="yes"
            doctype-public="-//OASIS//DTD DocBook XML V5.0//EN"
            doctype-system="http://www.oasis-open.org/docbook/xml/5.0/dtd/docbook.dtd"/>

<!-- JSON output -->
<xsl:output name="json" method="json" encoding="UTF-8"/>

<!-- Text output for CSV generation -->
<xsl:output method="text" encoding="UTF-8"/>

<!-- Multiple result documents -->
<xsl:template match="/">
  <xsl:result-document href="index.html" method="html" indent="yes">
    <html><body><xsl:apply-templates/></body></html>
  </xsl:result-document>
  <xsl:result-document href="data.xml" method="xml" indent="yes">
    <data><xsl:copy-of select="//record"/></data>
  </xsl:result-document>
</xsl:template>
```

## See Also

- [xsl:output](../instructions/xsl-output.md)
- [xsl:result-document](../instructions/xsl-result-document.md)
- [xsl:character-map](../instructions/xsl-character-map.md)
