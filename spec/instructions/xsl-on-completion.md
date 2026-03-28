---
name: xsl:on-completion
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#on-completion
---

# xsl:on-completion

Executed when `xsl:iterate` completes normally (all items processed without `xsl:break`).

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | No | `expression` | Expression producing the completion result. |

## Content Model

Sequence constructor (used if `select` is absent).

## Description

`xsl:on-completion` appears as the last child of `xsl:iterate` and provides a template that is evaluated after all items in the iteration sequence have been processed. The final parameter values are available within its body, making it the natural place to output summary information.

If `xsl:break` terminates the iteration early, `xsl:on-completion` is NOT evaluated. This distinction allows different behavior for normal completion versus early termination.

The result of `xsl:on-completion` is appended to the results produced by the iteration body. The parameter variables declared on `xsl:iterate` are accessible with their final values.

If no `xsl:on-completion` is present and the iteration completes normally, no additional output is produced after the last iteration.

## Examples

### Summary After Iteration

```xml
<xsl:iterate select="//record">
  <xsl:param name="count" as="xs:integer" select="0"/>
  <xsl:param name="total" as="xs:decimal" select="0"/>

  <row><xsl:value-of select="@value"/></row>

  <xsl:next-iteration>
    <xsl:with-param name="count" select="$count + 1"/>
    <xsl:with-param name="total" select="$total + @value"/>
  </xsl:next-iteration>

  <xsl:on-completion>
    <footer>
      <total><xsl:value-of select="$total"/></total>
      <average><xsl:value-of select="if ($count > 0) then $total div $count else 0"/></average>
    </footer>
  </xsl:on-completion>
</xsl:iterate>
```

### Empty Sequence Handling

```xml
<xsl:iterate select="$items">
  <xsl:param name="found" as="xs:boolean" select="false()"/>
  <!-- processing -->
  <xsl:on-completion>
    <xsl:if test="not($found)">
      <no-results/>
    </xsl:if>
  </xsl:on-completion>
</xsl:iterate>
```

## Error Codes

- **XTSE0010**: `xsl:on-completion` appears outside of `xsl:iterate`.
- **XTSE3120**: `xsl:on-completion` is not the last child of `xsl:iterate`.

## See Also

- [xsl:iterate](xsl-iterate.md)
- [xsl:break](xsl-break.md)
- [xsl:next-iteration](xsl-next-iteration.md)
