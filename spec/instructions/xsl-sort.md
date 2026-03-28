---
name: xsl:sort
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#sort
---

# xsl:sort

Defines a sort key within `xsl:apply-templates`, `xsl:for-each`, `xsl:for-each-group`, or `xsl:perform-sort`. Multiple `xsl:sort` elements define major-to-minor sort keys.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | No | `expression` | Expression computing the sort key. Default is `.` (context item as string). |
| `lang` | No | `{ xs:language }` | Language used for collation. |
| `order` | No | `{ "ascending" \| "descending" }` | Sort direction. Default is `ascending`. |
| `collation` | No | `{ uri }` | Collation URI for string comparison. |
| `stable` | No | `{ "yes" \| "no" }` | Whether the sort is stable. Default is `yes`. |
| `case-order` | No | `{ "upper-first" \| "lower-first" }` | Whether uppercase or lowercase letters sort first. |
| `data-type` | No | `{ "text" \| "number" \| eqname }` | Data type for comparison. Default is `text`. |

## Content Model

When `select` is absent, the content is a sequence constructor providing the sort key value. When `select` is present, the element must be empty.

## Examples

### Simple Sort

```xml
<xsl:apply-templates select="item">
  <xsl:sort select="@name"/>
</xsl:apply-templates>
```

### Multi-Key Sort

```xml
<xsl:for-each select="employee">
  <xsl:sort select="department"/>
  <xsl:sort select="salary" data-type="number" order="descending"/>
  <xsl:value-of select="name"/>
</xsl:for-each>
```

## Error Codes

- **XTDE0930**: The sort key value cannot be compared (e.g., incompatible types).
- **XTDE0950**: The `lang` value is not recognized.

## See Also

- [xsl:apply-templates](xsl-apply-templates.md)
- [xsl:for-each](xsl-for-each.md)
- [xsl:for-each-group](xsl-for-each-group.md)
