---
name: xsl:merge-source
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#merge-source
---

# xsl:merge-source

Defines an input source for `xsl:merge`, specifying where to read items and how they are sorted.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | No | `NCName` | Name for this source, used to identify items in `current-merge-group()`. |
| `for-each-item` | No | `expression` | Expression returning items to iterate over. |
| `for-each-source` | No | `expression` | Expression returning URIs of documents to process. |
| `select` | No | `expression` | Expression selecting nodes within each document/item. |
| `sort-before-merge` | No | `"yes" \| "no"` | Whether to sort the input before merging. Default is `no`. |
| `streamable` | No | `"yes" \| "no"` | Whether to process source documents in streaming mode. |

## Content Model

( `xsl:merge-key`+ )

One or more `xsl:merge-key` elements defining the sort keys.

## Description

Each `xsl:merge-source` defines one input feed for the merge operation. It specifies how to obtain the items (via `for-each-source` for document URIs or `for-each-item` for in-memory sequences) and which nodes within those items to merge (via `select`).

The `for-each-source` attribute is typically used to reference external documents. The expression should return a sequence of URI strings. For each URI, the document is loaded (or streamed if `streamable="yes"`) and the `select` expression is evaluated against it.

The `for-each-item` attribute iterates over an in-memory sequence, evaluating `select` against each item.

The `sort-before-merge` attribute indicates whether the processor should sort the input. When `"no"` (the default), the input is assumed to already be in the correct order. When `"yes"`, the processor sorts before merging. For streaming, the input must be pre-sorted.

The `name` attribute allows the `xsl:merge-action` to distinguish which source a particular item came from using `current-merge-group('name')`.

## Examples

### Document Source with Streaming

```xml
<xsl:merge-source name="transactions"
                  for-each-source="'transactions.xml'"
                  streamable="yes"
                  select="data/transaction">
  <xsl:merge-key select="@date" order="ascending"/>
  <xsl:merge-key select="@id" order="ascending"/>
</xsl:merge-source>
```

### In-Memory Source

```xml
<xsl:merge-source name="cached" for-each-item="$cached-records" select=".">
  <xsl:merge-key select="@timestamp" order="ascending"/>
</xsl:merge-source>
```

## Error Codes

- **XTDE2220**: The input is not sorted as declared and `sort-before-merge="no"`.

## See Also

- [xsl:merge](xsl-merge.md)
- [xsl:merge-key](xsl-merge-key.md)
- [xsl:merge-action](xsl-merge-action.md)
