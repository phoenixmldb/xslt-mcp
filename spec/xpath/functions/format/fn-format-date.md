---
name: fn:format-date
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-format-date
---

# fn:format-date

Formats an xs:date value as a string using a picture string.

## Signature

```
fn:format-date($value as xs:date?, $picture as xs:string) as xs:string?
fn:format-date($value as xs:date?, $picture as xs:string, $language as xs:string?, $calendar as xs:string?, $place as xs:string?) as xs:string?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$value` | `xs:date?` | The date to format. |
| `$picture` | `xs:string` | The picture string describing the output format. |
| `$language` | `xs:string?` | The language for names (e.g., month names). |
| `$calendar` | `xs:string?` | The calendar system to use. |
| `$place` | `xs:string?` | The place for timezone interpretation. |

## Return Type

`xs:string?`

## Description

The `fn:format-date` function formats an `xs:date` value according to a picture string. The picture string uses component specifiers in square brackets: `[Y]` for year, `[M]` for month, `[D]` for day, `[d]` for day of year, `[F]` for day of week, `[W]` for week of year, `[w]` for week of month. Characters outside brackets are literal output.

Each component specifier can include formatting modifiers: `[M01]` produces zero-padded month number, `[MNn]` produces capitalized month name, `[D1]` produces unpadded day number, `[Y0001]` produces four-digit year. The presentation modifier `N` gives names, `n` gives lowercase names, `Nn` gives title-case names.

If `$value` is the empty sequence, the function returns the empty sequence. The language parameter affects month and day names; the calendar parameter selects alternative calendars.

## Examples

```xpath
format-date(xs:date('2024-03-15'), '[Y0001]-[M01]-[D01]')
(: Returns "2024-03-15" :)

format-date(xs:date('2024-03-15'), '[D] [MNn] [Y]')
(: Returns "15 March 2024" :)

format-date(xs:date('2024-03-15'), '[FNn], [D1o] [MNn] [Y]', 'en', (), ())
(: Returns "Friday, 15th March 2024" :)

format-date(xs:date('2024-03-15'), '[M01]/[D01]/[Y0001]')
(: Returns "03/15/2024" :)
```

## Error Codes

- `FOFD1340` — The picture string is invalid.
- `FOFD1350` — The component is not available for the given data type.

## See Also

- [fn:format-dateTime](fn-format-dateTime.md)
- [fn:format-time](fn-format-time.md)
- [fn:format-integer](fn-format-integer.md)
