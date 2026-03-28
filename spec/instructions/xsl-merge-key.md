---
name: xsl:merge-key
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#merge-key
---

# xsl:merge-key

Defines a sort key for ordering items within an `xsl:merge-source`.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | Yes | `expression` | Expression computing the key value, evaluated with each item as context. |
| `order` | No | `"ascending" \| "descending"` | Sort direction. Default is `ascending`. |
| `collation` | No | `uri` | Collation URI for string comparisons. |
| `data-type` | No | `"text" \| "number" \| EQName` | How to interpret key values for comparison. Default is inferred from type. |
| `lang` | No | `language` | Language for collation. |

## Content Model

Empty.

## Description

`xsl:merge-key` appears as a child of `xsl:merge-source` and specifies how items from that source are ordered. The attributes mirror those of `xsl:sort`, defining the key expression, sort direction, and comparison semantics.

Multiple `xsl:merge-key` elements define a composite sort key: items are first ordered by the first key, then by the second key for items with equal first keys, and so on.

The `select` expression is evaluated with each item in the merge source as the context item. The resulting values must be comparable using the specified ordering.

When `sort-before-merge="no"` on the parent `xsl:merge-source`, the merge keys describe the expected order of the input (the processor verifies this). When `sort-before-merge="yes"`, the keys are used to actually sort the input before merging.

## Examples

### Date and ID Composite Key

```xml
<xsl:merge-source select="//record">
  <xsl:merge-key select="@date" order="ascending"/>
  <xsl:merge-key select="xs:integer(@id)" order="ascending"/>
</xsl:merge-source>
```

### Case-Insensitive String Key

```xml
<xsl:merge-source select="//entry">
  <xsl:merge-key select="@name" order="ascending"
                 collation="http://www.w3.org/2005/xpath-functions/collation/html-ascii-case-insensitive"/>
</xsl:merge-source>
```

## Error Codes

- **XTSE0010**: `xsl:merge-key` appears outside of `xsl:merge-source`.

## See Also

- [xsl:merge-source](xsl-merge-source.md)
- [xsl:merge](xsl-merge.md)
- [xsl:sort](xsl-sort.md)
