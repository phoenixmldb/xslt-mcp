---
name: xsl:use-package
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#use-package
---

# xsl:use-package

Imports an XSLT package for use in the current stylesheet.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | Yes | `uri` | The URI identifying the package to use. |
| `package-version` | No | `string` | Required version or version range (e.g., `"1.0"`, `"2.*"`). |

## Content Model

( `xsl:accept`*, `xsl:override`? )

## Description

`xsl:use-package` is a top-level declaration that imports a named XSLT package into the current stylesheet. Public and final components from the package become available in the using stylesheet.

The `name` attribute identifies the package by its URI (as declared in the package's `xsl:package/@name`). The `package-version` attribute can specify an exact version or a version range using wildcards (e.g., `"2.*"` matches any version 2.x).

Components from the used package are accessed by their names. Public components can be overridden within `xsl:override`. Final components can be used but not overridden. Private components are not visible.

`xsl:accept` children (if present) can selectively accept or reject components from the package, providing fine-grained control over which components are imported.

The package loading mechanism is implementation-defined. Processors typically support configuration to map package URIs to file locations.

## Examples

### Using a Utility Package

```xml
<xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:util="http://example.com/utils">

  <xsl:use-package name="http://example.com/utils" package-version="1.0"/>

  <xsl:template match="title">
    <h1><xsl:value-of select="util:capitalize(.)"/></h1>
  </xsl:template>
</xsl:stylesheet>
```

### Using with Override

```xml
<xsl:use-package name="http://example.com/base-layout" package-version="3.*">
  <xsl:override>
    <xsl:template name="footer">
      <footer>Custom footer content</footer>
    </xsl:template>
  </xsl:override>
</xsl:use-package>
```

### Version Range

```xml
<xsl:use-package name="http://example.com/formatting" package-version="2.*"/>
```

## Error Codes

- **XTSE3000**: The named package cannot be found.
- **XTSE3005**: No version matching the version constraint is available.
- **XTSE3080**: An abstract component in the package is not overridden.

## See Also

- [xsl:package](xsl-package.md)
- [xsl:override](xsl-override.md)
- [xsl:expose](xsl-expose.md)
