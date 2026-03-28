---
name: fn:matches
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-matches
---

# fn:matches

Tests whether a string matches a regular expression.

## Signature

```
fn:matches($input as xs:string?, $pattern as xs:string) as xs:boolean
fn:matches($input as xs:string?, $pattern as xs:string, $flags as xs:string) as xs:boolean
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$input` | `xs:string?` | The string to test. Empty sequence is treated as zero-length string. |
| `$pattern` | `xs:string` | Regular expression pattern. |
| `$flags` | `xs:string` | Flags: `s` (dot-all), `m` (multi-line), `i` (case-insensitive), `x` (comments). |

## Returns

`true` if `$input` matches `$pattern`; `false` otherwise. The match is against the entire string unless `^`/`$` anchors or `.` wildcards are used.

Note: `fn:matches` checks if the pattern occurs _anywhere_ in the input. To test the entire string, use `^...$` anchors.

## Examples

```xpath
matches("hello", "^h") → true()
matches("hello", "^Hello$", "i") → true()
matches("2024-01-15", "\d{4}-\d{2}-\d{2}") → true()
matches("abc", "xyz") → false()
```

## Error Codes

- **FORX0002**: The `$pattern` is not a valid regular expression.
- **FORX0001**: The `$flags` value is invalid.

## See Also

- [fn:replace](fn-replace.md)
- [fn:tokenize](fn-tokenize.md)
- [fn:contains](fn-contains.md)
