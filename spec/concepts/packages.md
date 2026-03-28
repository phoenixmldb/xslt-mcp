---
name: Packages
category: concept
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#packages-and-modules
---

# Packages

XSLT 3.0 introduces packages as a higher-level modularization mechanism. A package is a self-contained unit of stylesheet logic with explicit public and private interfaces.

## Structure

A package is declared using `xsl:package` (instead of `xsl:stylesheet`):

```xml
<xsl:package name="http://example.com/my-library"
    package-version="2.0"
    version="3.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <!-- Declarations with visibility control -->
  <xsl:function name="lib:helper" visibility="private">...</xsl:function>
  <xsl:function name="lib:api" visibility="public">...</xsl:function>

</xsl:package>
```

## Using Packages

Packages are imported using `xsl:use-package`:

```xml
<xsl:stylesheet version="3.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:use-package name="http://example.com/my-library"
      package-version="2.0">
    <!-- Optional overrides -->
    <xsl:override>
      <xsl:template match="item" visibility="public">
        <!-- Override the template from the package -->
      </xsl:template>
    </xsl:override>
  </xsl:use-package>

</xsl:stylesheet>
```

## Visibility

| Value | Description |
|-------|-------------|
| `public` | Accessible to using packages. Can be overridden. |
| `private` | Not accessible outside the package. |
| `final` | Accessible but cannot be overridden. |
| `abstract` | Must be overridden by the using package. |

## Package Versioning

Packages are identified by name and version. The `package-version` attribute on `xsl:use-package` can use ranges:

```xml
<xsl:use-package name="http://example.com/lib" package-version="2.*"/>
```

## See Also

- [xsl:import](../instructions/xsl-import.md)
- [xsl:include](../instructions/xsl-include.md)
- [xsl:function](../instructions/xsl-function.md)
