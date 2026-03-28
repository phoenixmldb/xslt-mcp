---
name: text
category: output-method
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#text-output-method
---

# Text Output Method

Produces plain text output. Only text nodes in the result tree contribute to the output; all markup is discarded.

## Serialization Parameters

| Parameter | Default | Description |
|-----------|---------|-------------|
| `encoding` | `UTF-8` | Character encoding. |
| `media-type` | `text/plain` | MIME media type. |
| `byte-order-mark` | `no` | Whether to emit a BOM. |
| `item-separator` | (none) | Separator between top-level items. (Since 3.0) |
| `normalization-form` | `none` | Unicode normalization. |

## Behavior

- Only the string values of text nodes are output.
- No escaping is performed.
- No XML declaration, no DOCTYPE, no markup of any kind.
- Useful for generating CSV, configuration files, source code, etc.

## Examples

```xml
<xsl:output method="text" encoding="UTF-8"/>

<xsl:template match="/">
  <xsl:for-each select="employees/employee">
    <xsl:value-of select="name"/>
    <xsl:text>,</xsl:text>
    <xsl:value-of select="department"/>
    <xsl:text>&#10;</xsl:text>
  </xsl:for-each>
</xsl:template>
```

Output:
```
John Smith,Engineering
Jane Doe,Marketing
```

## See Also

- [xsl:output](../instructions/xsl-output.md)
- [XML Output Method](xml.md)
