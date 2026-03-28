---
name: xsl:import
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#import
---

# xsl:import

Imports another stylesheet module. Declarations in the importing module have higher import precedence than those in the imported module. All `xsl:import` elements must precede all other top-level children of `xsl:stylesheet`.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `href` | Yes | `xs:anyURI` | URI of the stylesheet module to import. |

## Content Model

Empty.

## Examples

```xml
<xsl:stylesheet version="3.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:import href="common-templates.xsl"/>
  <xsl:import href="formatting.xsl"/>

  <!-- Declarations here override imported ones -->
  <xsl:template match="title">
    <h1><xsl:apply-templates/></h1>
  </xsl:template>

</xsl:stylesheet>
```

## Error Codes

- **XTSE0090**: An `xsl:import` element is not at the top, before all other children of `xsl:stylesheet`.
- **XTSE0160**: A stylesheet directly or indirectly imports itself (circular import).
- **XTSE0165**: The imported URI cannot be resolved.

## See Also

- [xsl:include](xsl-include.md)
- [xsl:stylesheet](xsl-stylesheet.md)
- [Concepts: Packages](../concepts/packages.md)
