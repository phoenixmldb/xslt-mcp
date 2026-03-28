---
name: xsl:accumulator-rule
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#accumulator-rules
---

# xsl:accumulator-rule

Defines a rule within an accumulator that updates the accumulated value when a matching node is encountered.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `match` | Yes | `pattern` | Pattern identifying which nodes trigger this rule. |
| `phase` | No | `"start" \| "end"` | Whether the rule fires on the opening tag (`start`) or closing tag (`end`). Default is `end`. |
| `select` | No | `expression` | Expression computing the new accumulated value. The variable `$value` refers to the current accumulated value. |
| `new-value` | No | `expression` | Synonym for `select` (alternative spelling). |

## Content Model

If `select` is absent, the content is a sequence constructor that computes the new value. The variable `$value` is available within the body.

## Description

Each accumulator rule specifies a pattern and a computation. When a node matching the pattern is encountered during document traversal, the rule fires and computes a new accumulated value from the current value (available as `$value`) and the context node.

The `phase` attribute is significant for element nodes. A rule with `phase="start"` fires when the start tag of a matching element is encountered; `phase="end"` fires after all children have been processed. For non-element nodes, the phase distinction is irrelevant.

When multiple rules match the same node at the same phase, it is a static error unless they have different import precedence, in which case the highest-precedence rule wins.

## Examples

### Counting Sections by Level

```xml
<xsl:accumulator name="section-depth" as="xs:integer" initial-value="0">
  <xsl:accumulator-rule match="section" phase="start"
                        select="$value + 1"/>
  <xsl:accumulator-rule match="section" phase="end"
                        select="$value - 1"/>
</xsl:accumulator>
```

### Tracking Last Seen ID

```xml
<xsl:accumulator name="last-id" as="xs:string" initial-value="''">
  <xsl:accumulator-rule match="*[@id]" select="string(@id)"/>
</xsl:accumulator>
```

## Error Codes

- **XTSE3350**: The rule does not satisfy streaming constraints when the accumulator is declared streamable.
- **XTSE3360**: Two rules match the same node at the same phase with the same import precedence.

## See Also

- [xsl:accumulator](xsl-accumulator.md)
