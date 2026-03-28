---
name: preceding-sibling
category: axis
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-31/#doc-xpath31-ReverseAxis
---

# preceding-sibling Axis

The `preceding-sibling` axis contains all siblings of the context node that appear before it in document order.

## Direction

Reverse axis.

## Principal Node Kind

Element.

## Nodes Selected

All nodes with the same parent as the context node that appear before it in document order. If the context node is an attribute or namespace node, this axis is empty.

## Examples

```xpath
preceding-sibling::item           <!-- All 'item' siblings before this node -->
preceding-sibling::*[1]           <!-- The immediately previous sibling element -->
preceding-sibling::h2[1]          <!-- The most recent h2 sibling -->
```

## See Also

- [following-sibling](following-sibling.md)
- [preceding](preceding.md)
