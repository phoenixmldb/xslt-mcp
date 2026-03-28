---
name: math:sin
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-math-sin
---

# math:sin

Returns the sine of an angle in radians.

## Signature

```
math:sin($radians as xs:double?) as xs:double?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$radians` | `xs:double?` | The angle in radians. |

## Return Type

`xs:double?`

## Description

The `math:sin` function returns the sine of the angle specified in radians. If `$radians` is the empty sequence, the function returns the empty sequence. The result follows IEEE 754 double-precision arithmetic rules.

For the sine function, the argument is an angle measured in radians. To convert degrees to radians, multiply by `math:pi() div 180`.

## Examples

```xpath
math:sin(0)
(: Returns the sine of 0 :)

math:sin(math:pi() div 2)
(: Returns the sine of pi/2 :)

math:sin(math:pi())
(: Returns the sine of pi :)
```

## Error Codes

None.

## See Also

- [math:asin](math-asin.md)
- [math:pi](math-pi.md)
