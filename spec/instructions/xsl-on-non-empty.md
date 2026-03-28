---
name: xsl:on-non-empty
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#on-non-empty
---

# xsl:on-non-empty

Provides content that is included only when the enclosing sequence constructor produces a non-empty result.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | No | `expression` | Expression producing the conditional content. |

## Content Model

Sequence constructor (used if `select` is absent).

## Description

`xsl:on-non-empty` is evaluated only if the other instructions in the same sequence constructor produce some output. It must appear as the first or last instruction in its containing sequence constructor.

This is the complement of `xsl:on-empty`. It is useful for adding headers, footers, wrappers, or separators that should only appear when there is actual content. Without this instruction, you would need to evaluate the content twice (once to check if it is empty, once to produce it) or use complex conditional logic.

## Examples

### Conditional Header

```xml
<xsl:variable name="content">
  <xsl:on-non-empty>
    <h2>Results</h2>
  </xsl:on-non-empty>
  <xsl:for-each select="results/item">
    <p><xsl:value-of select="."/></p>
  </xsl:for-each>
</xsl:variable>
```

### Conditional Separator

```xml
<xsl:for-each select="section">
  <xsl:apply-templates/>
  <xsl:on-non-empty select="'&#10;---&#10;'"/>
</xsl:for-each>
```

## Error Codes

- **XTSE0010**: `xsl:on-non-empty` is not at the start or end of the sequence constructor.

## See Also

- [xsl:on-empty](xsl-on-empty.md)
- [xsl:where-populated](xsl-where-populated.md)
