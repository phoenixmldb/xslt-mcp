---
name: xsl:fork
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#fork
---

# xsl:fork

Enables parallel evaluation of multiple consumers of a streamed input document.

## Attributes

None.

## Content Model

( `xsl:sequence` | `xsl:for-each` | `xsl:for-each-group` )+

Each child processes the streamed input independently.

## Description

`xsl:fork` is a streaming instruction that allows multiple independent operations to be performed on a streamed document in a single pass. Without `xsl:fork`, streaming mode restricts a sequence constructor to consuming the input exactly once. `xsl:fork` lifts this restriction by telling the processor that each child instruction independently consumes the streamed input.

Each child of `xsl:fork` processes the input stream as if it were the sole consumer. The processor may evaluate the children in parallel or may buffer and replay the input as needed. The results of all children are concatenated in document order.

This is critical for streaming scenarios where you need to produce multiple outputs or compute multiple aggregations from a single streamed document. For example, you might want to generate both a summary and a detailed listing from a large XML file without loading it entirely into memory.

The children must be `xsl:sequence`, `xsl:for-each`, or `xsl:for-each-group` instructions. Other instructions are not permitted as direct children of `xsl:fork`.

## Examples

### Parallel Aggregation and Listing

```xml
<xsl:mode streamable="yes"/>

<xsl:template match="/">
  <result>
    <xsl:fork>
      <xsl:sequence>
        <summary count="{count(//record)}" total="{sum(//record/@amount)}"/>
      </xsl:sequence>
      <xsl:for-each select="//record[@flagged='true']">
        <flagged id="{@id}"/>
      </xsl:for-each>
    </xsl:fork>
  </result>
</xsl:template>
```

### Multiple Groupings

```xml
<xsl:fork>
  <xsl:for-each-group select="data/entry" group-by="@category">
    <category name="{current-grouping-key()}" count="{count(current-group())}"/>
  </xsl:for-each-group>
  <xsl:for-each-group select="data/entry" group-by="@region">
    <region name="{current-grouping-key()}" count="{count(current-group())}"/>
  </xsl:for-each-group>
</xsl:fork>
```

## Error Codes

- **XTSE3085**: A child of `xsl:fork` is not one of the permitted instructions.
- **XTSE3090**: A child instruction does not satisfy streaming constraints.

## See Also

- [xsl:stream](xsl-stream.md)
- [xsl:source-document](xsl-source-document.md)
- [xsl:mode](xsl-mode.md)
