---
name: math:acos
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-math-acos
---

# math:acos

Returns the arc cosine of the argument.

## Signature

```
math:acos($arg as xs:double?) as xs:double?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:double?` | The value. |

## Return Type

`xs:double?`

## Description

The `math:acos` function returns the arc cosine of `$arg`, in radians. The result is in the range [0, pi]. If `$arg` is the empty sequence, the function returns the empty sequence.

For `math:asin` and `math:acos`, if the argument is outside the range [-1, 1], the result is `NaN`.

## Examples

```xpath
math:acos(0)
(: Returns the arc cosine of 0 in radians :)

math:acos(1)
(: Returns the arc cosine of 1 in radians :)
```

## Error Codes

None.

## See Also

- [math:cos](math-cos.md)
- [math:pi](math-pi.md)
