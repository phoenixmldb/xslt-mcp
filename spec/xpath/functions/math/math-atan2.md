---
name: math:atan2
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-math-atan2
---

# math:atan2

Returns the angle in radians subtended at the origin by the point (x, y) and the positive x-axis.

## Signature

```
math:atan2($y as xs:double, $x as xs:double) as xs:double
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$y` | `xs:double` | The y-coordinate. |
| `$x` | `xs:double` | The x-coordinate. |

## Return Type

`xs:double`

## Description

The `math:atan2` function returns the angle in radians between the positive x-axis and the point ($x, $y), in the range (-pi, pi]. This is the two-argument form of arc tangent, which correctly handles all four quadrants and the case where `$x` is zero.

Unlike `math:atan`, which only returns values in (-pi/2, pi/2), `math:atan2` uses both arguments to determine the correct quadrant of the angle. This function never returns the empty sequence because neither argument is optional.

## Examples

```xpath
math:atan2(0, 1)
(: Returns 0.0e0 :)

math:atan2(1, 0)
(: Returns 1.5707963267948966e0 — pi/2 :)

math:atan2(-1, 0)
(: Returns -1.5707963267948966e0 — -pi/2 :)

math:atan2(1, 1)
(: Returns 0.7853981633974483e0 — pi/4 :)
```

## Error Codes

None.

## See Also

- [math:atan](math-atan.md)
- [math:pi](math-pi.md)
