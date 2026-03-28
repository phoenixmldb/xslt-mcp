---
name: fn:true
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-true
---

# fn:true

Returns the boolean value `true`.

## Signature

```
fn:true() as xs:boolean
```

## Parameters

None.

## Returns

`xs:boolean` value `true`.

## Examples

```xpath
true() → true()
if (true()) then "yes" else "no" → "yes"
```

## See Also

- [fn:false](fn-false.md)
- [fn:boolean](fn-boolean.md)
