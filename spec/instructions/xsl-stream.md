---
name: xsl:stream
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#stream
---

# xsl:stream

Processes a document in streaming mode without building the full document tree in memory.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `href` | Yes | `uri` | URI of the document to stream. Attribute value template allowed. |

## Content Model

Sequence constructor, evaluated in streaming mode with the document node as context item.

## Description

`xsl:stream` opens a document and processes it using streaming, where the document is consumed as a sequence of events (start tags, end tags, text nodes) without building the complete XDM tree in memory. This is critical for processing documents that are too large to fit in memory.

The sequence constructor body must conform to streaming rules. The key constraints are:

- Each child axis step can be used only once (no re-traversal of the same nodes).
- Only one downward selection into children is allowed per template.
- Attribute and namespace axes can be accessed freely (they are available before child content).
- Functions like `copy-of()` can snapshot individual nodes for later use.
- `xsl:fork` enables multiple independent consumers of the stream.

`xsl:stream` is essentially shorthand for `xsl:source-document` with `streamable="yes"`. In newer XSLT 3.0 specifications, `xsl:source-document` is the preferred instruction as it offers more attributes (like `use-accumulators`). However, `xsl:stream` remains widely used.

The streaming model works best with flat or repetitive structures (like log files, data exports) where records can be processed independently. Deeply nested or cross-referencing structures may require `xsl:fork` or accumulators for streaming.

## Examples

### Processing a Large Log File

```xml
<xsl:mode streamable="yes"/>

<xsl:template name="xsl:initial-template">
  <errors>
    <xsl:stream href="server-log.xml">
      <xsl:apply-templates select="log/entry[@level='ERROR']"/>
    </xsl:stream>
  </errors>
</xsl:template>

<xsl:template match="entry">
  <error timestamp="{@timestamp}">
    <xsl:value-of select="message"/>
  </error>
</xsl:template>
```

### Counting and Filtering in One Pass

```xml
<xsl:stream href="transactions.xml">
  <xsl:fork>
    <xsl:sequence>
      <summary count="{count(data/transaction)}"/>
    </xsl:sequence>
    <xsl:for-each select="data/transaction[@amount > 10000]">
      <large-transaction id="{@id}" amount="{@amount}"/>
    </xsl:for-each>
  </xsl:fork>
</xsl:stream>
```

### Streaming with Copy-Of

```xml
<xsl:stream href="catalog.xml">
  <xsl:for-each select="catalog/product[category='electronics']">
    <xsl:copy-of select="."/>
  </xsl:for-each>
</xsl:stream>
```

## Error Codes

- **XTDE3170**: The document at the given URI cannot be opened.
- **XTSE3085**: The body does not satisfy streaming constraints.

## See Also

- [xsl:source-document](xsl-source-document.md)
- [xsl:fork](xsl-fork.md)
- [xsl:mode](xsl-mode.md)
- [xsl:accumulator](xsl-accumulator.md)
