---
name: fn:one-or-more
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-one-or-more
---

# fn:one-or-more

Asserts that a sequence contains one or more items, raising an error if empty.

## Signature

```
fn:one-or-more($arg as item()*) as item()+
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `item()*` | The sequence to test. |

## Return Type

`item()+`

## Description

The `fn:one-or-more` function returns `$arg` unchanged if it contains one or more items. If the sequence is empty, a dynamic error is raised. This function is used as a runtime assertion to enforce that a sequence is non-empty.

The function acts as a type narrowing mechanism, converting `item()*` to `item()+`. It is useful when an expression might return an empty sequence but the calling context requires at least one item.

Use this function when an empty result would indicate missing required data or a logic error that should fail fast rather than silently produce incorrect output.

## Examples

```xpath
one-or-more(//employee)
(: Returns all employees, or errors if none found :)

one-or-more('value')
(: Returns 'value' :)

one-or-more(())
(: Error FORG0004 :)
```

## Error Codes

- `FORG0004` — The sequence is empty.

## See Also

- [fn:zero-or-one](fn-zero-or-one.md)
- [fn:exactly-one](fn-exactly-one.md)
