---
name: xsl:accumulator
category: example
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#accumulators
---

# Accumulators in streaming mode

## Stylesheet

```xml
<xsl:stylesheet version="3.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema">

  <!-- Running total of item prices, usable in streaming mode -->
  <xsl:accumulator name="running-total"
                   initial-value="0"
                   streamable="yes">
    <!-- Fires on entry to each item element -->
    <xsl:accumulator-rule match="item" phase="start">
      <xsl:sequence select="$value + xs:decimal(@price)"/>
    </xsl:accumulator-rule>
  </xsl:accumulator>

  <!-- Track the current section heading -->
  <xsl:accumulator name="current-section"
                   initial-value="'(none)'"
                   streamable="yes">
    <xsl:accumulator-rule match="section" phase="start">
      <xsl:sequence select="string(@title)"/>
    </xsl:accumulator-rule>
  </xsl:accumulator>

  <xsl:mode streamable="yes" use-accumulators="running-total current-section"/>

  <xsl:template name="xsl:initial-template">
    <report>
      <xsl:apply-templates select="doc('catalog.xml')/catalog/item"/>
    </report>
  </xsl:template>

  <xsl:template match="item">
    <line section="{accumulator-before('current-section')}"
          subtotal="{accumulator-after('running-total')}">
      <xsl:value-of select="@name"/>
    </line>
  </xsl:template>

</xsl:stylesheet>
```

## What it does

Accumulators maintain a running value as the processor walks the source
tree. Each `xsl:accumulator-rule` fires when the processor enters (`phase="start"`)
or exits (`phase="end"`) a matching node. The `$value` variable in the rule
body holds the accumulator's current value before the update.

`accumulator-before(name)` reads the value just **before** the current
node's start event; `accumulator-after(name)` reads it just **after**.
`use-accumulators` on `xsl:mode` must list any accumulator used by templates
in that mode.

## Common pitfalls

- Forgetting `use-accumulators` on the mode causes a static error — the
  processor needs to know which accumulators to maintain during a streaming
  pass.
- `phase="end"` can only reference the accumulator value, not the node's
  descendants (they have already been streamed past). Use `phase="start"` if
  you need to read an attribute value.
- Accumulators are **per-tree**: a fresh invocation gets a fresh initial
  value. They do not persist between separate `doc()` calls.
- The rule `match` pattern must be streamable (no backward axes).
