---
name: fn:boolean
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-boolean
---

# fn:boolean

Computes the effective boolean value of its argument.

## Signature

```
fn:boolean($arg as item()*) as xs:boolean
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `item()*` | The value to convert to boolean. |

## Returns

The effective boolean value:
- Empty sequence: `false`
- First item is a node: `true`
- Single `xs:boolean`: its value
- Single `xs:string`: `false` if zero-length, `true` otherwise
- Single numeric: `false` if zero or `NaN`, `true` otherwise
- Otherwise: type error

## Examples

```xpath
boolean(()) → false()
boolean(0) → false()
boolean(1) → true()
boolean("") → false()
boolean("hello") → true()
boolean(//item) → true() (if any item elements exist)
```

## Error Codes

- **FORG0006**: The effective boolean value is not defined for the given type (e.g., a sequence of two or more atomic values).

## See Also

- [fn:not](fn-not.md)
- [fn:true](fn-true.md)
- [fn:false](fn-false.md)
