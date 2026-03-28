---
name: fn:substring
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-substring
---

# fn:substring

Returns a portion of a string, starting at a given position with an optional length. Positions are 1-based.

## Signature

```
fn:substring($sourceString as xs:string?, $start as xs:double) as xs:string
fn:substring($sourceString as xs:string?, $start as xs:double, $length as xs:double) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$sourceString` | `xs:string?` | The source string. Empty sequence is treated as zero-length string. |
| `$start` | `xs:double` | Starting position (1-based). Rounded to nearest integer. |
| `$length` | `xs:double` | Number of characters. Rounded to nearest integer. |

## Returns

The substring of `$sourceString` starting at position `$start` for `$length` characters. If `$length` is omitted, returns from `$start` to the end.

## Examples

```xpath
substring("Hello World", 7) → "World"
substring("Hello World", 1, 5) → "Hello"
substring("12345", 2, 3) → "234"
substring("motor car", 6) → " car"
```

## See Also

- [fn:substring-before](fn-substring-before.md)
- [fn:substring-after](fn-substring-after.md)
- [fn:string-length](fn-string-length.md)
