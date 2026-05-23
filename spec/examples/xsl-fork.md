---
name: xsl:fork
category: example
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#fork
---

# Concurrent independent outputs in a single streaming pass

## Stylesheet

```xml
<xsl:stylesheet version="3.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema">

  <xsl:mode streamable="yes"/>

  <xsl:template name="xsl:initial-template">
    <xsl:apply-templates select="doc('events.xml')/events"/>
  </xsl:template>

  <xsl:template match="events" mode="#default">
    <!-- xsl:fork evaluates its branches concurrently over the same stream -->
    <xsl:fork>
      <!-- Branch 1: count errors -->
      <xsl:sequence>
        <error-count>
          <xsl:value-of select="count(event[@type='error'])"/>
        </error-count>
      </xsl:sequence>

      <!-- Branch 2: collect warnings -->
      <xsl:sequence>
        <warnings>
          <xsl:copy-of select="event[@type='warning']"/>
        </warnings>
      </xsl:sequence>
    </xsl:fork>
  </xsl:template>

</xsl:stylesheet>
```

## What it does

`xsl:fork` allows multiple independent sequence constructors to consume the
same streaming source simultaneously in a single pass. Without `xsl:fork`,
you would need to read the document twice (or buffer it) to produce two
different views. The processor handles the fan-out internally; each branch
gets the same sequence of events from the parser. The results of all
branches are concatenated in document order.

## Common pitfalls

- Each branch inside `xsl:fork` must independently satisfy streaming
  constraints — if one branch requires non-streaming access, the entire
  fork is rejected.
- The branches are **logically concurrent** but not necessarily executed on
  separate threads; the actual scheduling is implementation-defined.
- Avoid side effects inside `xsl:fork` branches — the execution order
  between branches is not guaranteed.
- `xsl:fork` is only useful in a streamable context; in non-streaming
  stylesheets it is valid but provides no benefit.
