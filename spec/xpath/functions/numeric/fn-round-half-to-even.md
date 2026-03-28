---
name: fn:round-half-to-even
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-round-half-to-even
---

# fn:round-half-to-even

Rounds a number to the nearest value with the specified precision, using banker's rounding.

## Signature

```
fn:round-half-to-even($arg as xs:numeric?) as xs:numeric?
fn:round-half-to-even($arg as xs:numeric?, $precision as xs:integer) as xs:numeric?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:numeric?` | The number to round. |
| `$precision` | `xs:integer?` | The number of decimal digits to retain. Defaults to 0. Negative values round to powers of 10. |

## Return Type

`xs:numeric?`

## Description

The `fn:round-half-to-even` function rounds a number to the specified number of decimal places. When the value is exactly halfway between two possible rounded values, it rounds to the one whose least significant digit is even (banker's rounding). This avoids the systematic bias of always rounding 0.5 up.

If `$arg` is the empty sequence, the function returns the empty sequence. If `$precision` is 0 (the default), the result is an integer value. Negative precision values round to powers of 10: precision -1 rounds to the nearest 10, precision -2 to the nearest 100.

The result type matches the input type. This function is preferred over `fn:round` in financial calculations where unbiased rounding is required.

## Examples

```xpath
round-half-to-even(2.5)
(: Returns 2 — rounds to even :)

round-half-to-even(3.5)
(: Returns 4 — rounds to even :)

round-half-to-even(3.1415, 2)
(: Returns 3.14 :)

round-half-to-even(35, -1)
(: Returns 40 :)

round-half-to-even(25, -1)
(: Returns 20 — rounds to even :)

round-half-to-even(3.15, 1)
(: Returns 3.2 :)
```

## Error Codes

None.

## See Also

- [fn:round](fn-round.md)
- [fn:floor](fn-floor.md)
- [fn:ceiling](fn-ceiling.md)
