---
name: fn:codepoints-to-string
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-codepoints-to-string
---

# fn:codepoints-to-string

Creates a string from a sequence of Unicode codepoints.

## Signature

```
fn:codepoints-to-string($arg as xs:integer*) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:integer*` | A sequence of Unicode codepoint values. |

## Return Type

`xs:string`

## Description

The `fn:codepoints-to-string` function constructs a string from a sequence of Unicode codepoint integers. Each integer in the sequence is converted to the corresponding Unicode character, and the characters are concatenated to form the result string.

If `$arg` is the empty sequence, the function returns the zero-length string. Each integer must be a valid XML character codepoint; otherwise a dynamic error is raised.

This function is useful for constructing strings programmatically, working with character ranges, and implementing encoding/decoding operations.

## Examples

```xpath
codepoints-to-string((65, 66, 67))
(: Returns "ABC" :)

codepoints-to-string(104)
(: Returns "h" :)

codepoints-to-string(())
(: Returns "" :)

codepoints-to-string(for $i in 65 to 90 return $i)
(: Returns "ABCDEFGHIJKLMNOPQRSTUVWXYZ" :)
```

## Error Codes

- `FOCH0001` — A codepoint does not correspond to a valid XML character.

## See Also

- [fn:string-to-codepoints](fn-string-to-codepoints.md)
