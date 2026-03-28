---
name: Accumulators
category: concept
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#accumulators
---

# Accumulators

Accumulators provide a way to compute running values over a document as it is traversed. They are particularly useful in streaming mode where variables cannot reference previous siblings or maintain state across template invocations.

## Declaration

```xml
<xsl:accumulator name="word-count"
    as="xs:integer"
    initial-value="0"
    streamable="yes">

  <xsl:accumulator-rule match="text()"
      select="$value + count(tokenize(.))"/>

</xsl:accumulator>
```

## Attributes of xsl:accumulator

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | Yes | `eqname` | The accumulator name. |
| `as` | No | `sequence-type` | Type of the accumulated value. Default is `item()*`. |
| `initial-value` | Yes | `expression` | Starting value before the document is processed. |
| `streamable` | No | `"yes" \| "no"` | Whether the accumulator is streamable. Default is `no`. |
| `visibility` | No | `"public" \| "private" \| "final"` | Visibility within a package. |

## Accumulator Rules

Each `xsl:accumulator-rule` specifies:
- `match` — pattern identifying when the accumulator updates
- `select` — expression computing the new value (can reference `$value` for the current accumulated value)
- `phase` — `"start"` or `"end"` — whether the rule fires when entering or leaving the matched node. Default is `end`.

## Accessing Accumulator Values

```xml
<!-- Value of accumulator before processing the current node -->
accumulator-before('word-count')

<!-- Value of accumulator after processing the current node -->
accumulator-after('word-count')
```

## Examples

### Line Counter

```xml
<xsl:accumulator name="line-number" as="xs:integer" initial-value="0">
  <xsl:accumulator-rule match="line" select="$value + 1"/>
</xsl:accumulator>

<xsl:template match="line">
  <span class="line-no"><xsl:value-of select="accumulator-before('line-number') + 1"/></span>
  <xsl:value-of select="."/>
</xsl:template>
```

### Running Total

```xml
<xsl:accumulator name="running-total" as="xs:decimal" initial-value="0">
  <xsl:accumulator-rule match="transaction"
      select="$value + xs:decimal(@amount)"/>
</xsl:accumulator>
```

## See Also

- [Streaming](streaming.md)
- [xsl:iterate](../instructions/xsl-iterate.md)
