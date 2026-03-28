---
name: descendant
category: axis
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-31/#doc-xpath31-ForwardAxis
---

# descendant Axis

The `descendant` axis contains all descendants of the context node — children, children's children, and so on.

## Direction

Forward axis (document order).

## Principal Node Kind

Element.

## Nodes Selected

All descendant nodes (children, grandchildren, etc.). Does not include the context node itself or attribute/namespace nodes.

## Examples

```xpath
descendant::item          <!-- All 'item' descendants at any depth -->
descendant::text()        <!-- All text node descendants -->
descendant::*             <!-- All descendant elements -->
```

## See Also

- [descendant-or-self](descendant-or-self.md)
- [child](child.md)
- [ancestor](ancestor.md)
