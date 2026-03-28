---
name: xsl:next-iteration
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#next-iteration
---

# xsl:next-iteration

Continues to the next iteration in `xsl:iterate`, passing updated parameter values.

## Attributes

None.

## Content Model

( `xsl:with-param`* )

## Description

`xsl:next-iteration` appears within the body of `xsl:iterate` and signals that the current iteration is complete, passing updated parameter values to the next iteration. It is conceptually equivalent to the "continue" statement in imperative languages combined with updating loop variables.

Each `xsl:with-param` child provides the new value for a parameter declared on the `xsl:iterate`. All parameters must be supplied unless they have default values. The parameter values are evaluated in the context of the current iteration before advancing to the next item.

`xsl:next-iteration` must be the last instruction evaluated in the iteration body -- no sibling instructions may follow it in execution flow. This is enforced statically: it must appear in a position where no further instructions can be reached after it executes (similar to a return statement).

If the sequence is exhausted (no more items), control passes to `xsl:on-completion` if present.

## Examples

### Accumulating a Sum

```xml
<xsl:iterate select="//transaction">
  <xsl:param name="total" as="xs:decimal" select="0"/>
  <xsl:param name="count" as="xs:integer" select="0"/>

  <xsl:next-iteration>
    <xsl:with-param name="total" select="$total + @amount"/>
    <xsl:with-param name="count" select="$count + 1"/>
  </xsl:next-iteration>

  <xsl:on-completion>
    <summary total="{$total}" count="{$count}" average="{$total div $count}"/>
  </xsl:on-completion>
</xsl:iterate>
```

### Conditional State Update

```xml
<xsl:iterate select="$events">
  <xsl:param name="state" select="'idle'"/>

  <event type="{@type}" state="{$state}"/>

  <xsl:next-iteration>
    <xsl:with-param name="state" select="
      if (@type = 'start') then 'running'
      else if (@type = 'stop') then 'idle'
      else $state
    "/>
  </xsl:next-iteration>
</xsl:iterate>
```

## Error Codes

- **XTSE3120**: `xsl:next-iteration` appears outside the body of `xsl:iterate`.
- **XTSE3130**: Instructions appear after `xsl:next-iteration` that could be reached.

## See Also

- [xsl:iterate](xsl-iterate.md)
- [xsl:break](xsl-break.md)
- [xsl:on-completion](xsl-on-completion.md)
- [xsl:with-param](xsl-with-param.md)
