---
name: xsl:for-each-group
category: instruction
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#xsl-for-each-group
---

# xsl:for-each-group

Groups the items in a population and processes each group. One of four grouping attributes must be specified to determine how groups are formed.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | Yes | `expression` | Expression returning the population to group. |
| `group-by` | No* | `expression` | Grouping key expression. Items with the same key value are in the same group. |
| `group-adjacent` | No* | `expression` | Groups adjacent items with the same key value. |
| `group-starting-with` | No* | `pattern` | Starts a new group when an item matches this pattern. |
| `group-ending-with` | No* | `pattern` | Ends the current group when an item matches this pattern. |
| `composite` | No | `"yes" | "no"` | Whether the grouping key is composite (multi-part). Default is `no`. (Since 3.0) |
| `collation` | No | `{ uri }` | Collation used for string comparison of grouping keys. |

\* Exactly one of `group-by`, `group-adjacent`, `group-starting-with`, or `group-ending-with` must be specified.

## Content Model

( `xsl:sort`*, _sequence-constructor_ )

Within the body, `current-group()` returns the items in the current group and `current-grouping-key()` returns the grouping key.

## Examples

### Group By

```xml
<xsl:for-each-group select="city" group-by="@country">
  <xsl:sort select="current-grouping-key()"/>
  <div>
    <h2><xsl:value-of select="current-grouping-key()"/></h2>
    <ul>
      <xsl:for-each select="current-group()">
        <li><xsl:value-of select="@name"/></li>
      </xsl:for-each>
    </ul>
  </div>
</xsl:for-each-group>
```

### Group Adjacent

```xml
<!-- Groups consecutive paragraphs with the same @style -->
<xsl:for-each-group select="*" group-adjacent="@style">
  <div class="{current-grouping-key()}">
    <xsl:apply-templates select="current-group()"/>
  </div>
</xsl:for-each-group>
```

### Group Starting With

```xml
<!-- Groups elements starting with each h2 -->
<xsl:for-each-group select="*" group-starting-with="h2">
  <section>
    <xsl:apply-templates select="current-group()"/>
  </section>
</xsl:for-each-group>
```

### Composite Grouping Key (3.0)

```xml
<xsl:for-each-group select="record" group-by="year, month" composite="yes">
  <group year="{current-grouping-key()[1]}" month="{current-grouping-key()[2]}">
    <xsl:copy-of select="current-group()"/>
  </group>
</xsl:for-each-group>
```

## Error Codes

- **XTSE1080**: None of the four grouping attributes is present, or more than one is present.
- **XTSE1090**: `composite="yes"` is specified with `group-starting-with` or `group-ending-with`.
- **XTTE1100**: The grouping key for `group-adjacent` is not a single atomic value (unless `composite="yes"`).

## See Also

- [xsl:for-each](xsl-for-each.md)
- [xsl:sort](xsl-sort.md)
