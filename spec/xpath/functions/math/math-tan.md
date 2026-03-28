---
name: math:tan
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-math-tan
---

# math:tan

Returns the tangent of an angle in radians.

## Signature

```
math:tan($radians as xs:double?) as xs:double?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$radians` | `xs:double?` | The angle in radians. |

## Return Type

`xs:double?`

## Description

The `math:tan` function returns the tangent of the angle specified in radians. If `$radians` is the empty sequence, the function returns the empty sequence. The result follows IEEE 754 double-precision arithmetic rules.

For the tangent function, the argument is an angle measured in radians. To convert degrees to radians, multiply by `math:pi() div 180`.

## Examples

```xpath
math:tan(0)
(: Returns the tangent of 0 :)

math:tan(math:pi() div 2)
(: Returns the tangent of pi/2 :)

math:tan(math:pi())
(: Returns the tangent of pi :)
```

## Error Codes

None.

## See Also

- [math:atan](math-atan.md)
- [math:pi](math-pi.md)
