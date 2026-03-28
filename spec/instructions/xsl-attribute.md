---
name: xsl:attribute
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#creating-attributes
---

# xsl:attribute

Creates an attribute node in the result tree. The attribute is added to the nearest enclosing element.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | Yes | `{ expression }` | The attribute name. Evaluated as an attribute value template. |
| `namespace` | No | `{ uri }` | Namespace URI of the attribute. If omitted, resolved from the prefix in `name`. |
| `select` | No | `expression` | Expression producing the attribute value. |
| `separator` | No | `{ string }` | Separator between items when `select` returns a sequence. Default is a single space. |
| `type` | No | `eqname` | Schema type for validation. |
| `validation` | No | `"strict" \| "lax" \| "preserve" \| "strip"` | Validation mode. |
| `on-empty` | No | `expression` | Value to return if the attribute would be empty. (Since 3.0) |

## Content Model

When `select` is absent, the content is a sequence constructor. When `select` is present, the element must be empty.

## Examples

### Simple Attribute

```xml
<div>
  <xsl:attribute name="class">highlight</xsl:attribute>
  <xsl:apply-templates/>
</div>
```

### Computed Name

```xml
<xsl:attribute name="data-{$key}" select="$value"/>
```

### With Separator

```xml
<xsl:attribute name="class" select="$classes" separator=" "/>
```

## Error Codes

- **XTDE0850**: The effective value of `name` is not a valid QName.
- **XTDE0860**: An attribute is written after child nodes have been added to the element (attributes must come first).
- **XTDE0865**: The prefix in `name` is `xmlns`.

## See Also

- [xsl:element](xsl-element.md)
- [xsl:copy](xsl-copy.md)
