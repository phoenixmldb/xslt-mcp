---
name: fn:current
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#function-current
---

# fn:current

Returns the current item in XSLT processing.

## Signature

```
fn:current() as item()
```

## Parameters

None.

## Return Type

`item()`

## Description

The `fn:current` function returns the item that is the current item at the point in the stylesheet where the expression is evaluated. This is distinct from `.` (the context item), which changes within predicates and nested expressions. The `fn:current()` function always refers to the item being processed by the current template rule or `xsl:for-each`.

In a predicate like `[. = current()]`, `.` refers to each node being tested by the predicate, while `current()` refers to the node being processed by the enclosing template or `xsl:for-each`. This distinction is critical for cross-referencing and join operations.

This function is only available in XSLT; it cannot be used in standalone XPath or XQuery contexts.

## Examples

```xpath
(: Select items where category matches the current element's @cat :)
//item[@category = current()/@cat]

(: In a predicate, . and current() differ: :)
//employee[department = current()/department]
(: . is each employee; current() is the node being processed :)

(: Using key with current value: :)
key('lookup', current()/@ref)
```

## Error Codes

- `XTDE3360` — Called outside an XSLT transformation context.

## See Also

- [fn:current-group](fn-current-group.md)
- [fn:current-grouping-key](fn-current-grouping-key.md)
