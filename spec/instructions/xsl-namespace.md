---
name: xsl:namespace
category: instruction
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#creating-namespace-nodes
---

# xsl:namespace

Creates a namespace node in the result tree.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | Yes | `NCName \| ""` | The namespace prefix. Use empty string for the default namespace. Attribute value template allowed. |
| `select` | No | `expression` | Expression producing the namespace URI. |

## Content Model

Sequence constructor (used to produce the namespace URI if `select` is absent).

## Description

`xsl:namespace` explicitly creates a namespace binding on the parent element in the result tree. The `name` attribute specifies the prefix and the value (from `select` or the body) specifies the namespace URI.

This instruction is useful when namespace bindings need to be computed dynamically, which is not possible with literal namespace declarations in the stylesheet. Common use cases include generating elements whose namespace is determined at runtime, or ensuring specific namespace declarations appear on result elements.

If `name` is an empty string, the default namespace is set. If the namespace URI is empty, this unbinds the prefix (removes the namespace binding).

A namespace node is automatically added to an element when needed (for example, when creating an element with `xsl:element` in a specific namespace). `xsl:namespace` is only needed when explicit control over namespace declarations is required.

## Examples

### Dynamic Namespace Declaration

```xml
<xsl:element name="{$prefix}:{$local-name}" namespace="{$ns}">
  <xsl:namespace name="{$prefix}" select="$ns"/>
  <xsl:apply-templates/>
</xsl:element>
```

### Setting Default Namespace

```xml
<html>
  <xsl:namespace name="" select="'http://www.w3.org/1999/xhtml'"/>
  <body>
    <xsl:apply-templates/>
  </body>
</html>
```

## Error Codes

- **XTDE0905**: The `name` attribute is `xmlns`.
- **XTDE0910**: The namespace URI is empty and the prefix is non-empty, or vice versa in an invalid combination.
- **XTDE0920**: A namespace node conflicts with an existing namespace binding on the element.

## See Also

- [xsl:element](xsl-element.md)
- [xsl:namespace-alias](xsl-namespace-alias.md)
