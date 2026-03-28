---
name: xsl:when
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#xsl-choose
---

# xsl:when

A conditional branch within `xsl:choose`. Its content is instantiated if the `test` expression evaluates to `true` and no preceding `xsl:when` was satisfied.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `test` | Yes | `expression` | XPath expression evaluated as a boolean. |

## Content Model

_sequence-constructor_

## Examples

```xml
<xsl:choose>
  <xsl:when test="position() = 1">
    <li class="first"><xsl:value-of select="."/></li>
  </xsl:when>
  <xsl:when test="position() = last()">
    <li class="last"><xsl:value-of select="."/></li>
  </xsl:when>
  <xsl:otherwise>
    <li><xsl:value-of select="."/></li>
  </xsl:otherwise>
</xsl:choose>
```

## Error Codes

- **XTSE0010**: The `test` attribute is required.

## See Also

- [xsl:choose](xsl-choose.md)
- [xsl:otherwise](xsl-otherwise.md)
