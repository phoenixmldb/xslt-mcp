---
name: fn:format-dateTime
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-format-dateTime
---

# fn:format-dateTime

Formats an xs:dateTime value as a string using a picture string.

## Signature

```
fn:format-dateTime($value as xs:dateTime?, $picture as xs:string) as xs:string?
fn:format-dateTime($value as xs:dateTime?, $picture as xs:string, $language as xs:string?, $calendar as xs:string?, $place as xs:string?) as xs:string?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$value` | `xs:dateTime?` | The dateTime to format. |
| `$picture` | `xs:string` | The picture string describing the output format. |
| `$language` | `xs:string?` | The language for names. |
| `$calendar` | `xs:string?` | The calendar system to use. |
| `$place` | `xs:string?` | The place for timezone interpretation. |

## Return Type

`xs:string?`

## Description

The `fn:format-dateTime` function formats an `xs:dateTime` value according to a picture string. It combines all the component specifiers available in both `fn:format-date` and `fn:format-time`: `[Y]`, `[M]`, `[D]`, `[d]`, `[F]`, `[W]`, `[w]` for the date portion, and `[H]`, `[h]`, `[m]`, `[s]`, `[f]`, `[P]`, `[Z]`, `[z]` for the time portion.

Characters outside square brackets are treated as literal text. To include a literal bracket, double it: `[[` produces `[`. Each component specifier supports width and presentation modifiers.

If `$value` is the empty sequence, the function returns the empty sequence.

## Examples

```xpath
format-dateTime(current-dateTime(), '[Y0001]-[M01]-[D01]T[H01]:[m01]:[s01]')
(: Returns ISO-style dateTime like "2024-03-15T14:30:00" :)

format-dateTime(current-dateTime(), '[FNn] [D] [MNn] [Y] at [h]:[m01][Pn]')
(: Returns "Friday 15 March 2024 at 2:30pm" :)

format-dateTime(current-dateTime(), '[D01]/[M01]/[Y0001] [H01]:[m01]')
(: Returns "15/03/2024 14:30" :)
```

## Error Codes

- `FOFD1340` — The picture string is invalid.
- `FOFD1350` — The component is not available for the given data type.

## See Also

- [fn:format-date](fn-format-date.md)
- [fn:format-time](fn-format-time.md)
