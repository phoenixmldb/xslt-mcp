---
name: fn:ends-with
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-ends-with
---

# fn:ends-with

Tests whether a string ends with a given suffix.

## Signature

```
fn:ends-with($arg1 as xs:string?, $arg2 as xs:string?) as xs:boolean
fn:ends-with($arg1 as xs:string?, $arg2 as xs:string?, $collation as xs:string) as xs:boolean
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg1` | `xs:string?` | The string to test. |
| `$arg2` | `xs:string?` | The suffix to test for. |
| `$collation` | `xs:string` | Collation URI. |

## Returns

`true` if `$arg1` ends with `$arg2`; `false` otherwise. Returns `true` if `$arg2` is the zero-length string or empty sequence.

## Examples

```xpath
ends-with("hello world", "world") → true()
ends-with("hello.xml", ".xml") → true()
ends-with("hello", "xyz") → false()
```

## See Also

- [fn:starts-with](fn-starts-with.md)
- [fn:contains](fn-contains.md)
