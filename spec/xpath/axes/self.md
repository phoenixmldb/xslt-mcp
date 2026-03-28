---
name: self
category: axis
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-31/#doc-xpath31-ForwardAxis
---

# self Axis

The `self` axis contains only the context node itself.

## Direction

Forward axis.

## Principal Node Kind

Element.

## Nodes Selected

The context node, if it matches the node test. Otherwise, empty.

## Abbreviated Syntax

```xpath
self::node()  ≡  .
```

## Examples

```xpath
self::node()           <!-- The context node -->
.                      <!-- Same (abbreviated) -->
self::chapter          <!-- The context node, only if it is a 'chapter' element -->
self::text()           <!-- The context node, only if it is a text node -->
```

## See Also

- [parent](parent.md)
- [child](child.md)
