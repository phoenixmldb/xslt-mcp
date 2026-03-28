---
name: xsl:override
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#override
---

# xsl:override

Overrides components from a used package.

## Attributes

None.

## Content Model

( `xsl:template` | `xsl:function` | `xsl:variable` | `xsl:param` | `xsl:attribute-set` )*

## Description

`xsl:override` appears as a child of `xsl:use-package` and contains replacement definitions for components from the used package. The components in `xsl:override` replace the corresponding components from the package, provided the original components were declared with `visibility="public"` or `visibility="abstract"`.

Components declared as `final` in the package cannot be overridden. Attempting to override a `final` or `private` component is a static error.

Overriding follows substitution semantics: within the package, any reference to the original component now resolves to the override. This enables customization of package behavior without modifying the package itself.

Abstract components (declared with `visibility="abstract"`) must be overridden -- it is a static error to use a package without providing implementations for all its abstract components.

## Examples

### Overriding a Template

```xml
<xsl:use-package name="http://example.com/base-format" package-version="1.0">
  <xsl:override>
    <xsl:template name="header">
      <header class="custom">
        <h1><xsl:value-of select="$site-title"/></h1>
      </header>
    </xsl:template>
  </xsl:override>
</xsl:use-package>
```

### Implementing Abstract Components

```xml
<xsl:use-package name="http://example.com/plugin-framework" package-version="2.0">
  <xsl:override>
    <xsl:function name="plugin:transform" as="node()*">
      <xsl:param name="input" as="node()"/>
      <xsl:apply-templates select="$input" mode="custom"/>
    </xsl:function>

    <xsl:variable name="plugin:config" as="map(*)">
      <xsl:map>
        <xsl:map-entry key="'name'" select="'my-plugin'"/>
      </xsl:map>
    </xsl:variable>
  </xsl:override>
</xsl:use-package>
```

## Error Codes

- **XTSE3058**: Attempting to override a `final` component.
- **XTSE3060**: Attempting to override a `private` component.
- **XTSE3080**: An abstract component is not overridden.

## See Also

- [xsl:use-package](xsl-use-package.md)
- [xsl:package](xsl-package.md)
- [xsl:expose](xsl-expose.md)
