---
name: math:exp
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-math-exp
---

# math:exp

Returns e raised to the power of the argument.

## Signature

```
math:exp($arg as xs:double?) as xs:double?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:double?` | The exponent. |

## Return Type

`xs:double?`

## Description

The `math:exp` function returns the value of e (Euler's number, approximately 2.71828) raised to the power of `$arg`. If `$arg` is the empty sequence, the function returns the empty sequence.

This function computes the natural exponential function. It is the inverse of `math:log`.

## Examples

```xpath
math:exp(0)
(: Returns 1.0e0 :)

math:exp(1)
(: Returns 2.718281828459045e0 :)

math:exp(-1)
(: Returns 0.36787944117144233e0 :)
```

## Error Codes

None.

## See Also

- [math:exp10](math-exp10.md)
- [math:log](math-log.md)
- [math:pow](math-pow.md)
