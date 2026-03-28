---
name: math:pi
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-math-pi
---

# math:pi

Returns the mathematical constant pi.

## Signature

```
math:pi() as xs:double
```

## Parameters

None.

## Return Type

`xs:double`

## Description

The `math:pi` function returns an approximation of the mathematical constant pi (the ratio of a circle's circumference to its diameter). The returned value is the `xs:double` value closest to pi, which is approximately 3.141592653589793.

This function is in the `http://www.w3.org/2005/xpath-functions/math` namespace.

## Examples

```xpath
math:pi()
(: Returns 3.141592653589793e0 :)

math:pi() * $radius * $radius
(: Computes the area of a circle :)

2 * math:pi() * $radius
(: Computes the circumference :)
```

## Error Codes

None.

## See Also

- [math:sin](math-sin.md)
- [math:cos](math-cos.md)
