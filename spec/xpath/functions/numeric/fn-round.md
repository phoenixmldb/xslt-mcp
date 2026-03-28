---
name: fn:round
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-round
---

# fn:round

Rounds a number to the nearest integer, or to a specified number of decimal places.

## Signature

```
fn:round($arg as xs:numeric?) as xs:numeric?
fn:round($arg as xs:numeric?, $precision as xs:integer) as xs:numeric?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:numeric?` | The number to round. |
| `$precision` | `xs:integer` | Number of decimal places. Default is 0. Negative values round to the left of the decimal point. |

## Returns

The rounded value. Rounds half away from zero (0.5 rounds to 1, -0.5 rounds to -1).

## Examples

```xpath
round(2.5) → 3
round(2.4) → 2
round(-2.5) → -2
round(3.1415, 2) → 3.14
round(1234, -2) → 1200
round(()) → ()
```

## See Also

- [fn:floor](fn-floor.md)
- [fn:ceiling](fn-ceiling.md)
- [fn:abs](fn-abs.md)
- [fn:format-number](fn-format-number.md)
