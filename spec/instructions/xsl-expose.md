---
name: xsl:expose
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#expose
---

# xsl:expose

Controls the visibility of components in a stylesheet package.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `component` | Yes | `"template" \| "function" \| "variable" \| "attribute-set" \| "mode" \| "*"` | The kind of component whose visibility is being set. |
| `names` | Yes | `name-tests` | Pattern matching component names. Use `*` for all names. |
| `visibility` | Yes | `"public" \| "private" \| "final" \| "abstract"` | The visibility to assign. |

## Content Model

Empty.

## Description

`xsl:expose` appears as a child of `xsl:package` and determines which components are visible to stylesheets that use the package. By default, all components in a package are private. `xsl:expose` overrides this default for matching components.

The visibility values have the following meanings:

- `public` -- the component is visible and can be overridden by the using package.
- `private` -- the component is not visible outside the package.
- `final` -- the component is visible but cannot be overridden.
- `abstract` -- the component has no implementation and must be overridden by the using package.

The `names` attribute uses name tests (same syntax as match patterns for names) to select which components are affected. For example, `names="*"` selects all components of the given kind, while `names="my:*"` selects all components in the `my` namespace.

Multiple `xsl:expose` elements can appear in a package. They are processed in order, and later declarations can override earlier ones for the same component.

## Examples

### Public Package API

```xml
<xsl:package name="http://example.com/utils" package-version="2.0"
             version="3.0">
  <xsl:expose component="function" names="util:*" visibility="public"/>
  <xsl:expose component="template" names="util:render" visibility="final"/>
  <xsl:expose component="mode" names="util:process" visibility="public"/>
  <xsl:expose component="variable" names="*" visibility="private"/>

  <!-- Package contents -->
</xsl:package>
```

### Abstract Components

```xml
<xsl:expose component="function" names="plugin:transform" visibility="abstract"/>
```

## Error Codes

- **XTSE3040**: An `xsl:expose` declaration conflicts with another for the same component.
- **XTSE0010**: `xsl:expose` appears outside `xsl:package`.

## See Also

- [xsl:package](xsl-package.md)
- [xsl:use-package](xsl-use-package.md)
- [xsl:override](xsl-override.md)
