---
name: xsl:value-of
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#value-of
---

# xsl:value-of

Creates a text node in the result tree. The value is determined by the `select` expression or the enclosed sequence constructor.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | No | `expression` | XPath expression whose string value becomes the text node. |
| `separator` | No | `{ string }` | Separator between items when the expression returns a sequence. Default is a single space when `select` is used, or zero-length string when content is used. |
| `disable-output-escaping` | No | `"yes" | "no"` | Disables escaping of special characters. Default is `no`. Use with caution. |

## Content Model

When `select` is absent, the content is a sequence constructor. When `select` is present, the element must be empty.

## Examples

### Using select

```xml
<xsl:value-of select="title"/>
```

### With Separator

```xml
<!-- Outputs: "red, green, blue" -->
<xsl:value-of select="colors/color" separator=", "/>
```

### Using Content

```xml
<xsl:value-of>
  <xsl:text>Hello, </xsl:text>
  <xsl:value-of select="$name"/>
</xsl:value-of>
```

## Error Codes

- **XTSE0870**: Both `select` and a sequence constructor are present.
- **XTTE0870**: The value of the `select` expression cannot be converted to a string.

## See Also

- [xsl:text](xsl-text.md)
- [xsl:sequence](xsl-sequence.md)
- [xsl:copy-of](xsl-copy-of.md)
