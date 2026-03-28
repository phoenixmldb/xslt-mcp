---
name: fn:format-time
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-format-time
---

# fn:format-time

Formats an xs:time value as a string using a picture string.

## Signature

```
fn:format-time($value as xs:time?, $picture as xs:string) as xs:string?
fn:format-time($value as xs:time?, $picture as xs:string, $language as xs:string?, $calendar as xs:string?, $place as xs:string?) as xs:string?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$value` | `xs:time?` | The time to format. |
| `$picture` | `xs:string` | The picture string describing the output format. |
| `$language` | `xs:string?` | The language for names. |
| `$calendar` | `xs:string?` | The calendar system to use. |
| `$place` | `xs:string?` | The place for timezone interpretation. |

## Return Type

`xs:string?`

## Description

The `fn:format-time` function formats an `xs:time` value according to a picture string. The picture string uses component specifiers: `[H]` for hours (0-23), `[h]` for hours (1-12), `[m]` for minutes, `[s]` for seconds, `[f]` for fractional seconds, `[P]` for AM/PM, `[Z]` for timezone, `[z]` for timezone name.

Formatting modifiers work as with `fn:format-date`: `[H01]` gives zero-padded 24-hour time, `[h1]` gives unpadded 12-hour time, `[m01]` gives zero-padded minutes, `[s01]` gives zero-padded seconds.

If `$value` is the empty sequence, the function returns the empty sequence.

## Examples

```xpath
format-time(xs:time('14:30:00'), '[H01]:[m01]:[s01]')
(: Returns "14:30:00" :)

format-time(xs:time('14:30:00'), '[h]:[m01] [P]')
(: Returns "2:30 PM" :)

format-time(xs:time('09:05:02'), '[H01]:[m01]')
(: Returns "09:05" :)
```

## Error Codes

- `FOFD1340` — The picture string is invalid.
- `FOFD1350` — The component is not available for the given data type.

## See Also

- [fn:format-dateTime](fn-format-dateTime.md)
- [fn:format-date](fn-format-date.md)
