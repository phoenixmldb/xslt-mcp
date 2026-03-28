---
name: xsl:accumulator
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#accumulator
---

# xsl:accumulator

Declares an accumulator for computing running totals or values during document traversal.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | Yes | `EQName` | The name of the accumulator. |
| `as` | No | `sequence-type` | The type of the accumulated value. Default is `item()*`. |
| `initial-value` | Yes | `expression` | Expression that produces the initial value before any rules fire. |
| `streamable` | No | `"yes" \| "no"` | Whether the accumulator is streamable. Default is `no`. |

## Content Model

( `xsl:accumulator-rule`+ )

One or more `xsl:accumulator-rule` elements that define how the accumulated value changes as the document is traversed.

## Description

Accumulators provide a mechanism for computing values that depend on the order of nodes in a document, such as running totals, counters, or positional state. They are evaluated lazily: rules fire as the processor encounters matching nodes during document traversal.

The accumulated value is accessible via the `accumulator-before()` and `accumulator-after()` functions. `accumulator-before(name)` returns the value before a given node is processed, and `accumulator-after(name)` returns the value after the node and all its descendants have been processed.

Accumulators are particularly useful in streaming mode, where they provide a way to maintain state across nodes without holding the entire document in memory. When `streamable="yes"`, the accumulator rules must conform to streaming constraints.

An accumulator is associated with a document. To apply an accumulator to a document, the stylesheet must declare it in the `use-accumulators` attribute of `xsl:source-document`, or it must be declared as universally applicable.

## Examples

### Chapter Numbering

```xml
<xsl:accumulator name="chapter-count" as="xs:integer" initial-value="0">
  <xsl:accumulator-rule match="chapter" select="$value + 1"/>
</xsl:accumulator>

<xsl:template match="chapter">
  <h1>Chapter <xsl:value-of select="accumulator-before('chapter-count')"/></h1>
  <xsl:apply-templates/>
</xsl:template>
```

### Running Total in Streaming Mode

```xml
<xsl:accumulator name="running-total" as="xs:decimal"
                 initial-value="0" streamable="yes">
  <xsl:accumulator-rule match="transaction" phase="end"
                        select="$value + xs:decimal(@amount)"/>
</xsl:accumulator>

<xsl:mode streamable="yes"/>

<xsl:template match="ledger">
  <xsl:source-document href="transactions.xml" streamable="yes"
                       use-accumulators="running-total">
    <xsl:apply-templates select="transaction[last()]"/>
  </xsl:source-document>
</xsl:template>
```

## Error Codes

- **XTSE3340**: Two accumulators in the same package have the same name.
- **XTSE3350**: An accumulator rule does not satisfy streaming constraints when `streamable="yes"`.

## See Also

- [xsl:accumulator-rule](xsl-accumulator-rule.md)
- [xsl:source-document](xsl-source-document.md)
