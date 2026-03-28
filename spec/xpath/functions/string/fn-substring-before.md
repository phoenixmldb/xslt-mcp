---
name: fn:substring-before
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-substring-before
---

# fn:substring-before

Returns the part of a string before the first occurrence of a substring.

## Signature

```
fn:substring-before($arg1 as xs:string?, $arg2 as xs:string?) as xs:string
fn:substring-before($arg1 as xs:string?, $arg2 as xs:string?, $collation as xs:string) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg1` | `xs:string?` | The string to search in. |
| `$arg2` | `xs:string?` | The substring to search for. |
| `$collation` | `xs:string?` | The collation to use. |

## Return Type

`xs:string`

## Description

The `fn:substring-before` function returns the portion of `$arg1` that precedes the first occurrence of `$arg2`. If `$arg2` is not found in `$arg1`, or if `$arg2` is the zero-length string, the function returns the zero-length string.

If either argument is the empty sequence, it is treated as the zero-length string. The collation determines how the substring match is performed.

This function is commonly used for parsing delimited strings, extracting prefixes, and splitting strings at known markers.

## Examples

```xpath
substring-before('hello-world', '-')
(: Returns "hello" :)

substring-before('2024-03-15', '-')
(: Returns "2024" :)

substring-before('no match', 'xyz')
(: Returns "" :)

substring-before('abc', '')
(: Returns "" :)
```

## Error Codes

- `FOCH0002` — The collation URI is not supported.

## See Also

- [fn:substring-after](fn-substring-after.md)
- [fn:substring](fn-substring.md)
- [fn:contains](fn-contains.md)
