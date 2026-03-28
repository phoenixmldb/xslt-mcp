---
name: xsl:param
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#param
---

# xsl:param

Declares a parameter. Can appear as a child of `xsl:template`, `xsl:function`, `xsl:iterate`, or as a top-level (stylesheet) parameter.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | Yes | `eqname` | The parameter name. |
| `select` | No | `expression` | Default value expression, used if no value is supplied by the caller. |
| `as` | No | `sequence-type` | Required type of the parameter. |
| `required` | No | `"yes" | "no"` | Whether the parameter must be supplied. Default is `no`. When `yes`, no default value may be specified. |
| `tunnel` | No | `"yes" | "no"` | Whether this is a tunnel parameter. Default is `no`. |
| `static` | No | `"yes" | "no"` | Whether the parameter is evaluated statically. Default is `no`. (Since 3.0) |
| `visibility` | No | `"public" | "private" | "final" | "abstract"` | Visibility within a package. Only for top-level parameters. (Since 3.0) |

## Content Model

When `select` is absent and `required` is `no`, the content is a sequence constructor providing the default value. When `required` is `yes`, the element must be empty and `select` must be absent.

## Examples

### Template Parameter

```xml
<xsl:template name="format-date">
  <xsl:param name="date" as="xs:date" required="yes"/>
  <xsl:param name="format" as="xs:string" select="'[D01]/[M01]/[Y0001]'"/>
  <xsl:value-of select="format-date($date, $format)"/>
</xsl:template>
```

### Stylesheet Parameter

```xml
<xsl:param name="debug" as="xs:boolean" select="false()" static="yes"/>
```

### Tunnel Parameter

```xml
<xsl:template match="chapter">
  <xsl:param name="base-uri" tunnel="yes"/>
  <div data-source="{$base-uri}">
    <xsl:apply-templates/>
  </div>
</xsl:template>
```

## Error Codes

- **XTSE0010**: `xsl:param` elements must precede other children in a template or function.
- **XTSE0660**: A required parameter has no supplied value.
- **XTTE0590**: The supplied value does not match the declared type.

## See Also

- [xsl:variable](xsl-variable.md)
- [xsl:with-param](xsl-with-param.md)
- [xsl:template](xsl-template.md)
- [xsl:function](xsl-function.md)
