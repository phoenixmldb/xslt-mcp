---
name: math:asin
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-math-asin
---

# math:asin

Returns the arc sine of the argument.

## Signature

```
math:asin($arg as xs:double?) as xs:double?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:double?` | The value. |

## Return Type

`xs:double?`

## Description

The `math:asin` function returns the arc sine of `$arg`, in radians. The result is in the range [-pi/2, pi/2]. If `$arg` is the empty sequence, the function returns the empty sequence.

For `math:asin` and `math:acos`, if the argument is outside the range [-1, 1], the result is `NaN`.

## Examples

```xpath
math:asin(0)
(: Returns the arc sine of 0 in radians :)

math:asin(1)
(: Returns the arc sine of 1 in radians :)
```

## Error Codes

None.

## See Also

- [math:sin](math-sin.md)
- [math:pi](math-pi.md)
