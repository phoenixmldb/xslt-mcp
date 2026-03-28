---
name: xsl:decimal-format
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#decimal-format
---

# xsl:decimal-format

Declares a decimal format used by the `fn:format-number()` function.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | No | `EQName` | Name of this format. If omitted, defines the default format. |
| `decimal-separator` | No | `char` | Character for the decimal point. Default `.`. |
| `grouping-separator` | No | `char` | Character for digit grouping. Default `,`. |
| `infinity` | No | `string` | String representing infinity. Default `Infinity`. |
| `minus-sign` | No | `char` | Character for minus. Default `-`. |
| `NaN` | No | `string` | String representing not-a-number. Default `NaN`. |
| `percent` | No | `char` | Character for percent. Default `%`. |
| `per-mille` | No | `char` | Character for per-mille. Default `\u2030`. |
| `zero-digit` | No | `char` | Character for zero digit. Default `0`. |
| `digit` | No | `char` | Character for optional digit placeholder. Default `#`. |
| `pattern-separator` | No | `char` | Character separating positive and negative subpatterns. Default `;`. |
| `exponent-separator` | No | `char` | Character separating mantissa from exponent. Default `e`. Added in XSLT 3.0. |

## Content Model

Empty.

## Description

`xsl:decimal-format` is a top-level declaration that configures how numbers are formatted by the `fn:format-number()` function. Each attribute defines a character or string used in the formatting picture and the resulting output.

If the `name` attribute is omitted, the declaration defines the default decimal format. If a name is given, the format is referenced in `format-number()` as the third argument: `format-number($value, $picture, $name)`.

Multiple `xsl:decimal-format` declarations with the same name are allowed only if they have identical attribute values (allowing the same declaration to appear in multiple imported modules). Otherwise, it is a static error.

The `exponent-separator` attribute was added in XSLT 3.0 to support scientific notation formatting.

## Examples

### European Number Formatting

```xml
<xsl:decimal-format name="european"
                    decimal-separator=","
                    grouping-separator="."/>

<!-- Usage: format-number(1234567.89, '#.###,00', 'european')
     Result: 1.234.567,89 -->
```

### Default Format Override

```xml
<xsl:decimal-format decimal-separator="," grouping-separator=" "/>

<!-- Usage: format-number(1234.5, '# ##0,00')
     Result: 1 234,50 -->
```

## Error Codes

- **XTSE1290**: Two decimal format declarations with the same name have conflicting attribute values.
- **XTSE1295**: A character attribute value is not a single character.

## See Also

- [xsl:number](xsl-number.md)
