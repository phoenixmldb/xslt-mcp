---
name: xsl:for-each
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#for-each
---

# xsl:for-each

Iterates over a sequence, evaluating its body once per item. Within the body, the context item is set to the current item, and `position()` and `last()` reflect the iteration.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | Yes | `expression` | XPath expression returning the sequence to iterate over. |

## Content Model

( `xsl:sort`*, _sequence-constructor_ )

## Examples

### Basic Iteration

```xml
<ul>
  <xsl:for-each select="items/item">
    <li><xsl:value-of select="."/></li>
  </xsl:for-each>
</ul>
```

### With Sort

```xml
<xsl:for-each select="employees/employee">
  <xsl:sort select="last-name"/>
  <xsl:sort select="first-name"/>
  <p><xsl:value-of select="last-name"/>, <xsl:value-of select="first-name"/></p>
</xsl:for-each>
```

## Error Codes

- **XTSE0010**: The `select` attribute is required.

## See Also

- [xsl:for-each-group](xsl-for-each-group.md)
- [xsl:sort](xsl-sort.md)
- [xsl:iterate](xsl-iterate.md)
- [xsl:apply-templates](xsl-apply-templates.md)
