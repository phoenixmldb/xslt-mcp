---
name: xsl:perform-sort
category: instruction
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#perform-sort
---

# xsl:perform-sort

Sorts a sequence of items.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | No | `expression` | Expression producing the sequence to sort. |

## Content Model

( `xsl:sort`+, _sequence-constructor_? )

One or more `xsl:sort` elements define the sort keys. If `select` is absent, a sequence constructor follows the sort keys.

## Description

`xsl:perform-sort` sorts a sequence according to one or more sort keys. It provides sorting as a standalone operation, unlike `xsl:for-each` or `xsl:apply-templates` where sorting is combined with iteration.

The input sequence comes from either the `select` attribute or the body sequence constructor (after the `xsl:sort` elements). The sort keys are defined by `xsl:sort` children, which specify the key expression, direction, collation, and data type.

This instruction is particularly useful for sorting a sequence before storing it in a variable, passing it to a function, or using it with `xsl:iterate` (which does not support inline sort keys).

The sorted result preserves all items; only their order changes. The sort is stable: items with equal keys maintain their relative order from the input.

## Examples

### Sorting into a Variable

```xml
<xsl:variable name="sorted-items" as="element()*">
  <xsl:perform-sort select="//item">
    <xsl:sort select="@price" data-type="number" order="descending"/>
    <xsl:sort select="@name" order="ascending"/>
  </xsl:perform-sort>
</xsl:variable>
```

### Sorting Constructed Nodes

```xml
<xsl:variable name="results" as="element()*">
  <xsl:perform-sort>
    <xsl:sort select="@score" data-type="number" order="descending"/>
    <xsl:for-each select="$candidates">
      <result name="{@name}" score="{my:compute-score(.)}"/>
    </xsl:for-each>
  </xsl:perform-sort>
</xsl:variable>
```

### Sorting for xsl:iterate

```xml
<xsl:iterate select="$sorted-items">
  <xsl:param name="running-total" as="xs:decimal" select="0"/>
  <!-- xsl:iterate doesn't support xsl:sort, so pre-sort with xsl:perform-sort -->
</xsl:iterate>
```

## Error Codes

- **XTSE0010**: No `xsl:sort` children are present.

## See Also

- [xsl:sort](xsl-sort.md)
- [xsl:for-each](xsl-for-each.md)
