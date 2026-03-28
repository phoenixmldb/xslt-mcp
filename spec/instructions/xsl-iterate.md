---
name: xsl:iterate
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#iterate
---

# xsl:iterate

Iterates over a sequence with the ability to maintain state between iterations using parameters. Unlike `xsl:for-each`, allows early termination via `xsl:break` and passing values forward via `xsl:next-iteration`. Designed for streaming.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | Yes | `expression` | Expression returning the sequence to iterate over. |

## Content Model

( `xsl:param`*, _sequence-constructor_, `xsl:on-completion`? )

Within the body, `xsl:next-iteration` passes updated parameter values to the next iteration, and `xsl:break` terminates the loop early.

## Examples

### Running Total

```xml
<xsl:iterate select="transactions/transaction">
  <xsl:param name="balance" as="xs:decimal" select="0"/>

  <row>
    <amount><xsl:value-of select="@amount"/></amount>
    <balance><xsl:value-of select="$balance + @amount"/></balance>
  </row>

  <xsl:next-iteration>
    <xsl:with-param name="balance" select="$balance + @amount"/>
  </xsl:next-iteration>
</xsl:iterate>
```

### Early Termination

```xml
<xsl:iterate select="items/item">
  <xsl:param name="total" as="xs:decimal" select="0"/>

  <xsl:choose>
    <xsl:when test="$total + @price > 100">
      <xsl:break>
        <over-budget total="{$total}"/>
      </xsl:break>
    </xsl:when>
    <xsl:otherwise>
      <xsl:next-iteration>
        <xsl:with-param name="total" select="$total + @price"/>
      </xsl:next-iteration>
    </xsl:otherwise>
  </xsl:choose>

  <xsl:on-completion>
    <within-budget total="{$total}"/>
  </xsl:on-completion>
</xsl:iterate>
```

## Error Codes

- **XTSE0010**: The `select` attribute is required.
- **XTSE3120**: `xsl:next-iteration` or `xsl:break` is not used correctly within the iteration body.

## See Also

- [xsl:for-each](xsl-for-each.md)
- [Concepts: Streaming](../concepts/streaming.md)
