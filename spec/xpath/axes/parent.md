---
name: parent
category: axis
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-31/#doc-xpath31-ReverseAxis
---

# parent Axis

The `parent` axis contains the parent of the context node, if there is one.

## Direction

Reverse axis.

## Principal Node Kind

Element.

## Nodes Selected

The single parent node. Document nodes have no parent. The parent of an attribute node is the element that carries it, even though the attribute is not a child of that element.

## Abbreviated Syntax

```xpath
parent::node()  ≡  ..
parent::item    (no abbreviation)
```

## Examples

```xpath
parent::node()     <!-- The parent node -->
..                 <!-- Same (abbreviated) -->
parent::chapter    <!-- Parent if it is a 'chapter' element -->
../title           <!-- 'title' siblings of the context node -->
```

## See Also

- [child](child.md)
- [ancestor](ancestor.md)
