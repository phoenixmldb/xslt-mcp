---
name: following
category: axis
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-31/#doc-xpath31-ForwardAxis
---

# following Axis

The `following` axis contains all nodes that appear after the context node in document order, excluding descendants, attribute nodes, and namespace nodes.

## Direction

Forward axis.

## Principal Node Kind

Element.

## Nodes Selected

All nodes in the document that come after the end of the context node in document order, but that are not descendants of the context node.

## Examples

```xpath
following::chapter        <!-- All 'chapter' elements after this node -->
following::*[1]           <!-- The first element after this node (non-descendant) -->
following::text()         <!-- All text nodes after this node -->
```

## See Also

- [following-sibling](following-sibling.md)
- [preceding](preceding.md)
