---
name: Modes
category: concept
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#modes
---

# Modes

Modes allow the same source nodes to be processed multiple times, each time producing different results. A template rule is associated with one or more modes; it only matches nodes when processing is occurring in one of those modes.

## Declaring Modes (Since 3.0)

In XSLT 3.0, modes can be explicitly declared using `xsl:mode`:

```xml
<xsl:mode name="toc" on-no-match="shallow-skip" visibility="public"/>
<xsl:mode name="body" on-no-match="text-only-copy"/>
```

## xsl:mode Attributes

| Attribute | Values | Description |
|-----------|--------|-------------|
| `name` | `eqname` | Mode name. Use `#unnamed` or `#default` for the default mode. |
| `on-no-match` | See below | Behavior when no template matches. |
| `on-multiple-match` | `"use-last" \| "fail"` | Behavior for ambiguous matches. Default is `use-last`. |
| `visibility` | `"public" \| "private" \| "final"` | Visibility in packages. |
| `streamable` | `"yes" \| "no"` | Whether the mode must be streamable. |
| `typed` | `"yes" \| "no" \| "strict" \| "lax" \| "unspecified"` | Type validation of input nodes. |

## on-no-match Values

| Value | Description |
|-------|-------------|
| `text-only-copy` | Copy text nodes; apply-templates to element children. (Default for `#unnamed`.) |
| `shallow-copy` | Shallow-copy the node and apply-templates to attributes and children. |
| `deep-copy` | Deep-copy the node. |
| `shallow-skip` | Skip the node; apply-templates to children. |
| `deep-skip` | Skip the node and all descendants. |
| `fail` | Raise a dynamic error. |

## Special Mode Names

- `#default` — The default mode, either `#unnamed` or the value of `default-mode` on `xsl:stylesheet`.
- `#unnamed` — The unnamed mode.
- `#all` — Template applies to all modes.
- `#current` — In `xsl:apply-templates`, use the current mode.

## Examples

### Processing in Different Modes

```xml
<xsl:template match="/">
  <html>
    <nav>
      <xsl:apply-templates select="//section" mode="toc"/>
    </nav>
    <main>
      <xsl:apply-templates mode="body"/>
    </main>
  </html>
</xsl:template>

<xsl:template match="section" mode="toc">
  <li><a href="#{@id}"><xsl:value-of select="title"/></a></li>
</xsl:template>

<xsl:template match="section" mode="body">
  <section id="{@id}">
    <xsl:apply-templates mode="#current"/>
  </section>
</xsl:template>
```

## See Also

- [xsl:template](../instructions/xsl-template.md)
- [xsl:apply-templates](../instructions/xsl-apply-templates.md)
- [Template Rules](template-rules.md)
