---
name: xsl:merge
category: example
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#merge
---

# Streaming merge of two sorted sources

## Stylesheet

```xml
<xsl:stylesheet version="3.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:mode streamable="yes"/>

  <xsl:template name="xsl:initial-template">
    <merged>
      <xsl:merge>
        <!-- First source: orders sorted by date -->
        <xsl:merge-source name="orders"
                          for-each-source="'orders.xml'"
                          select="root/order"
                          sort-before-merge="no">
          <xsl:merge-key select="xs:date(@date)"
                         xmlns:xs="http://www.w3.org/2001/XMLSchema"/>
        </xsl:merge-source>

        <!-- Second source: invoices sorted by date -->
        <xsl:merge-source name="invoices"
                          for-each-source="'invoices.xml'"
                          select="root/invoice"
                          sort-before-merge="no">
          <xsl:merge-key select="xs:date(@date)"
                         xmlns:xs="http://www.w3.org/2001/XMLSchema"/>
        </xsl:merge-source>

        <!-- For each key group, emit a combined element -->
        <xsl:merge-action>
          <day date="{current-merge-key()}">
            <xsl:copy-of select="current-merge-group('orders')"/>
            <xsl:copy-of select="current-merge-group('invoices')"/>
          </day>
        </xsl:merge-action>
      </xsl:merge>
    </merged>
  </xsl:template>

</xsl:stylesheet>
```

## What it does

`xsl:merge` performs a streaming merge join across multiple pre-sorted
input sequences. Both documents are consumed in a single pass without
loading either into memory. For each distinct merge key value,
`xsl:merge-action` fires once with `current-merge-group()` providing all
items from that source that share that key. `current-merge-key()` returns
the shared key value.

## Common pitfalls

- Both sources must be **already sorted** by the merge key unless `sort-before-merge="yes"` —
  mis-sorted data causes `XTDE2220`.
- `current-merge-group()` and `current-merge-key()` are only valid **inside** `xsl:merge-action`.
- The merge key expression must produce a comparable value — using a typed constructor like
  `xs:date(...)` avoids string-vs-date comparison surprises.
- `sort-before-merge="yes"` buffers the entire source in memory first; avoid on large files.
