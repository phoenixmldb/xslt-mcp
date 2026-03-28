---
name: fn:contains
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-contains
---

# fn:contains

Tests whether one string contains another as a substring.

## Signature

```
fn:contains($arg1 as xs:string?, $arg2 as xs:string?) as xs:boolean
fn:contains($arg1 as xs:string?, $arg2 as xs:string?, $collation as xs:string) as xs:boolean
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg1` | `xs:string?` | The string to search in. Empty sequence is treated as zero-length string. |
| `$arg2` | `xs:string?` | The substring to search for. Empty sequence is treated as zero-length string. |
| `$collation` | `xs:string` | Collation URI. |

## Returns

`true` if `$arg1` contains `$arg2` as a substring; `false` otherwise. Returns `true` if `$arg2` is the zero-length string.

## Examples

```xpath
contains("hello world", "world") → true()
contains("hello", "xyz") → false()
contains("hello", "") → true()
contains((), "test") → false()
```

## See Also

- [fn:starts-with](fn-starts-with.md)
- [fn:ends-with](fn-ends-with.md)
- [fn:matches](fn-matches.md)
