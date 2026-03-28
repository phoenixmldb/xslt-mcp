---
name: fn:abs
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-abs
---

# fn:abs

Returns the absolute value of a number.

## Signature

```
fn:abs($arg as xs:numeric?) as xs:numeric?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:numeric?` | The numeric value. |

## Returns

The absolute value. Returns the empty sequence if `$arg` is the empty sequence. The result type matches the input type.

## Examples

```xpath
abs(-5) → 5
abs(5) → 5
abs(-3.14) → 3.14
abs(()) → ()
```

## See Also

- [fn:round](fn-round.md)
- [fn:floor](fn-floor.md)
- [fn:ceiling](fn-ceiling.md)
