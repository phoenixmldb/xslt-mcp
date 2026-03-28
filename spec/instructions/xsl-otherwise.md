---
name: xsl:otherwise
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#xsl-choose
---

# xsl:otherwise

The default branch of an `xsl:choose` instruction. Its content is instantiated when no preceding `xsl:when` test evaluated to `true`.

## Attributes

None.

## Content Model

_sequence-constructor_

## Examples

```xml
<xsl:choose>
  <xsl:when test="@lang = 'en'">English</xsl:when>
  <xsl:when test="@lang = 'fr'">French</xsl:when>
  <xsl:otherwise>Unknown Language</xsl:otherwise>
</xsl:choose>
```

## See Also

- [xsl:choose](xsl-choose.md)
- [xsl:when](xsl-when.md)
