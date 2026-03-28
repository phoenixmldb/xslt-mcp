---
name: fn:concat
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-concat
---

# fn:concat

Concatenates two or more string arguments. Accepts a variable number of arguments (minimum 2).

## Signature

```
fn:concat($arg1 as xs:anyAtomicType?, $arg2 as xs:anyAtomicType?, ...) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg1` | `xs:anyAtomicType?` | First value to concatenate. |
| `$arg2` | `xs:anyAtomicType?` | Second value to concatenate. |
| `...` | `xs:anyAtomicType?` | Additional values (variadic). |

## Returns

The string concatenation of all arguments. Each argument is cast to `xs:string`; empty sequences become zero-length strings.

## Examples

```xpath
concat("Hello", " ", "World") → "Hello World"
concat("Item: ", @name, " (", @code, ")") → "Item: Widget (W01)"
concat("Count: ", 42) → "Count: 42"
```

## Error Codes

- **XPST0017**: Fewer than two arguments supplied (static error).

## See Also

- [fn:string-join](fn-string-join.md)
- [fn:string](fn-string.md)
