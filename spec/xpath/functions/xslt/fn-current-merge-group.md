---
name: fn:current-merge-group
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#function-current-merge-group
---

# fn:current-merge-group

Returns the items in the current merge group within xsl:merge.

## Signature

```
fn:current-merge-group() as item()*
fn:current-merge-group($source as xs:string) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$source` | `xs:string?` | The name of a specific merge source. If omitted, returns items from all sources. |

## Return Type

`item()*`

## Description

The `fn:current-merge-group` function returns the items in the current merge group during the processing of an `xsl:merge` instruction. When called without arguments, it returns all items from all merge sources that share the current merge key. When called with a source name, it returns only the items from that specific merge source.

The `xsl:merge` instruction combines pre-sorted input sequences, grouping items with equal merge keys. This function provides access to the items in each group for processing.

This function is only meaningful within the body of the `xsl:merge-action` element of an `xsl:merge` instruction.

## Examples

```xpath
(: Within xsl:merge-action: :)
current-merge-group()
(: Returns all items with the current merge key :)

current-merge-group('orders')
(: Returns only items from the 'orders' merge source :)

count(current-merge-group('invoices'))
(: Counts invoice items in the current group :)
```

## Error Codes

- `XTDE3370` — The named source does not exist in the xsl:merge.

## See Also

- [fn:current-merge-key](fn-current-merge-key.md)
- [fn:current-group](fn-current-group.md)
