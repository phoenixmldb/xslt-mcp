---
name: descendant-or-self
category: axis
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-31/#doc-xpath31-ForwardAxis
---

# descendant-or-self Axis

The `descendant-or-self` axis contains the context node and all its descendants.

## Direction

Forward axis.

## Principal Node Kind

Element.

## Nodes Selected

The context node itself plus all descendants.

## Abbreviated Syntax

The `//` abbreviation uses this axis:

```xpath
descendant-or-self::node()/child::item  ≡  .//item
/descendant-or-self::node()/child::item  ≡  //item
```

## Examples

```xpath
descendant-or-self::item     <!-- Self (if item) and all descendant items -->
.//item                      <!-- Same: all 'item' descendants and self -->
//item                       <!-- All 'item' elements in the document -->
```

## See Also

- [descendant](descendant.md)
- [ancestor-or-self](ancestor-or-self.md)
