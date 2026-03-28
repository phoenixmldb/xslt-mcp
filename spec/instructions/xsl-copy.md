---
name: xsl:copy
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#shallow-copy
---

# xsl:copy

Creates a shallow copy of the context item. For element nodes, copies the element with its namespace nodes but not its attributes or children (those must be produced by the sequence constructor). For other node types, creates a complete copy.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | No | `expression` | The item to copy. Default is the context item (`.`). (Since 3.0) |
| `copy-namespaces` | No | `"yes" \| "no"` | Whether to copy namespace nodes. Default is `yes`. |
| `inherit-namespaces` | No | `"yes" \| "no"` | Whether children inherit the copied namespaces. Default is `yes`. |
| `use-attribute-sets` | No | `eqnames` | Space-separated names of attribute sets to apply. |
| `type` | No | `eqname` | Schema type for validation. |
| `validation` | No | `"strict" \| "lax" \| "preserve" \| "strip"` | Validation mode for the copied element. |
| `on-empty` | No | `expression` | Value to return if the result is empty. (Since 3.0) |

## Content Model

_sequence-constructor_

## Examples

### Identity Transform

```xml
<xsl:template match="@* | node()">
  <xsl:copy>
    <xsl:apply-templates select="@* | node()"/>
  </xsl:copy>
</xsl:template>
```

### Copy Element with Modifications

```xml
<xsl:template match="item">
  <xsl:copy>
    <xsl:copy-of select="@*"/>
    <xsl:attribute name="processed">true</xsl:attribute>
    <xsl:apply-templates/>
  </xsl:copy>
</xsl:template>
```

## Error Codes

- **XTTE0950**: The context item is not a node (when `select` is not used and the context item is not a node).
- **XTTE3140**: Type validation fails on the copied element.

## See Also

- [xsl:copy-of](xsl-copy-of.md)
- [xsl:element](xsl-element.md)
