---
name: xsl:number
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#number
---

# xsl:number

Formats a number and inserts the result as a text node. Can count nodes in the source tree or format an explicit value.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `value` | No | `expression` | Expression providing the number to format. If absent, the position is computed by counting. |
| `select` | No | `expression` | The node to use as starting point for counting. Default is the context item. (Since 3.0) |
| `level` | No | `"single" \| "multiple" \| "any"` | Counting mode. Default is `single`. |
| `count` | No | `pattern` | Pattern identifying which nodes to count. |
| `from` | No | `pattern` | Pattern identifying where counting restarts. |
| `format` | No | `{ string }` | Format pattern (e.g., `1`, `a`, `A`, `i`, `I`, `01`). Default is `1`. |
| `lang` | No | `{ xs:language }` | Language for language-dependent numbering (e.g., alphabetic sequences). |
| `letter-value` | No | `{ "alphabetic" \| "traditional" }` | Numbering scheme. |
| `ordinal` | No | `{ string }` | Ordinal suffix (language-dependent). |
| `grouping-separator` | No | `{ char }` | Character used as thousands separator. |
| `grouping-size` | No | `{ xs:integer }` | Number of digits per group. |

## Content Model

Empty.

## Examples

### Simple Numbering

```xml
<xsl:template match="item">
  <p>
    <xsl:number format="1. "/>
    <xsl:value-of select="."/>
  </p>
</xsl:template>
```

### Hierarchical Numbering

```xml
<xsl:template match="section/title">
  <h2>
    <xsl:number level="multiple" count="chapter|section" format="1.1 "/>
    <xsl:value-of select="."/>
  </h2>
</xsl:template>
```

### Explicit Value

```xml
<xsl:number value="$page-num" format="i"/>
<!-- Outputs: iv (for value 4) -->
```

## Error Codes

- **XTDE0980**: The `value` is negative or NaN.
- **XTDE0990**: The format token is invalid.

## See Also

- [fn:format-number](../xpath/functions/numeric/fn-format-number.md)
- [xsl:value-of](xsl-value-of.md)
