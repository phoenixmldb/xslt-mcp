---
name: xsl:strip-space
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#strip
---

# xsl:strip-space

Strips whitespace-only text nodes from specified elements in source documents.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `elements` | Yes | `name-tests` | Space-separated list of element name tests. Supports `*`, `prefix:*`, and specific names. |

## Content Model

Empty.

## Description

`xsl:strip-space` is a top-level declaration that removes whitespace-only text nodes from the specified elements in the source document tree before processing begins. This is useful for cleaning up formatting whitespace in XML documents, which can interfere with counting, positioning, and output formatting.

The most common pattern is `<xsl:strip-space elements="*"/>` to strip all whitespace-only text nodes from the entire document, combined with `<xsl:preserve-space elements="..."/>` for elements where whitespace is significant (such as `pre` or `code`).

Stripping applies to the source document, not to the stylesheet or the result tree. Only text nodes that consist entirely of whitespace characters (spaces, tabs, carriage returns, newlines) are affected. Text nodes with any non-whitespace content are never stripped.

## Examples

### Strip All Whitespace

```xml
<xsl:strip-space elements="*"/>
```

### Strip Specific Elements

```xml
<xsl:strip-space elements="table row cell"/>
```

### Combined with Preserve

```xml
<xsl:strip-space elements="*"/>
<xsl:preserve-space elements="pre code script"/>
```

## Error Codes

None specific to this instruction.

## See Also

- [xsl:preserve-space](xsl-preserve-space.md)
- [xsl:text](xsl-text.md)
