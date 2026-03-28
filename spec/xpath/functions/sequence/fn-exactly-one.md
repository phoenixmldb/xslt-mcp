---
name: fn:exactly-one
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-exactly-one
---

# fn:exactly-one

Asserts that a sequence contains exactly one item, raising an error otherwise.

## Signature

```
fn:exactly-one($arg as item()*) as item()
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `item()*` | The sequence to test. |

## Return Type

`item()`

## Description

The `fn:exactly-one` function returns `$arg` if it contains exactly one item. If the sequence is empty or contains more than one item, a dynamic error is raised. This is the strictest of the three cardinality assertion functions.

The function acts as a type narrowing mechanism, converting `item()*` to `item()`. It is useful when an expression must produce exactly one result and any other cardinality indicates an error condition.

Use this function to enforce strict cardinality constraints on lookups, configuration values, or any expression where exactly one result is expected.

## Examples

```xpath
exactly-one(//config/database/@host)
(: Returns the host attribute, or errors if missing or multiple :)

exactly-one(42)
(: Returns 42 :)

exactly-one(())
(: Error FORG0005 :)

exactly-one((1, 2))
(: Error FORG0005 :)
```

## Error Codes

- `FORG0005` — The sequence does not contain exactly one item.

## See Also

- [fn:zero-or-one](fn-zero-or-one.md)
- [fn:one-or-more](fn-one-or-more.md)
