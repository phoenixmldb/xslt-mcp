---
name: xsl:choose
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#xsl-choose
---

# xsl:choose

Provides multi-way conditional processing. Evaluates `xsl:when` branches in order and processes the first whose `test` is true. An optional `xsl:otherwise` handles the case when no `xsl:when` is satisfied.

## Attributes

None.

## Content Model

( `xsl:when`+, `xsl:otherwise`? )

At least one `xsl:when` is required. At most one `xsl:otherwise` is allowed, and it must be last.

## Examples

```xml
<xsl:choose>
  <xsl:when test="@type = 'error'">
    <span class="error"><xsl:value-of select="."/></span>
  </xsl:when>
  <xsl:when test="@type = 'warning'">
    <span class="warning"><xsl:value-of select="."/></span>
  </xsl:when>
  <xsl:otherwise>
    <span class="info"><xsl:value-of select="."/></span>
  </xsl:otherwise>
</xsl:choose>
```

## Error Codes

- **XTSE0010**: No `xsl:when` child element is present.
- **XTSE0010**: `xsl:otherwise` is not the last child.

## See Also

- [xsl:when](xsl-when.md)
- [xsl:otherwise](xsl-otherwise.md)
- [xsl:if](xsl-if.md)
