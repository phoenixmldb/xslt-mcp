---
name: math:log
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-math-log
---

# math:log

Returns the natural logarithm of the argument.

## Signature

```
math:log($arg as xs:double?) as xs:double?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:double?` | The value. |

## Return Type

`xs:double?`

## Description

The `math:log` function returns the natural (base-e) logarithm of `$arg`. If `$arg` is the empty sequence, the function returns the empty sequence. For negative values, the result is `NaN`. For zero, the result is `-INF`. This function is the inverse of `math:exp`.

## Examples

```xpath
math:log(1)
(: Returns 0.0e0 :)

math:log(math:exp(1))
(: Returns 1.0e0 :)

math:log(0)
(: Returns -INF :)

math:log(-1)
(: Returns NaN :)
```

## Error Codes

None.

## See Also

- [math:log10](math-log10.md)
- [math:exp](math-exp.md)
