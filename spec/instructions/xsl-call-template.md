---
name: xsl:call-template
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#call-template
---

# xsl:call-template

Invokes a named template. The context item, position, and size remain unchanged from the caller.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | Yes | `eqname` | The name of the template to call. Must match a named `xsl:template`. |

## Content Model

( `xsl:with-param`* )

## Examples

```xml
<xsl:template match="page">
  <html>
    <xsl:call-template name="page-header">
      <xsl:with-param name="title" select="@title"/>
    </xsl:call-template>
    <body>
      <xsl:apply-templates/>
    </body>
  </html>
</xsl:template>

<xsl:template name="page-header">
  <xsl:param name="title" as="xs:string"/>
  <head>
    <title><xsl:value-of select="$title"/></title>
  </head>
</xsl:template>
```

## Error Codes

- **XTSE0650**: No template with the specified name exists.
- **XTSE0660**: A required parameter is not supplied.
- **XTSE0670**: A parameter is supplied that is not declared in the target template (when the template does not accept additional parameters).
- **XTTE0590**: The supplied parameter value does not match the required type.

## See Also

- [xsl:template](xsl-template.md)
- [xsl:with-param](xsl-with-param.md)
- [xsl:param](xsl-param.md)
