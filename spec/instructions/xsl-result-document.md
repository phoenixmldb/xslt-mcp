---
name: xsl:result-document
category: instruction
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#result-document
---

# xsl:result-document

Creates a secondary result tree, typically written to a separate output file. The principal output is produced by the initial template or match on `/`; additional outputs use `xsl:result-document`.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `href` | No | `{ uri }` | URI of the output destination. If omitted, replaces the principal output. |
| `format` | No | `{ eqname }` | Name of an `xsl:output` declaration to use. |
| `method` | No | `{ "xml" \| "html" \| "xhtml" \| "text" \| "json" \| "adaptive" \| eqname }` | Output method (overrides format). |
| `version` | No | `{ nmtoken }` | Output version. |
| `html-version` | No | `{ xs:decimal }` | HTML version. |
| `encoding` | No | `{ string }` | Character encoding. |
| `indent` | No | `{ "yes" \| "no" }` | Whether to indent output. |
| `omit-xml-declaration` | No | `{ "yes" \| "no" }` | Whether to omit the XML declaration. |
| `standalone` | No | `{ "yes" \| "no" \| "omit" }` | Standalone attribute in XML declaration. |
| `doctype-public` | No | `{ string }` | Public identifier for DOCTYPE. |
| `doctype-system` | No | `{ string }` | System identifier for DOCTYPE. |
| `cdata-section-elements` | No | `{ eqnames }` | Elements whose text content is output as CDATA. |
| `byte-order-mark` | No | `{ "yes" \| "no" }` | Whether to emit a BOM. |
| `output-version` | No | `{ nmtoken }` | Serialization specification version. |
| `type` | No | `eqname` | Schema type for validation. |
| `validation` | No | `"strict" \| "lax" \| "preserve" \| "strip"` | Validation mode. |
| `build-tree` | No | `{ "yes" \| "no" }` | Whether to build a tree before serializing. (Since 3.0) |

## Content Model

_sequence-constructor_

## Examples

### Multiple Output Files

```xml
<xsl:template match="chapter">
  <xsl:result-document href="chapter-{@id}.html" method="html" html-version="5">
    <html>
      <head><title><xsl:value-of select="title"/></title></head>
      <body>
        <xsl:apply-templates/>
      </body>
    </html>
  </xsl:result-document>
</xsl:template>
```

### JSON Output

```xml
<xsl:result-document href="data.json" method="json">
  <xsl:map>
    <xsl:map-entry key="'name'" select="$name"/>
    <xsl:map-entry key="'count'" select="$count"/>
  </xsl:map>
</xsl:result-document>
```

## Error Codes

- **XTDE1400**: Two result documents have the same effective URI.
- **XTDE1420**: The URI is not valid.
- **XTDE1430**: The result document cannot be written (e.g., permission denied).
- **XTDE1480**: The `format` attribute references a non-existent named output.

## See Also

- [xsl:output](xsl-output.md)
