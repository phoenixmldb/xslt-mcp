---
name: xsl:break
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#break
---

# xsl:break

Terminates iteration early within `xsl:iterate`.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | No | `expression` | Expression producing the result of the iteration upon breaking. |

## Content Model

Sequence constructor (used as the result if `select` is absent).

## Description

`xsl:break` causes early termination of an `xsl:iterate` loop. When evaluated, the remaining items in the sequence are skipped and the `xsl:on-completion` block is not executed. The result of the `xsl:iterate` instruction is the concatenation of results produced by prior iterations plus the result of the `xsl:break`.

The `select` attribute or the body sequence constructor produces a value that becomes part of the overall result of the iteration. If neither is provided, the break produces an empty sequence.

`xsl:break` must be a descendant of `xsl:iterate` but not nested inside another `xsl:iterate`. It is a static error if `xsl:break` appears outside an `xsl:iterate` body or within a nested `xsl:iterate`.

## Examples

### Finding First Match

```xml
<xsl:iterate select="$records">
  <xsl:if test="@status = 'error'">
    <xsl:break select="."/>
  </xsl:if>
</xsl:iterate>
```

### Budget Limit

```xml
<xsl:iterate select="items/item">
  <xsl:param name="total" as="xs:decimal" select="0"/>
  <xsl:if test="$total + @price > $budget">
    <xsl:break>
      <exceeded-at item="{@name}" total="{$total}"/>
    </xsl:break>
  </xsl:if>
  <xsl:next-iteration>
    <xsl:with-param name="total" select="$total + @price"/>
  </xsl:next-iteration>
</xsl:iterate>
```

## Error Codes

- **XTSE3120**: `xsl:break` appears outside the body of `xsl:iterate`.

## See Also

- [xsl:iterate](xsl-iterate.md)
- [xsl:next-iteration](xsl-next-iteration.md)
- [xsl:on-completion](xsl-on-completion.md)
