---
name: fn:format-number
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-format-number
---

# fn:format-number

Formats a number as a string using a picture string, following the rules of `xsl:decimal-format`.

## Signature

```
fn:format-number($value as xs:numeric?, $picture as xs:string) as xs:string
fn:format-number($value as xs:numeric?, $picture as xs:string, $decimal-format-name as xs:string?) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$value` | `xs:numeric?` | The number to format. |
| `$picture` | `xs:string` | Format picture string (e.g., `#,##0.00`). |
| `$decimal-format-name` | `xs:string?` | Name of an `xsl:decimal-format` declaration. |

## Picture String Symbols

| Symbol | Meaning |
|--------|---------|
| `0` | Mandatory digit (zero-padded) |
| `#` | Optional digit |
| `.` | Decimal separator |
| `,` | Grouping separator |
| `%` | Multiply by 100 and show as percentage |
| `‰` | Multiply by 1000 and show as per-mille |
| `;` | Separates positive and negative sub-pictures |

## Returns

The formatted string representation.

## Examples

```xpath
format-number(1234.5, '#,##0.00') → "1,234.50"
format-number(0.25, '0%') → "25%"
format-number(42, '0000') → "0042"
format-number(-3.14, '#.##;(#.##)') → "(3.14)"
format-number(1000000, '#,###') → "1,000,000"
```

## Error Codes

- **FODF1280**: The picture string is invalid.
- **FODF1310**: The decimal format name is not declared.

## See Also

- [fn:round](fn-round.md)
- [fn:number](fn-number.md)
- [xsl:number](../../instructions/xsl-number.md)
