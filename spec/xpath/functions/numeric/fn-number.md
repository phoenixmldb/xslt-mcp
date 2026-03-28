---
name: fn:number
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-number
---

# fn:number

Converts an atomic value to `xs:double`.

## Signature

```
fn:number() as xs:double
fn:number($arg as xs:anyAtomicType?) as xs:double
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:anyAtomicType?` | The value to convert. If omitted, uses the context item. |

## Returns

The value converted to `xs:double`. Returns `NaN` if conversion is not possible. Returns `NaN` for the empty sequence.

## Examples

```xpath
number("42.5") → 42.5
number("abc") → NaN
number(true()) → 1.0
number(false()) → 0.0
```

## Error Codes

- **XPDY0002**: The context item is absent when called with no argument.
- **FOTY0014**: `$arg` is a function item.

## See Also

- [fn:string](../string/fn-string.md)
- [fn:round](fn-round.md)
