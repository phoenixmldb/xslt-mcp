---
name: xsl:template
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#template-element
---

# xsl:template

Defines a template rule or a named template. Template rules match nodes in the source document; named templates are invoked by `xsl:call-template`.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `match` | No* | `pattern` | Pattern identifying which nodes this template matches. Required for template rules. |
| `name` | No* | `eqname` | Name of the template for `xsl:call-template`. Required for named templates. |
| `priority` | No | `xs:decimal` | Numeric priority when multiple rules match. Overrides the default priority. |
| `mode` | No | `mode-tokens` | Space-separated list of mode names, `#default`, or `#all`. Default is `#default`. |
| `as` | No | `sequence-type` | Required type of the template result. If omitted, `item()*` is assumed. |
| `visibility` | No | `"public" | "private" | "final" | "abstract"` | Visibility within a package. (Since 3.0) |

\* At least one of `match` or `name` must be specified. Both may be present.

## Content Model

( `xsl:context-item`?, `xsl:param`*, _sequence-constructor_ )

## Examples

### Template Rule

```xml
<xsl:template match="book">
  <div class="book">
    <h2><xsl:value-of select="title"/></h2>
    <xsl:apply-templates select="chapter"/>
  </div>
</xsl:template>
```

### Named Template

```xml
<xsl:template name="page-header" as="element(header)">
  <xsl:param name="title" as="xs:string" required="yes"/>
  <header>
    <h1><xsl:value-of select="$title"/></h1>
  </header>
</xsl:template>
```

### Template with Mode

```xml
<xsl:template match="section" mode="toc">
  <li>
    <a href="#{generate-id()}"><xsl:value-of select="title"/></a>
  </li>
</xsl:template>
```

## Error Codes

- **XTSE0010**: An `xsl:template` element must have a `match` attribute, a `name` attribute, or both.
- **XTSE0500**: The pattern in `match` is invalid.
- **XTSE0520**: A named template has the same name and arity as another in the same module.
- **XTSE0540**: The default priority is ambiguous because the match pattern has multiple alternatives.
- **XTTE0505**: The result of the template does not match the required type declared in `as`.

## See Also

- [xsl:apply-templates](xsl-apply-templates.md)
- [xsl:call-template](xsl-call-template.md)
- [xsl:param](xsl-param.md)
- [Concepts: Template Rules](../concepts/template-rules.md)
- [Concepts: Modes](../concepts/modes.md)
