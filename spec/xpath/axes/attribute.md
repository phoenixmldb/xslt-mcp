---
name: attribute
category: axis
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-31/#doc-xpath31-ForwardAxis
---

# attribute Axis

The `attribute` axis contains the attributes of the context node.

## Direction

Forward axis (but not in any defined document order).

## Principal Node Kind

Attribute.

## Nodes Selected

All attribute nodes of the context node. Only element nodes have attributes; for other node types this axis is empty. Namespace declarations (`xmlns:...`) are not attributes in XPath.

## Abbreviated Syntax

```xpath
attribute::name  ≡  @name
attribute::*     ≡  @*
```

## Examples

```xpath
attribute::id        <!-- The 'id' attribute -->
@id                  <!-- Same (abbreviated) -->
@*                   <!-- All attributes -->
@xml:lang            <!-- The xml:lang attribute -->
```

## See Also

- [self](self.md)
- [child](child.md)
- [namespace](namespace.md)
