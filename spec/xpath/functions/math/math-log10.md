---
name: math:log10
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-math-log10
---

# math:log10

Returns the base-10 logarithm of the argument.

## Signature

```
math:log10($arg as xs:double?) as xs:double?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:double?` | The value. |

## Return Type

`xs:double?`

## Description

The `math:log10` function returns the base-10 logarithm of `$arg`. If `$arg` is the empty sequence, the function returns the empty sequence. For negative values, the result is `NaN`. For zero, the result is `-INF`. This function is the inverse of `math:exp10`.

## Examples

```xpath
math:log10(1)
(: Returns 0.0e0 :)

math:log10(100)
(: Returns 2.0e0 :)

math:log10(1000)
(: Returns 3.0e0 :)
```

## Error Codes

None.

## See Also

- [math:log](math-log.md)
- [math:exp10](math-exp10.md)
