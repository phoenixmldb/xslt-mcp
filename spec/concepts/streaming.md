---
name: Streaming
category: concept
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#streaming
---

# Streaming

Streaming allows XSLT 3.0 to process very large documents without loading the entire document into memory. In streaming mode, the processor reads the input document sequentially, processing each node as it is encountered.

## Enabling Streaming

Streaming can be enabled in several ways:

### Streamable Mode

```xml
<xsl:mode streamable="yes"/>
```

### Stream Source

```xml
<xsl:source-document streamable="yes" href="large-file.xml">
  <xsl:apply-templates/>
</xsl:source-document>
```

## Streamability Rules

For a template to be streamable, it must satisfy certain constraints:

1. **Single downward selection**: Each child axis can be navigated at most once.
2. **No look-ahead or look-back**: Cannot access siblings of the current node.
3. **No multiple passes**: The document is read once, sequentially.
4. **Grounded values only**: Variables bound to streamed nodes must be grounded (fully materialized).

### Streamable Patterns

- `match="element-name"` — Streamable
- `match="element-name[@attr]"` — Streamable (attributes are available)
- `match="parent/child"` — Streamable

### Non-Streamable Patterns

- Accessing `preceding-sibling::` or `following-sibling::` axes
- Multiple uses of `child::` axis
- Sorting based on descendant values

## xsl:iterate for Streaming

`xsl:iterate` is designed for streaming scenarios where you need to maintain state:

```xml
<xsl:mode streamable="yes"/>

<xsl:template match="/">
  <xsl:source-document streamable="yes" href="transactions.xml">
    <xsl:iterate select="transactions/transaction">
      <xsl:param name="balance" select="0" as="xs:decimal"/>
      <xsl:variable name="new-balance" select="$balance + @amount"/>
      <entry balance="{$new-balance}"/>
      <xsl:next-iteration>
        <xsl:with-param name="balance" select="$new-balance"/>
      </xsl:next-iteration>
    </xsl:iterate>
  </xsl:source-document>
</xsl:template>
```

## Accumulators

Accumulators provide another mechanism for gathering information during streaming. See [Accumulators](accumulators.md).

## See Also

- [xsl:iterate](../instructions/xsl-iterate.md)
- [Accumulators](accumulators.md)
- [Modes](modes.md)
