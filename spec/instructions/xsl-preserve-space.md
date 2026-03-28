---
name: xsl:preserve-space
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#strip
---

# xsl:preserve-space

Preserves whitespace-only text nodes for specified elements in source documents.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `elements` | Yes | `name-tests` | Space-separated list of element name tests. Supports `*`, `prefix:*`, and specific names. |

## Content Model

Empty.

## Description

`xsl:preserve-space` is a top-level declaration that identifies elements whose whitespace-only text node children should be preserved during source document parsing. By default, all whitespace-only text nodes are preserved, so this instruction is primarily used to counteract `xsl:strip-space` for specific elements.

When both `xsl:strip-space` and `xsl:preserve-space` apply to the same element, `xsl:preserve-space` takes priority if it has higher import precedence or is more specific. Name tests are resolved with the following priority: specific names beat `prefix:*` patterns, which beat `*`.

Whitespace-only text nodes are text nodes that contain only spaces, tabs, carriage returns, and newlines. Text nodes that contain any non-whitespace character are never stripped, regardless of these declarations.

## Examples

### Preserving Pre-formatted Content

```xml
<xsl:strip-space elements="*"/>
<xsl:preserve-space elements="pre code listing"/>
```

### Namespace-Qualified Elements

```xml
<xsl:preserve-space elements="html:pre html:code"/>
```

## Error Codes

None specific to this instruction.

## See Also

- [xsl:strip-space](xsl-strip-space.md)
- [xsl:text](xsl-text.md)
