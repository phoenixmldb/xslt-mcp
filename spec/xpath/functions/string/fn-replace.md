---
name: fn:replace
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-replace
---

# fn:replace

Replaces occurrences of a regular expression pattern in a string with a replacement string.

## Signature

```
fn:replace($input as xs:string?, $pattern as xs:string, $replacement as xs:string) as xs:string
fn:replace($input as xs:string?, $pattern as xs:string, $replacement as xs:string, $flags as xs:string) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$input` | `xs:string?` | The input string. Empty sequence is treated as zero-length string. |
| `$pattern` | `xs:string` | Regular expression pattern to match. |
| `$replacement` | `xs:string` | Replacement string. Use `$1`, `$2`, etc. for captured groups. Use `\\` for literal backslash. |
| `$flags` | `xs:string` | Regular expression flags: `s` (dot-all), `m` (multi-line), `i` (case-insensitive), `x` (comments). |

## Returns

The string with all matching substrings replaced.

## Examples

```xpath
replace("abcdef", "cd", "XX") → "abXXef"
replace("2024-01-15", "(\d{4})-(\d{2})-(\d{2})", "$2/$3/$1") → "01/15/2024"
replace("hello   world", "\s+", " ") → "hello world"
replace("HELLO", "hello", "world", "i") → "world"
```

## Error Codes

- **FORX0002**: The `$pattern` is not a valid regular expression.
- **FORX0003**: The `$pattern` matches a zero-length string.
- **FORX0004**: The `$replacement` string is invalid (e.g., unescaped `$` or `\`).

## See Also

- [fn:matches](fn-matches.md)
- [fn:tokenize](fn-tokenize.md)
- [fn:translate](fn-translate.md)
