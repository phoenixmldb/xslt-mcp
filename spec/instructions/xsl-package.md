---
name: xsl:package
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#packages
---

# xsl:package

Top-level element for a stylesheet package, providing component encapsulation and reuse.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | No | `uri` | The URI identifying this package. |
| `package-version` | No | `string` | Version number of the package (e.g., `"1.0"`, `"2.1.3"`). |
| `version` | Yes | `string` | XSLT version (e.g., `"3.0"`). |
| `declared-modes` | No | `"yes" \| "no"` | Whether undeclared modes are forbidden. Default is `yes`. |

## Content Model

Same as `xsl:stylesheet`: top-level declarations including `xsl:import`, `xsl:use-package`, `xsl:expose`, `xsl:template`, `xsl:function`, `xsl:variable`, `xsl:param`, `xsl:mode`, `xsl:output`, etc.

## Description

`xsl:package` is the XSLT 3.0 alternative to `xsl:stylesheet` for defining reusable stylesheet packages. It serves the same role as `xsl:stylesheet` but adds package-level features: named identity, versioning, component visibility, and the ability to be used by other packages via `xsl:use-package`.

A package provides encapsulation. Components within a package are private by default and must be explicitly exposed using `xsl:expose` to make them visible to consumers. This prevents unintended dependencies on internal implementation details.

The `name` attribute is a URI that uniquely identifies the package. The `package-version` supports multi-part version numbers and enables version-compatible loading via `xsl:use-package`.

When `declared-modes="yes"` (the default), every mode used in the package must have an explicit `xsl:mode` declaration. This catches typos in mode names.

A stylesheet can use `xsl:stylesheet` or `xsl:transform` instead of `xsl:package`. The main reason to use `xsl:package` is when you want to take advantage of package features (visibility, versioning, and use by other packages).

## Examples

### Reusable Utility Package

```xml
<xsl:package name="http://example.com/utils" package-version="1.0"
             version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
             xmlns:util="http://example.com/utils">

  <xsl:expose component="function" names="util:*" visibility="public"/>

  <xsl:function name="util:capitalize" as="xs:string">
    <xsl:param name="input" as="xs:string"/>
    <xsl:sequence select="upper-case(substring($input, 1, 1)) || substring($input, 2)"/>
  </xsl:function>

  <xsl:function name="util:slug" as="xs:string">
    <xsl:param name="input" as="xs:string"/>
    <xsl:sequence select="lower-case(replace(normalize-space($input), '\s+', '-'))"/>
  </xsl:function>
</xsl:package>
```

### Package with Abstract Components

```xml
<xsl:package name="http://example.com/renderer" package-version="2.0"
             version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
             xmlns:r="http://example.com/renderer">

  <xsl:expose component="function" names="r:render" visibility="final"/>
  <xsl:expose component="function" names="r:format-item" visibility="abstract"/>

  <xsl:function name="r:render" as="element()">
    <xsl:param name="items" as="item()*"/>
    <output>
      <xsl:for-each select="$items">
        <xsl:sequence select="r:format-item(.)"/>
      </xsl:for-each>
    </output>
  </xsl:function>
</xsl:package>
```

## Error Codes

- **XTSE0020**: The `version` attribute is missing.
- **XTSE3040**: Component visibility conflicts.
- **XTSE3050**: Multiple packages with the same name and version.

## See Also

- [xsl:stylesheet](xsl-stylesheet.md)
- [xsl:use-package](xsl-use-package.md)
- [xsl:expose](xsl-expose.md)
- [xsl:override](xsl-override.md)
