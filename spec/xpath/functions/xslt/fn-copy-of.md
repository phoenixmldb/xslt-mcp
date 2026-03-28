---
name: fn:copy-of
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#function-copy-of
---

# fn:copy-of

Creates a deep copy of a node.

## Signature

```
fn:copy-of() as item()
fn:copy-of($input as item()*) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$input` | `item()*` | The items to copy. Defaults to the context item. |

## Return Type

`item()*`

## Description

The `fn:copy-of` function creates a deep copy of the input items. For nodes, this creates new node instances that are copies of the original nodes with all their descendants. Unlike `fn:snapshot`, the copy does not include ancestor information — the copied nodes are standalone trees.

This function is particularly relevant in streaming transformations. In streaming mode, a node may only be visited once; `fn:copy-of` captures the node's subtree so it can be processed multiple times.

For atomic values and function items, the function returns them unchanged. If no argument is provided, the context item is used.

## Examples

```xpath
copy-of(//item[1])
(: Returns a deep copy of the first item element :)

copy-of()
(: Returns a deep copy of the context item :)

let $copy := copy-of(//data)
return ($copy/a, $copy/b)
(: Copies data once, then navigates it multiple times :)
```

## Error Codes

- `XPTY0004` — If the context item is not defined (when called without arguments).

## See Also

- [fn:snapshot](fn-snapshot.md)
