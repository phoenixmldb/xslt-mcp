---
name: fn:format-integer
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-format-integer
---

# fn:format-integer

Formats an integer using a picture string.

## Signature

```
fn:format-integer($value as xs:integer?, $picture as xs:string) as xs:string
fn:format-integer($value as xs:integer?, $picture as xs:string, $lang as xs:string?) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$value` | `xs:integer?` | The integer to format. |
| `$picture` | `xs:string` | The picture string. |
| `$lang` | `xs:string?` | The language for word-based formatting. |

## Return Type

`xs:string`

## Description

The `fn:format-integer` function formats an integer according to a picture string. The picture `1` produces decimal digits, `01` produces zero-padded, `A` produces uppercase alphabetic (A, B, ..., Z, AA, ...), `a` produces lowercase alphabetic, `I` produces uppercase Roman numerals, `i` produces lowercase Roman numerals, `w` produces words, `W` produces uppercase words, and `Ww` produces title-case words.

The ordinal modifier `o` appended to the picture produces ordinal numbers: `1o` gives "1st", "2nd", etc. Grouping separators can be included: `#,##0` gives comma-separated groups.

If `$value` is the empty sequence, the function returns an empty string.

## Examples

```xpath
format-integer(42, '1')
(: Returns "42" :)

format-integer(42, '001')
(: Returns "042" :)

format-integer(3, 'I')
(: Returns "III" :)

format-integer(3, 'a')
(: Returns "c" :)

format-integer(1, 'Ww', 'en')
(: Returns "One" :)

format-integer(1500000, '#,##0')
(: Returns "1,500,000" :)

format-integer(3, '1o', 'en')
(: Returns "3rd" :)
```

## Error Codes

- `FODF1310` — The picture string is invalid.

## See Also

- [fn:format-number](../numeric/fn-format-number.md)
- [fn:format-date](fn-format-date.md)
