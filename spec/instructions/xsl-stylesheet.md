---
name: xsl:stylesheet
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#stylesheet-element
---

# xsl:stylesheet

The `xsl:stylesheet` element is the outermost element of a stylesheet module. It declares the stylesheet version, namespaces, and contains all top-level declarations. `xsl:transform` is a synonym with identical semantics.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `version` | Yes | `xs:decimal` | The XSLT version (e.g., `3.0`). Required on the outermost element. |
| `id` | No | `xs:ID` | An identifier for the stylesheet. |
| `default-mode` | No | `eqname | "#unnamed"` | The default mode for `xsl:template` and `xsl:apply-templates`. Default is `#unnamed`. (Since 3.0) |
| `default-validation` | No | `"preserve" | "strip"` | Default validation mode for instructions that create new nodes. Default is `strip`. |
| `default-collation` | No | `uri-list` | Space-separated list of collation URIs. The first recognized URI is used as the default collation. |
| `extension-element-prefixes` | No | `prefixes` | Space-separated list of namespace prefixes for extension instructions. |
| `exclude-result-prefixes` | No | `prefixes | "#all"` | Namespace prefixes to exclude from the result tree. |
| `expand-text` | No | `xs:boolean` | Whether text value templates are enabled. Default is `no`. (Since 3.0) |
| `use-when` | No | `expression` | Static expression controlling conditional compilation. |
| `xpath-default-namespace` | No | `xs:anyURI` | Default namespace for unprefixed element and type names in XPath expressions. |
| `input-type-annotations` | No | `"preserve" | "strip" | "unspecified"` | How type annotations on input documents are handled. |

## Content Model

( `xsl:import`*, _top-level-declarations_ )

Top-level declarations include: `xsl:template`, `xsl:variable`, `xsl:param`, `xsl:function`, `xsl:output`, `xsl:key`, `xsl:decimal-format`, `xsl:attribute-set`, `xsl:namespace-alias`, `xsl:strip-space`, `xsl:preserve-space`, `xsl:include`, `xsl:character-map`, `xsl:accumulator`, `xsl:mode`, `xsl:use-package`, and user-defined data elements.

## Examples

```xml
<xsl:stylesheet version="3.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:xs="http://www.w3.org/2001/XMLSchema"
    expand-text="yes"
    default-mode="main">

  <xsl:output method="html" html-version="5"/>

  <xsl:template match="/" mode="main">
    <html>
      <body>{.}</body>
    </html>
  </xsl:template>

</xsl:stylesheet>
```

## Error Codes

- **XTSE0010**: A top-level element is not permitted in this position.
- **XTSE0020**: The `version` attribute is required on `xsl:stylesheet`.
- **XTSE0090**: An `xsl:import` element must precede all other children of `xsl:stylesheet`.

## See Also

- [xsl:transform](xsl-transform.md) (synonym)
- [xsl:import](xsl-import.md)
- [xsl:include](xsl-include.md)
- [xsl:output](xsl-output.md)
