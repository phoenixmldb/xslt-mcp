---
name: math:atan
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-math-atan
---

# math:atan

Returns the arc tangent of the argument.

## Signature

```
math:atan($arg as xs:double?) as xs:double?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:double?` | The value. |

## Return Type

`xs:double?`

## Description

The `math:atan` function returns the arc tangent of `$arg`, in radians. The result is in the range (-pi/2, pi/2). If `$arg` is the empty sequence, the function returns the empty sequence.

For `math:asin` and `math:acos`, if the argument is outside the range [-1, 1], the result is `NaN`.

## Examples

```xpath
math:atan(0)
(: Returns the arc tangent of 0 in radians :)

math:atan(1)
(: Returns the arc tangent of 1 in radians :)
```

## Error Codes

None.

## See Also

- [math:tan](math-tan.md)
- [math:pi](math-pi.md)
