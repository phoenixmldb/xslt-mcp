---
name: fn:position
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-position
---

# fn:position

Returns the position of the context item within the sequence being processed. Positions are 1-based.

## Signature

```
fn:position() as xs:integer
```

## Parameters

None.

## Returns

The context position (1-based integer).

## Examples

```xml
<xsl:for-each select="item">
  <xsl:if test="position() = 1">
    <first><xsl:value-of select="."/></first>
  </xsl:if>
</xsl:for-each>

<!-- In a predicate -->
<xsl:value-of select="items/item[position() le 3]"/>
```

## Error Codes

- **XPDY0002**: The context item is absent.

## See Also

- [fn:last](fn-last.md)
