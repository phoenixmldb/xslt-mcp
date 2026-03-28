---
name: ancestor
category: axis
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-31/#doc-xpath31-ReverseAxis
---

# ancestor Axis

The `ancestor` axis contains all ancestors of the context node — the parent, the parent's parent, and so on up to the document node.

## Direction

Reverse axis (nodes are in reverse document order: parent first, then grandparent, etc.).

## Principal Node Kind

Element.

## Nodes Selected

All ancestors, starting from the parent up to the root. The document node is an ancestor of all nodes in the document. The context node itself is never included.

## Examples

```xpath
ancestor::div          <!-- All ancestor 'div' elements -->
ancestor::*            <!-- All ancestor elements -->
ancestor::node()       <!-- All ancestor nodes (including the document node) -->
ancestor::section[1]   <!-- The nearest ancestor 'section' -->
```

## See Also

- [ancestor-or-self](ancestor-or-self.md)
- [parent](parent.md)
- [descendant](descendant.md)
