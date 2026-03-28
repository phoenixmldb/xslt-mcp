---
name: fn:starts-with
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-starts-with
---

# fn:starts-with

Tests whether a string starts with a given prefix.

## Signature

```
fn:starts-with($arg1 as xs:string?, $arg2 as xs:string?) as xs:boolean
fn:starts-with($arg1 as xs:string?, $arg2 as xs:string?, $collation as xs:string) as xs:boolean
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg1` | `xs:string?` | The string to test. |
| `$arg2` | `xs:string?` | The prefix to test for. |
| `$collation` | `xs:string` | Collation URI. |

## Returns

`true` if `$arg1` starts with `$arg2`; `false` otherwise. Returns `true` if `$arg2` is the zero-length string or empty sequence.

## Examples

```xpath
starts-with("hello world", "hello") → true()
starts-with("hello", "world") → false()
starts-with("hello", "") → true()
```

## See Also

- [fn:ends-with](fn-ends-with.md)
- [fn:contains](fn-contains.md)
