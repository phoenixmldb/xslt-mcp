---
name: fn:string-length
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-string-length
---

# fn:string-length

Returns the number of characters in a string.

## Signature

```
fn:string-length() as xs:integer
fn:string-length($arg as xs:string?) as xs:integer
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:string?` | The string to measure. If omitted, uses the string value of the context item. |

## Returns

The number of characters (Unicode code points) in `$arg`. Returns 0 for the empty sequence.

## Examples

```xpath
string-length("hello") → 5
string-length("") → 0
string-length(()) → 0
```

## Error Codes

- **XPDY0002**: The context item is absent when called with no argument.

## See Also

- [fn:substring](fn-substring.md)
- [fn:string](fn-string.md)
