---
name: xsl:with-param
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#with-param
---

# xsl:with-param

Supplies a parameter value to a template invocation. Used within `xsl:apply-templates`, `xsl:call-template`, `xsl:apply-imports`, and `xsl:next-match`.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | Yes | `eqname` | The parameter name. Must match a declared `xsl:param` in the target template. |
| `select` | No | `expression` | Expression providing the parameter value. |
| `as` | No | `sequence-type` | Required type of the value. |
| `tunnel` | No | `"yes" \| "no"` | Whether this is a tunnel parameter. Default is `no`. |

## Content Model

When `select` is absent, the content is a sequence constructor. When `select` is present, the element must be empty.

## Examples

### Basic Parameter Passing

```xml
<xsl:call-template name="render-page">
  <xsl:with-param name="title" select="@name"/>
  <xsl:with-param name="body">
    <xsl:apply-templates/>
  </xsl:with-param>
</xsl:call-template>
```

### Tunnel Parameters

```xml
<xsl:apply-templates select="section">
  <xsl:with-param name="base-uri" select="base-uri(.)" tunnel="yes"/>
</xsl:apply-templates>
```

## Error Codes

- **XTSE0670**: A parameter is supplied that is not declared in the target template.
- **XTTE0590**: The value does not match the declared type.

## See Also

- [xsl:param](xsl-param.md)
- [xsl:call-template](xsl-call-template.md)
- [xsl:apply-templates](xsl-apply-templates.md)
