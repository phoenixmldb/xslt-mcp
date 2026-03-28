---
name: fn:translate
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-translate
---

# fn:translate

Replaces individual characters in a string. Each character in `$mapString` is replaced by the character at the corresponding position in `$transString`. Characters in `$mapString` with no corresponding character in `$transString` are removed.

## Signature

```
fn:translate($arg as xs:string?, $mapString as xs:string, $transString as xs:string) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:string?` | The source string. |
| `$mapString` | `xs:string` | Characters to replace. |
| `$transString` | `xs:string` | Replacement characters. |

## Returns

The translated string.

## Examples

```xpath
translate("bar", "abc", "ABC") → "BAr"
translate("--aaa--", "abc-", "ABC") → "AAA"
translate("hello", "aeiou", "AEIOU") → "hEllO"
```

## See Also

- [fn:replace](fn-replace.md)
- [fn:upper-case](fn-upper-case.md)
- [fn:lower-case](fn-lower-case.md)
