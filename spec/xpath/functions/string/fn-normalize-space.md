---
name: fn:normalize-space
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-normalize-space
---

# fn:normalize-space

Strips leading and trailing whitespace and collapses internal sequences of whitespace to a single space character.

## Signature

```
fn:normalize-space() as xs:string
fn:normalize-space($arg as xs:string?) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:string?` | The string to normalize. If omitted, uses the string value of the context item. |

## Returns

The normalized string. Whitespace characters (space, tab, newline, carriage return) are collapsed.

## Examples

```xpath
normalize-space("  hello   world  ") → "hello world"
normalize-space("no-change") → "no-change"
normalize-space(()) → ""
```

## Error Codes

- **XPDY0002**: The context item is absent when called with no argument.

## See Also

- [fn:string](fn-string.md)
- [fn:translate](fn-translate.md)
