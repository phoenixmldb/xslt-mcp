---
name: xsl:copy-of
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#copy-of
---

# xsl:copy-of

Creates a deep copy of the selected nodes or atomic values. Unlike `xsl:copy`, this recursively copies all descendants, attributes, and namespace nodes.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | Yes | `expression` | Expression returning items to deep-copy. |
| `copy-namespaces` | No | `"yes" \| "no"` | Whether to copy namespace nodes. Default is `yes`. |
| `type` | No | `eqname` | Schema type for validation. |
| `validation` | No | `"strict" \| "lax" \| "preserve" \| "strip"` | Validation mode for the copied nodes. |

## Content Model

Empty.

## Examples

### Deep Copy of Nodes

```xml
<xsl:copy-of select="header"/>
```

### Copy Without Namespaces

```xml
<xsl:copy-of select="$fragment" copy-namespaces="no"/>
```

### Copy Attributes

```xml
<div>
  <xsl:copy-of select="@class | @id"/>
  <xsl:apply-templates/>
</div>
```

## Error Codes

- **XTTE3160**: Validation of the copied tree fails.

## See Also

- [xsl:copy](xsl-copy.md)
- [xsl:sequence](xsl-sequence.md)
