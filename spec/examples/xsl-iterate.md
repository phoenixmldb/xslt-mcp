---
name: xsl:iterate
category: example
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#xsl-iterate
---

# Stateful iteration with carry-over parameters

## Stylesheet

```xml
<xsl:stylesheet version="3.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema">

  <!-- Number lines, keeping a running counter and a previous-value -->
  <xsl:template name="xsl:initial-template">
    <xsl:variable name="items" select="doc('data.xml')/data/item"/>
    <table>
      <xsl:iterate select="$items">
        <!-- Carry-over params — updated each iteration -->
        <xsl:param name="row-num"  select="1"   as="xs:integer"/>
        <xsl:param name="prev-val" select="0.0" as="xs:double"/>

        <!-- Output this row -->
        <row n="{$row-num}" delta="{xs:double(@value) - $prev-val}">
          <xsl:value-of select="@label"/>
        </row>

        <!-- Update params for the next iteration -->
        <xsl:next-iteration>
          <xsl:with-param name="row-num"  select="$row-num + 1"/>
          <xsl:with-param name="prev-val" select="xs:double(@value)"/>
        </xsl:next-iteration>
      </xsl:iterate>
    </table>
  </xsl:template>

</xsl:stylesheet>
```

## What it does

`xsl:iterate` is a streamable replacement for `xsl:for-each` when you need
to carry state from one iteration to the next. `xsl:param` inside
`xsl:iterate` declares named carry-over variables; `xsl:next-iteration`
updates them before the next item is processed. This pattern replaces the
`xsl:for-each` + accumulator combination needed in XSLT 2.0.

You can exit early using `xsl:break`, which also lets you return a final
value via its `select` attribute.

## Common pitfalls

- `xsl:next-iteration` must appear as a **direct child** of `xsl:iterate` —
  it cannot be inside a conditional or nested construct.
- Parameters declared in `xsl:iterate` have a default (the `select`
  attribute) that applies only on the **first** iteration. After the first
  item, the value comes entirely from `xsl:next-iteration`.
- `xsl:iterate` is streamable over a forward sequence but its body is not
  inherently streaming — the selected sequence is consumed item by item.
- `xsl:break` without `select` returns an empty sequence; the `select`
  attribute provides a final computed result from the loop.
