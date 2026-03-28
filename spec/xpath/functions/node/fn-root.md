---
name: fn:root
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-root
---

# fn:root

Returns the root of the tree containing the given node.

## Signature

```
fn:root() as node()
fn:root($arg as node()?) as node()?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `node()?` | The node whose root is returned. Defaults to the context item. |

## Return Type

`node()?`

## Description

The `fn:root` function returns the root node of the tree containing `$arg`. If `$arg` is a document node, it returns that document node. If `$arg` is any other node in a document tree, it returns the document node at the root. For nodes in a tree without a document node (a fragment), it returns the topmost node.

If `$arg` is the empty sequence, the function returns the empty sequence. If no argument is provided, the context item is used (it must be a node).

This function is useful for navigating back to the document level from any position in the tree, especially in template rules where the context may be deep in the hierarchy.

## Examples

```xpath
root()
(: Returns the document node of the current context :)

root($node)/bookstore/book
(: Navigates from any node to the document root and then selects :)

root(.) instance of document-node()
(: Tests if the root is a document node :)
```

## Error Codes

- `XPTY0004` — If the context item is not a node (when called without arguments).

## See Also

- [fn:base-uri](fn-base-uri.md)
- [fn:document-uri](fn-document-uri.md)
