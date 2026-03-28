---
name: xsl:merge
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#merge
---

# xsl:merge

Merges multiple pre-sorted input sequences into a single output based on merge keys.

## Attributes

None.

## Content Model

( `xsl:merge-source`+, `xsl:merge-action` )

One or more `xsl:merge-source` elements define the inputs, followed by exactly one `xsl:merge-action` that processes each merged group.

## Description

`xsl:merge` combines multiple sorted input sequences, processing items with matching keys together. It is the XSLT equivalent of a merge join and is designed for processing large datasets that are already sorted, including in streaming mode.

Each `xsl:merge-source` defines an input sequence and its sort keys. The inputs must already be sorted by these keys (or `sort-before-merge="yes"` must be specified). The merge operation interleaves items from all sources based on key order, grouping items with equal keys.

For each group of items sharing the same key, the `xsl:merge-action` is evaluated. Within the action, `fn:current-merge-group()` returns all items with the current key, optionally filtered by source name. `fn:current-merge-key()` returns the key value.

This instruction is critical for large data processing. Unlike loading all data into memory and sorting, `xsl:merge` can process inputs in a streaming fashion, consuming minimal memory. It is ideal for merging log files, combining sorted database exports, or consolidating data from multiple sources.

## Examples

### Merging Two Sorted Files

```xml
<xsl:merge>
  <xsl:merge-source name="orders"
                    for-each-source="'orders.xml'"
                    select="order"
                    sort-before-merge="no">
    <xsl:merge-key select="@date" order="ascending"/>
  </xsl:merge-source>

  <xsl:merge-source name="invoices"
                    for-each-source="'invoices.xml'"
                    select="invoice"
                    sort-before-merge="no">
    <xsl:merge-key select="@date" order="ascending"/>
  </xsl:merge-source>

  <xsl:merge-action>
    <group date="{current-merge-key()}">
      <xsl:copy-of select="current-merge-group('orders')"/>
      <xsl:copy-of select="current-merge-group('invoices')"/>
    </group>
  </xsl:merge-action>
</xsl:merge>
```

### Merging Multiple Log Files

```xml
<xsl:merge>
  <xsl:merge-source for-each-source="uri-collection('logs/')"
                    select="log/entry">
    <xsl:merge-key select="@timestamp" order="ascending"/>
  </xsl:merge-source>

  <xsl:merge-action>
    <xsl:copy-of select="current-merge-group()"/>
  </xsl:merge-action>
</xsl:merge>
```

## Error Codes

- **XTDE2220**: An input sequence is not sorted according to the declared merge keys.
- **XTSE0010**: No `xsl:merge-action` is present.

## See Also

- [xsl:merge-source](xsl-merge-source.md)
- [xsl:merge-action](xsl-merge-action.md)
- [xsl:merge-key](xsl-merge-key.md)
- [xsl:sort](xsl-sort.md)
