---
name: fn:last
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-last
---

# fn:last

Returns the number of items in the sequence being processed (the context size).

## Signature

```
fn:last() as xs:integer
```

## Parameters

None.

## Returns

The context size as a positive integer.

## Examples

```xml
<xsl:for-each select="item">
  <xsl:if test="position() = last()">
    <last-item><xsl:value-of select="."/></last-item>
  </xsl:if>
</xsl:for-each>

<!-- Select the last element -->
<xsl:value-of select="items/item[last()]"/>
```

## Error Codes

- **XPDY0002**: The context item is absent.

## See Also

- [fn:position](fn-position.md)
- [fn:count](../numeric/fn-count.md)
