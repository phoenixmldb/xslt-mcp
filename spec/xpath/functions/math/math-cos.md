---
name: math:cos
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-math-cos
---

# math:cos

Returns the cosine of an angle in radians.

## Signature

```
math:cos($radians as xs:double?) as xs:double?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$radians` | `xs:double?` | The angle in radians. |

## Return Type

`xs:double?`

## Description

The `math:cos` function returns the cosine of the angle specified in radians. If `$radians` is the empty sequence, the function returns the empty sequence. The result follows IEEE 754 double-precision arithmetic rules.

For the cosine function, the argument is an angle measured in radians. To convert degrees to radians, multiply by `math:pi() div 180`.

## Examples

```xpath
math:cos(0)
(: Returns the cosine of 0 :)

math:cos(math:pi() div 2)
(: Returns the cosine of pi/2 :)

math:cos(math:pi())
(: Returns the cosine of pi :)
```

## Error Codes

None.

## See Also

- [math:acos](math-acos.md)
- [math:pi](math-pi.md)
