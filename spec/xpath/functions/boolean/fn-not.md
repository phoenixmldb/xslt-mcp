---
name: fn:not
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-not
---

# fn:not

Returns the boolean negation of the effective boolean value of its argument.

## Signature

```
fn:not($arg as item()*) as xs:boolean
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `item()*` | The value to negate. |

## Returns

`true` if the effective boolean value of `$arg` is `false`; `false` otherwise.

## Examples

```xpath
not(true()) → false()
not(false()) → true()
not(()) → true()
not(0) → true()
not("hello") → false()
not(//nonexistent) → true()
```

## Error Codes

- **FORG0006**: The effective boolean value cannot be computed.

## See Also

- [fn:boolean](fn-boolean.md)
- [fn:true](fn-true.md)
- [fn:false](fn-false.md)
