---
name: fn:zero-or-one
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-zero-or-one
---

# fn:zero-or-one

Asserts that a sequence contains zero or one items, raising an error otherwise.

## Signature

```
fn:zero-or-one($arg as item()*) as item()?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `item()*` | The sequence to test. |

## Return Type

`item()?`

## Description

The `fn:zero-or-one` function returns `$arg` unchanged if it contains zero or one items. If the sequence contains two or more items, a dynamic error is raised. This function is used as a runtime assertion to enforce cardinality constraints.

If `$arg` is the empty sequence, it returns the empty sequence. If `$arg` is a single item, that item is returned. The function acts as a type narrowing mechanism, converting `item()*` to `item()?`.

Use this function when you expect at most one result from an expression and want to catch cases where multiple results would indicate a data or logic error.

## Examples

```xpath
zero-or-one(//config/setting[@name='timeout'])
(: Returns the setting element, or () if none, or errors if multiple :)

zero-or-one(())
(: Returns () :)

zero-or-one('hello')
(: Returns 'hello' :)

zero-or-one((1, 2))
(: Error FORG0003 :)
```

## Error Codes

- `FORG0003` — The sequence contains more than one item.

## See Also

- [fn:one-or-more](fn-one-or-more.md)
- [fn:exactly-one](fn-exactly-one.md)
