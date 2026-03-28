---
name: fn:snapshot
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#function-snapshot
---

# fn:snapshot

Creates a copy of a node that retains its ancestry and namespace context.

## Signature

```
fn:snapshot() as item()
fn:snapshot($input as item()*) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$input` | `item()*` | The items to snapshot. Defaults to the context item. |

## Return Type

`item()*`

## Description

The `fn:snapshot` function creates a deep copy of the input items, but unlike a simple copy, it preserves ancestor information. For node items, the result includes copies of all ancestor elements up to and including the document node, with the node positioned in the same structural context. Sibling nodes of ancestors are not included.

This function is particularly important for streaming transformations where a node may need to be captured along with its context for later processing. The snapshot preserves namespace bindings, base URI, and type annotations from ancestors.

For atomic values and function items, the function returns them unchanged. If no argument is provided, the context item is used.

## Examples

```xpath
snapshot(//item[1])
(: Returns a copy of the first item element with its ancestor chain :)

snapshot()
(: Returns a snapshot of the context item :)

(: The snapshot preserves ancestor context: :)
snapshot(//p)/../name()
(: Returns the parent element name, preserved from the original tree :)
```

## Error Codes

- `XPTY0004` — If the context item is not defined (when called without arguments).

## See Also

- [fn:copy-of](fn-copy-of.md)
