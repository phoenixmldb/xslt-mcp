---
name: xsl:namespace-alias
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#namespace-aliasing
---

# xsl:namespace-alias

Maps a namespace prefix used in the stylesheet to a different namespace in the output.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `stylesheet-prefix` | Yes | `NCName \| "#default"` | The prefix used in the stylesheet. |
| `result-prefix` | Yes | `NCName \| "#default"` | The prefix (and its namespace) to use in the output. |

## Content Model

Empty.

## Description

`xsl:namespace-alias` is a top-level declaration that remaps a namespace from the stylesheet's perspective to the output's perspective. This is primarily used when writing stylesheets that generate other stylesheets (or other XSLT/XSL-FO/XML documents that use the XSLT namespace).

The problem it solves: if a stylesheet needs to output elements in the XSLT namespace (for example, generating another stylesheet), the literal XSLT elements in the generating stylesheet would be interpreted as instructions rather than literal output. By using a temporary alias namespace in the stylesheet and mapping it to the XSLT namespace in the output, this conflict is avoided.

The `#default` keyword refers to the default namespace (no prefix). Both `stylesheet-prefix` and `result-prefix` reference prefixes that must be declared as namespace bindings in scope at the `xsl:namespace-alias` element.

## Examples

### Generating an XSLT Stylesheet

```xml
<xsl:stylesheet version="3.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:out="http://www.w3.org/1999/XSL/Transform/alias">

  <xsl:namespace-alias stylesheet-prefix="out" result-prefix="xsl"/>

  <xsl:template match="fields">
    <out:stylesheet version="3.0">
      <out:template match="/">
        <html>
          <xsl:for-each select="field">
            <out:value-of select="{@xpath}"/>
          </xsl:for-each>
        </html>
      </out:template>
    </out:stylesheet>
  </xsl:template>
</xsl:stylesheet>
```

## Error Codes

- **XTSE0810**: The `stylesheet-prefix` is not declared in scope.
- **XTSE0812**: The `result-prefix` is not declared in scope.

## See Also

- [xsl:namespace](xsl-namespace.md)
- [xsl:element](xsl-element.md)
