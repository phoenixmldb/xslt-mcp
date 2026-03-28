---
name: fn:upper-case
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-upper-case
---

# fn:upper-case

Converts a string to uppercase using Unicode default case mappings.

## Signature

```
fn:upper-case($arg as xs:string?) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:string?` | The string to convert. Empty sequence is treated as zero-length string. |

## Returns

The string with all characters converted to their uppercase equivalents.

## Examples

```xpath
upper-case("hello") → "HELLO"
upper-case("aBcD") → "ABCD"
upper-case(()) → ""
```

## See Also

- [fn:lower-case](fn-lower-case.md)
- [fn:translate](fn-translate.md)
