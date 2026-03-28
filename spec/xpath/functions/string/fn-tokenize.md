---
name: fn:tokenize
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-tokenize
---

# fn:tokenize

Splits a string into a sequence of strings using a regular expression pattern as the delimiter.

## Signature

```
fn:tokenize($input as xs:string?) as xs:string*
fn:tokenize($input as xs:string?, $pattern as xs:string) as xs:string*
fn:tokenize($input as xs:string?, $pattern as xs:string, $flags as xs:string) as xs:string*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$input` | `xs:string?` | The string to tokenize. |
| `$pattern` | `xs:string` | Regular expression pattern for the delimiter. If omitted, splits on whitespace (after stripping leading/trailing whitespace). |
| `$flags` | `xs:string` | Regular expression flags. |

## Returns

A sequence of strings. Empty strings from consecutive delimiters are included unless the one-argument form is used.

## Examples

```xpath
tokenize("a, b, c", ",\s*") → ("a", "b", "c")
tokenize("one two three") → ("one", "two", "three")
tokenize("a::b::c", "::") → ("a", "b", "c")
tokenize("  hello  world  ") → ("hello", "world")
```

## Error Codes

- **FORX0002**: The `$pattern` is not a valid regular expression.
- **FORX0003**: The `$pattern` matches a zero-length string.

## See Also

- [fn:string-join](fn-string-join.md)
- [fn:replace](fn-replace.md)
- [fn:matches](fn-matches.md)
