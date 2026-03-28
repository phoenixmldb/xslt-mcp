---
name: math:sqrt
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-math-sqrt
---

# math:sqrt

Returns the non-negative square root of the argument.

## Signature

```
math:sqrt($arg as xs:double?) as xs:double?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:double?` | The value. |

## Return Type

`xs:double?`

## Description

The `math:sqrt` function returns the non-negative square root of `$arg`. If `$arg` is the empty sequence, the function returns the empty sequence. For negative values, the result is `NaN`. This is equivalent to `math:pow($arg, 0.5)`.

## Examples

```xpath
math:sqrt(4)
(: Returns 2.0e0 :)

math:sqrt(2)
(: Returns 1.4142135623730951e0 :)

math:sqrt(0)
(: Returns 0.0e0 :)

math:sqrt(-1)
(: Returns NaN :)
```

## Error Codes

None.

## See Also

- [math:pow](math-pow.md)
