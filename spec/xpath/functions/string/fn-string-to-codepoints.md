---
name: fn:string-to-codepoints
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-string-to-codepoints
---

# fn:string-to-codepoints

Returns the sequence of Unicode codepoints for a string.

## Signature

```
fn:string-to-codepoints($arg as xs:string?) as xs:integer*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:string?` | The string to convert. |

## Return Type

`xs:integer*`

## Description

The `fn:string-to-codepoints` function returns a sequence of integers representing the Unicode codepoints of each character in the input string. The first character produces the first integer, and so on.

If `$arg` is the empty sequence or the zero-length string, the function returns the empty sequence. Each returned integer is the Unicode codepoint (code value) of the corresponding character.

This function is useful for character-level processing, encoding operations, and implementing custom string algorithms.

## Examples

```xpath
string-to-codepoints('ABC')
(: Returns (65, 66, 67) :)

string-to-codepoints('hello')
(: Returns (104, 101, 108, 108, 111) :)

string-to-codepoints('')
(: Returns () :)
```

## Error Codes

None.

## See Also

- [fn:codepoints-to-string](fn-codepoints-to-string.md)
