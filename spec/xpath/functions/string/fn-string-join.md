---
name: fn:string-join
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-string-join
---

# fn:string-join

Joins a sequence of strings with a separator.

## Signature

```
fn:string-join($arg1 as xs:anyAtomicType*) as xs:string
fn:string-join($arg1 as xs:anyAtomicType*, $arg2 as xs:string) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg1` | `xs:anyAtomicType*` | Sequence of values to join. Each is cast to `xs:string`. |
| `$arg2` | `xs:string` | Separator string. Default is zero-length string. |

## Returns

A string formed by concatenating the string values of `$arg1`, with `$arg2` inserted between adjacent items.

## Examples

```xpath
string-join(("a", "b", "c"), ", ") → "a, b, c"
string-join(("x", "y", "z")) → "xyz"
string-join((), ", ") → ""
string-join(1 to 5, "-") → "1-2-3-4-5"
```

## See Also

- [fn:concat](fn-concat.md)
- [fn:tokenize](fn-tokenize.md)
