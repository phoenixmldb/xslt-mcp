---
name: fn:substring-after
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-substring-after
---

# fn:substring-after

Returns the part of a string after the first occurrence of a substring.

## Signature

```
fn:substring-after($arg1 as xs:string?, $arg2 as xs:string?) as xs:string
fn:substring-after($arg1 as xs:string?, $arg2 as xs:string?, $collation as xs:string) as xs:string
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

The `fn:substring-after` function returns the portion of `$arg1` that follows the first occurrence of `$arg2`. If `$arg2` is not found in `$arg1`, the function returns the zero-length string. If `$arg2` is the zero-length string, the function returns `$arg1`.

If either argument is the empty sequence, it is treated as the zero-length string. The collation determines how the substring match is performed.

This function is commonly used for extracting suffixes, parsing delimited data, and splitting strings at known delimiters.

## Examples

```xpath
substring-after('hello-world', '-')
(: Returns "world" :)

substring-after('2024-03-15', '-')
(: Returns "03-15" :)

substring-after('name=value', '=')
(: Returns "value" :)

substring-after('no match', 'xyz')
(: Returns "" :)
```

## Error Codes

- `FOCH0002` — The collation URI is not supported.

## See Also

- [fn:substring-before](fn-substring-before.md)
- [fn:substring](fn-substring.md)
- [fn:contains](fn-contains.md)
