---
name: xsl:merge-action
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#merge-action
---

# xsl:merge-action

Defines the processing to perform for each group of items with matching merge keys.

## Attributes

None.

## Content Model

Sequence constructor.

## Description

`xsl:merge-action` appears as the last child of `xsl:merge` and contains the template for processing each group of items that share the same merge key values. It is evaluated once for each distinct set of key values across all merge sources.

Within the body, two functions provide access to the merged data:

- `fn:current-merge-group()` -- returns all items from all sources that have the current key values. An optional string argument filters by source name: `current-merge-group('orders')` returns only items from the merge source named `orders`.
- `fn:current-merge-key()` -- returns the current merge key value. When multiple merge keys are defined, it returns the value of the first key. Use `current-merge-key(N)` (proposed in later specs) or access the key from items in the group.

The context item within `xsl:merge-action` is not defined. To process items, you must use `current-merge-group()` to obtain them.

## Examples

### Combining Records by Key

```xml
<xsl:merge-action>
  <combined-record key="{current-merge-key()}">
    <orders>
      <xsl:copy-of select="current-merge-group('orders')"/>
    </orders>
    <payments>
      <xsl:copy-of select="current-merge-group('payments')"/>
    </payments>
  </combined-record>
</xsl:merge-action>
```

### Deduplication

```xml
<xsl:merge-action>
  <xsl:copy-of select="current-merge-group()[1]"/>
</xsl:merge-action>
```

## Error Codes

- **XTSE0010**: `xsl:merge-action` appears outside of `xsl:merge`.

## See Also

- [xsl:merge](xsl-merge.md)
- [xsl:merge-source](xsl-merge-source.md)
- [xsl:merge-key](xsl-merge-key.md)
