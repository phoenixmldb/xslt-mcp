---
name: xsl:source-document
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#source-document
---

# xsl:source-document

Opens a source document for processing, with support for streaming.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `href` | Yes | `uri` | URI of the document to load. Attribute value template allowed. |
| `streamable` | No | `"yes" \| "no"` | Whether to process the document in streaming mode. Default is `no`. |
| `use-accumulators` | No | `EQNames` | Space-separated list of accumulator names to apply to the document. |
| `validation` | No | `"strict" \| "lax" \| "preserve" \| "strip"` | Validation mode for the loaded document. |
| `type` | No | `EQName` | Schema type for validation. |

## Content Model

Sequence constructor, evaluated with the document node as the context item.

## Description

`xsl:source-document` loads a document and processes it with the contained sequence constructor. When `streamable="yes"`, the document is processed in streaming mode, enabling processing of documents larger than available memory.

This instruction replaces the use of `fn:doc()` for streaming scenarios. While `fn:doc()` builds the complete tree in memory, `xsl:source-document` with `streamable="yes"` processes the document as a stream of events without materializing the full tree.

The `use-accumulators` attribute specifies which accumulators should be applied to the document as it is traversed. This is the mechanism for associating accumulators with specific documents.

In non-streaming mode (`streamable="no"`), `xsl:source-document` behaves similarly to using `fn:doc()` to load the document, but with additional control over validation and accumulator association.

The sequence constructor body is evaluated with the document node as the context item (`.`). Within a streaming body, the usual streaming constraints apply: a single downward selection into children, no multiple traversals of the same nodes.

## Examples

### Streaming a Large File

```xml
<xsl:mode streamable="yes"/>

<xsl:template name="xsl:initial-template">
  <xsl:source-document href="huge-data.xml" streamable="yes">
    <result>
      <xsl:apply-templates select="data/record"/>
    </result>
  </xsl:source-document>
</xsl:template>

<xsl:template match="record">
  <row id="{@id}">
    <xsl:value-of select="name"/>
  </row>
</xsl:template>
```

### With Accumulators

```xml
<xsl:accumulator name="record-count" as="xs:integer"
                 initial-value="0" streamable="yes">
  <xsl:accumulator-rule match="record" select="$value + 1"/>
</xsl:accumulator>

<xsl:template name="xsl:initial-template">
  <xsl:source-document href="data.xml" streamable="yes"
                       use-accumulators="record-count">
    <summary total="{accumulator-after('record-count')}"/>
  </xsl:source-document>
</xsl:template>
```

### Non-Streaming with Validation

```xml
<xsl:source-document href="order.xml" validation="strict">
  <xsl:apply-templates select="order/line-item"/>
</xsl:source-document>
```

## Error Codes

- **XTDE3170**: The document cannot be loaded from the given URI.
- **XTSE3085**: The body does not satisfy streaming constraints when `streamable="yes"`.

## See Also

- [xsl:stream](xsl-stream.md)
- [xsl:accumulator](xsl-accumulator.md)
- [xsl:mode](xsl-mode.md)
