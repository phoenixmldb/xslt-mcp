---
name: xsl:text
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#xsl-text
---

# xsl:text

Creates a text node in the result tree with explicit control over whitespace. Unlike literal text, whitespace within `xsl:text` is always preserved.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `disable-output-escaping` | No | `"yes" \| "no"` | Disables escaping of special characters (`<`, `&`, etc.). Default is `no`. Use with caution. |

## Content Model

Text only (no child elements).

## Examples

### Preserving Whitespace

```xml
<xsl:text>  Two leading spaces preserved  </xsl:text>
```

### Explicit Newlines

```xml
<xsl:value-of select="$name"/>
<xsl:text>&#10;</xsl:text>
<xsl:value-of select="$address"/>
```

### Disable Output Escaping

```xml
<xsl:text disable-output-escaping="yes">&lt;br/&gt;</xsl:text>
<!-- Outputs: <br/> (not escaped) -->
```

## Error Codes

- **XTSE0010**: The element contains child elements (only text content is allowed).

## See Also

- [xsl:value-of](xsl-value-of.md)
- [xsl:sequence](xsl-sequence.md)
