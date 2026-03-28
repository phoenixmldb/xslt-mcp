---
name: math:pow
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-math-pow
---

# math:pow

Returns the first argument raised to the power of the second.

## Signature

```
math:pow($x as xs:double?, $y as xs:numeric) as xs:double?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$x` | `xs:double?` | The base. |
| `$y` | `xs:numeric` | The exponent. |

## Return Type

`xs:double?`

## Description

The `math:pow` function returns `$x` raised to the power `$y`. If `$x` is the empty sequence, the function returns the empty sequence. The behavior follows IEEE 754 rules for special values.

Special cases: `math:pow($x, 0)` returns 1.0 for any `$x` (including NaN). `math:pow(0, $y)` returns 0 for positive `$y` and `INF` for negative `$y`.

## Examples

```xpath
math:pow(2, 10)
(: Returns 1024.0e0 :)

math:pow(2, -1)
(: Returns 0.5e0 :)

math:pow(5, 0)
(: Returns 1.0e0 :)

math:pow(9, 0.5)
(: Returns 3.0e0 :)
```

## Error Codes

None.

## See Also

- [math:sqrt](math-sqrt.md)
- [math:exp](math-exp.md)
