---
name: xsl:output
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#output
---

# xsl:output

Top-level declaration that defines serialization properties for the output. Multiple `xsl:output` declarations may exist; their properties are merged, with import precedence determining priority.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | No | `eqname` | Named output definition for use with `xsl:result-document`. |
| `method` | No | `"xml" \| "html" \| "xhtml" \| "text" \| "json" \| "adaptive" \| eqname` | Output method. |
| `version` | No | `{ nmtoken }` | Version of the output method (e.g., `1.0`, `1.1` for XML). |
| `html-version` | No | `{ xs:decimal }` | HTML version (e.g., `5`). (Since 3.0) |
| `encoding` | No | `{ string }` | Character encoding (e.g., `UTF-8`). |
| `indent` | No | `{ "yes" \| "no" }` | Whether to indent the output. |
| `suppress-indentation` | No | `{ eqnames }` | Elements whose content should not be indented. (Since 3.0) |
| `omit-xml-declaration` | No | `{ "yes" \| "no" }` | Whether to omit the XML declaration. |
| `standalone` | No | `{ "yes" \| "no" \| "omit" }` | Value of `standalone` in the XML declaration. |
| `doctype-public` | No | `{ string }` | Public identifier for the DOCTYPE. |
| `doctype-system` | No | `{ string }` | System identifier for the DOCTYPE. |
| `cdata-section-elements` | No | `{ eqnames }` | Elements whose text content is output as CDATA sections. |
| `media-type` | No | `{ string }` | MIME type of the output. |
| `byte-order-mark` | No | `{ "yes" \| "no" }` | Whether to emit a byte order mark. |
| `normalization-form` | No | `{ "NFC" \| "NFD" \| "NFKC" \| "NFKD" \| "fully-normalized" \| "none" }` | Unicode normalization form. |
| `use-character-maps` | No | `eqnames` | Character maps to apply during serialization. |
| `item-separator` | No | `{ string }` | Separator between top-level items. (Since 3.0) |
| `build-tree` | No | `{ "yes" \| "no" }` | Whether to build a tree before serializing. (Since 3.0) |

## Content Model

Empty.

## Examples

### HTML5 Output

```xml
<xsl:output method="html" html-version="5" encoding="UTF-8" indent="yes"/>
```

### XML Output

```xml
<xsl:output method="xml" version="1.0" encoding="UTF-8"
    indent="yes" omit-xml-declaration="no"/>
```

### JSON Output

```xml
<xsl:output method="json" encoding="UTF-8" indent="yes"/>
```

### Named Output

```xml
<xsl:output name="text-output" method="text" encoding="UTF-8"/>

<xsl:template match="/">
  <xsl:result-document format="text-output" href="output.txt">
    <xsl:value-of select="data"/>
  </xsl:result-document>
</xsl:template>
```

## Error Codes

- **XTSE1460**: Two `xsl:output` declarations with the same import precedence specify conflicting values for the same attribute.
- **XTSE1570**: The `method` value is not one of the recognized output methods.

## See Also

- [xsl:result-document](xsl-result-document.md)
- [Output Method: xml](../output-methods/xml.md)
- [Output Method: html](../output-methods/html.md)
- [Output Method: json](../output-methods/json.md)
