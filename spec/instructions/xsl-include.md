---
name: xsl:include
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#include
---

# xsl:include

Includes another stylesheet module. Unlike `xsl:import`, included declarations have the same import precedence as declarations in the including module. Can appear anywhere among top-level elements (not restricted to appearing first).

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `href` | Yes | `xs:anyURI` | URI of the stylesheet module to include. |

## Content Model

Empty.

## Examples

```xml
<xsl:stylesheet version="3.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:include href="utility-functions.xsl"/>

  <xsl:template match="/">
    <xsl:value-of select="my:util-function(.)"/>
  </xsl:template>

</xsl:stylesheet>
```

## Error Codes

- **XTSE0170**: A stylesheet directly or indirectly includes itself (circular inclusion).
- **XTSE0165**: The included URI cannot be resolved.

## See Also

- [xsl:import](xsl-import.md)
- [xsl:stylesheet](xsl-stylesheet.md)
