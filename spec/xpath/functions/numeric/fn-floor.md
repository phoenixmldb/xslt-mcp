---
name: fn:floor
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-floor
---

# fn:floor

Returns the largest integer less than or equal to the argument (rounds toward negative infinity).

## Signature

```
fn:floor($arg as xs:numeric?) as xs:numeric?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:numeric?` | The number to floor. |

## Returns

The largest integer not greater than `$arg`. Returns the empty sequence if `$arg` is the empty sequence.

## Examples

```xpath
floor(2.9) → 2
floor(2.0) → 2
floor(-2.1) → -3
floor(()) → ()
```

## See Also

- [fn:ceiling](fn-ceiling.md)
- [fn:round](fn-round.md)
