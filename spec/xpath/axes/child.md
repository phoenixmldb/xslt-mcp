---
name: child
category: axis
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-31/#doc-xpath31-ForwardAxis
---

# child Axis

The `child` axis contains the children of the context node. This is the default axis in abbreviated syntax.

## Direction

Forward axis.

## Principal Node Kind

Element.

## Nodes Selected

The child nodes of the context node. Document nodes and element nodes have children; attribute nodes, text nodes, comment nodes, processing instruction nodes, and namespace nodes do not.

## Abbreviated Syntax

`child::` is the default axis and can be omitted.

```xpath
child::item    ≡  item
child::*       ≡  *
child::text()  ≡  text()
```

## Examples

```xpath
child::chapter          <!-- All chapter children -->
chapter                 <!-- Same (abbreviated) -->
child::node()           <!-- All child nodes -->
child::text()           <!-- All text node children -->
```

## See Also

- [parent](parent.md)
- [descendant](descendant.md)
