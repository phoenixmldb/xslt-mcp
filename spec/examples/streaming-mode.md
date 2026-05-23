---
name: xsl:mode-streamable
category: example
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#streamable-stylesheet
---

# Declaring and using a streamable mode

## Stylesheet

```xml
<xsl:stylesheet version="3.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema">

  <!-- Declare a streamable mode named "stream-pass" -->
  <xsl:mode name="stream-pass" streamable="yes"/>

  <!-- Entry point: open the source document in streaming mode -->
  <xsl:template name="xsl:initial-template">
    <result>
      <xsl:apply-templates select="doc('large-log.xml')/log/entry"
                           mode="stream-pass"/>
    </result>
  </xsl:template>

  <!-- Each entry is processed once and discarded — no tree is built -->
  <xsl:template match="entry" mode="stream-pass">
    <xsl:if test="@severity = 'ERROR'">
      <error ts="{@timestamp}">
        <xsl:value-of select="message"/>
      </error>
    </xsl:if>
  </xsl:template>

</xsl:stylesheet>
```

## What it does

`streamable="yes"` on `xsl:mode` instructs the processor to enforce
streaming constraints on all templates in that mode. The source document
is read as a stream — only the current node and its ancestors are kept in
memory. This lets you process arbitrarily large XML files without loading
them entirely. The processor will statically reject any template that
would require holding sibling or descendant context beyond what streaming
allows.

## Common pitfalls

- You cannot use `position()`, `last()`, or sibling axes (`following-sibling::`, `preceding::`) in
  streaming mode — they require knowing the full sequence, which streaming cannot provide.
- `xsl:value-of` inside a streamable template is fine; `xsl:copy-of` is only allowed if the
  selected subtree is consumed in a single downward pass (grounded).
- Streaming is checked **statically** — a template that happens to be safe at runtime is still
  rejected if the processor cannot prove it at compile time.
