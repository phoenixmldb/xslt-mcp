---
name: fn:ceiling
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-ceiling
---

# fn:ceiling

Returns the smallest integer greater than or equal to the argument (rounds toward positive infinity).

## Signature

```
fn:ceiling($arg as xs:numeric?) as xs:numeric?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:numeric?` | The number to ceil. |

## Returns

The smallest integer not less than `$arg`. Returns the empty sequence if `$arg` is the empty sequence.

## Examples

```xpath
ceiling(2.1) → 3
ceiling(2.0) → 2
ceiling(-2.9) → -2
ceiling(()) → ()
```

## See Also

- [fn:floor](fn-floor.md)
- [fn:round](fn-round.md)
