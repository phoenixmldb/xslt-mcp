---
name: ancestor-or-self
category: axis
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-31/#doc-xpath31-ReverseAxis
---

# ancestor-or-self Axis

The `ancestor-or-self` axis contains the context node and all its ancestors.

## Direction

Reverse axis.

## Principal Node Kind

Element.

## Nodes Selected

The context node itself, plus all ancestors up to the root document node.

## Examples

```xpath
ancestor-or-self::div        <!-- Self (if div) plus all ancestor divs -->
ancestor-or-self::*          <!-- Self and all ancestor elements -->
ancestor-or-self::node()[1]  <!-- The context node itself -->
```

## See Also

- [ancestor](ancestor.md)
- [descendant-or-self](descendant-or-self.md)
