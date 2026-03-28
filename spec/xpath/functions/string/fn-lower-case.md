---
name: fn:lower-case
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-lower-case
---

# fn:lower-case

Converts a string to lowercase using Unicode default case mappings.

## Signature

```
fn:lower-case($arg as xs:string?) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:string?` | The string to convert. Empty sequence is treated as zero-length string. |

## Returns

The string with all characters converted to their lowercase equivalents.

## Examples

```xpath
lower-case("HELLO") → "hello"
lower-case("aBcD") → "abcd"
lower-case(()) → ""
```

## See Also

- [fn:upper-case](fn-upper-case.md)
- [fn:translate](fn-translate.md)
