---
name: xsl:element
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#creating-elements
---

# xsl:element

Creates an element node in the result tree with a computed name. Use when the element name is not known at compile time.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | Yes | `{ expression }` | The name of the element to create. Evaluated as an attribute value template. |
| `namespace` | No | `{ uri }` | Namespace URI of the element. If omitted, resolved from the prefix in `name`. |
| `inherit-namespaces` | No | `"yes" \| "no"` | Whether children inherit namespaces. Default is `yes`. |
| `use-attribute-sets` | No | `eqnames` | Space-separated names of attribute sets to apply. |
| `type` | No | `eqname` | Schema type for validation. |
| `validation` | No | `"strict" \| "lax" \| "preserve" \| "strip"` | Validation mode. |
| `on-empty` | No | `expression` | Value to return if the element would be empty. (Since 3.0) |

## Content Model

_sequence-constructor_

## Examples

### Computed Element Name

```xml
<xsl:element name="{local-name()}">
  <xsl:apply-templates/>
</xsl:element>
```

### With Namespace

```xml
<xsl:element name="svg:rect" namespace="http://www.w3.org/2000/svg">
  <xsl:attribute name="width">100</xsl:attribute>
  <xsl:attribute name="height">50</xsl:attribute>
</xsl:element>
```

## Error Codes

- **XTDE0820**: The effective value of `name` is not a valid QName.
- **XTDE0830**: The prefix in `name` has no namespace binding and no `namespace` attribute is specified.
- **XTTE3130**: Validation of the element fails.

## See Also

- [xsl:attribute](xsl-attribute.md)
- [xsl:copy](xsl-copy.md)
