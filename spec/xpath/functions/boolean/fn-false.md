---
name: fn:false
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-false
---

# fn:false

Returns the boolean value `false`.

## Signature

```
fn:false() as xs:boolean
```

## Parameters

None.

## Returns

`xs:boolean` value `false`.

## Examples

```xpath
false() → false()
if (false()) then "yes" else "no" → "no"
```

## See Also

- [fn:true](fn-true.md)
- [fn:boolean](fn-boolean.md)
