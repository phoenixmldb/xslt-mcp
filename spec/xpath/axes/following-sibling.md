---
name: following-sibling
category: axis
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-31/#doc-xpath31-ForwardAxis
---

# following-sibling Axis

The `following-sibling` axis contains all siblings of the context node that appear after it in document order.

## Direction

Forward axis.

## Principal Node Kind

Element.

## Nodes Selected

All nodes with the same parent as the context node that appear after it in document order. If the context node is an attribute or namespace node, this axis is empty.

## Examples

```xpath
following-sibling::item           <!-- All 'item' siblings after this node -->
following-sibling::*[1]           <!-- The immediately next sibling element -->
following-sibling::node()         <!-- All following sibling nodes -->
```

## See Also

- [preceding-sibling](preceding-sibling.md)
- [following](following.md)
