---
name: xsl:mode
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#mode
---

# xsl:mode

Declares properties of a template mode, including streaming capability and built-in template behavior.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | No | `EQName` | Name of the mode. If omitted, configures the unnamed (default) mode. |
| `streamable` | No | `"yes" \| "no"` | Whether templates in this mode must conform to streaming rules. Default is `no`. |
| `on-no-match` | No | `"shallow-copy" \| "deep-copy" \| "shallow-skip" \| "deep-skip" \| "text-only-copy" \| "fail"` | Behavior when no template matches. Default is `text-only-copy`. |
| `on-multiple-match` | No | `"use-last" \| "fail"` | Behavior when multiple templates match. Default is `use-last`. |
| `visibility` | No | `"public" \| "private" \| "final"` | Visibility for package-level access control. |
| `typed` | No | `"yes" \| "no" \| "strict" \| "lax" \| "unspecified"` | Whether typed pattern matching is enabled. |
| `warning-on-no-match` | No | `"yes" \| "no"` | Whether to issue a warning when no template matches. |
| `warning-on-multiple-match` | No | `"yes" \| "no"` | Whether to issue a warning when multiple templates match. |

## Content Model

Empty.

## Description

`xsl:mode` is a top-level declaration that configures the behavior of a template mode. Prior to XSLT 3.0, mode behavior was implicit. This instruction makes it explicit and configurable.

The `on-no-match` attribute is one of the most important features. It controls what happens when `xsl:apply-templates` encounters a node with no matching template rule:

- `text-only-copy` (default) -- copies text nodes, applies templates to element children. This is the traditional XSLT behavior from the built-in templates.
- `shallow-copy` -- copies the node and applies templates to its attributes and children. This is the identity transform behavior, extremely useful for pass-through stylesheets that only modify specific nodes.
- `deep-copy` -- copies the entire subtree unchanged.
- `shallow-skip` -- skips the node, applies templates to children.
- `deep-skip` -- skips the entire subtree.
- `fail` -- raises an error.

The `streamable` attribute declares that all template rules in this mode must satisfy streaming constraints. When `streamable="yes"`, each template rule can only make one downward selection into child nodes, and the processor can evaluate the stylesheet without building the full tree in memory. This is critical for processing large documents.

## Examples

### Identity Transform Mode

```xml
<xsl:mode on-no-match="shallow-copy"/>

<xsl:template match="secret-element"/>
<!-- Everything else is copied through unchanged -->
```

### Streaming Mode

```xml
<xsl:mode name="streaming" streamable="yes" on-no-match="shallow-skip"/>

<xsl:template match="record" mode="streaming">
  <xsl:copy>
    <xsl:copy-of select="@id, @date"/>
  </xsl:copy>
</xsl:template>
```

### Strict Mode (Fail on Unhandled Nodes)

```xml
<xsl:mode on-no-match="fail" on-multiple-match="fail"/>

<!-- Every node type must have an explicit template -->
<xsl:template match="/">
  <xsl:apply-templates select="root"/>
</xsl:template>

<xsl:template match="root">
  <xsl:apply-templates select="item"/>
</xsl:template>

<xsl:template match="item">
  <xsl:value-of select="."/>
</xsl:template>
```

## Error Codes

- **XTSE3085**: A template in a streamable mode does not satisfy streaming constraints.
- **XTDE0540**: `on-no-match="fail"` and no template matches the node.
- **XTDE0545**: `on-multiple-match="fail"` and multiple templates match.

## See Also

- [xsl:template](xsl-template.md)
- [xsl:apply-templates](xsl-apply-templates.md)
- [xsl:stream](xsl-stream.md)
