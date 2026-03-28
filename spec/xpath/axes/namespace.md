---
name: namespace
category: axis
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-31/#doc-xpath31-ForwardAxis
---

# namespace Axis

The `namespace` axis contains the namespace nodes of the context node.

## Direction

Forward axis.

## Principal Node Kind

Namespace.

## Nodes Selected

All namespace nodes in scope for the context element. Only element nodes have namespace nodes. This axis is deprecated in XPath 2.0+ but still supported for backward compatibility.

## Notes

- Each in-scope namespace binding (including `xml`) produces a namespace node.
- The `namespace` axis is deprecated. Use `in-scope-prefixes()` and `namespace-uri-for-prefix()` instead.

## Examples

```xpath
namespace::*            <!-- All namespace nodes -->
namespace::xsl          <!-- The 'xsl' namespace node -->
```

## See Also

- [attribute](attribute.md)
- [self](self.md)
