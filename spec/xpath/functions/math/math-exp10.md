---
name: math:exp10
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-math-exp10
---

# math:exp10

Returns 10 raised to the power of the argument.

## Signature

```
math:exp10($arg as xs:double?) as xs:double?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:double?` | The exponent. |

## Return Type

`xs:double?`

## Description

The `math:exp10` function returns 10 raised to the power of `$arg`. If `$arg` is the empty sequence, the function returns the empty sequence. This function is the inverse of `math:log10`.

## Examples

```xpath
math:exp10(0)
(: Returns 1.0e0 :)

math:exp10(2)
(: Returns 100.0e0 :)

math:exp10(-1)
(: Returns 0.1e0 :)
```

## Error Codes

None.

## See Also

- [math:exp](math-exp.md)
- [math:log10](math-log10.md)
