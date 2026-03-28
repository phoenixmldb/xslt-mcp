---
name: preceding
category: axis
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-31/#doc-xpath31-ReverseAxis
---

# preceding Axis

The `preceding` axis contains all nodes that appear before the context node in document order, excluding ancestors, attribute nodes, and namespace nodes.

## Direction

Reverse axis.

## Principal Node Kind

Element.

## Nodes Selected

All nodes in the document that come before the start of the context node in document order, excluding any ancestors (since ancestors contain the context node, they neither precede nor follow it).

## Examples

```xpath
preceding::chapter        <!-- All 'chapter' elements before this node -->
preceding::*[1]           <!-- The nearest preceding element (non-ancestor) -->
preceding::h2[1]          <!-- The most recent h2 before this node -->
```

## See Also

- [preceding-sibling](preceding-sibling.md)
- [following](following.md)
- [ancestor](ancestor.md)
