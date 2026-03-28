---
name: xsl:processing-instruction
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#creating-processing-instructions
---

# xsl:processing-instruction

Creates a processing instruction node in the result tree.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | Yes | `NCName` | The target name of the processing instruction. Attribute value template allowed. |
| `select` | No | `expression` | Expression producing the PI content. Added in XSLT 3.0. |

## Content Model

Sequence constructor (used to produce the content if `select` is absent).

## Description

`xsl:processing-instruction` creates a processing instruction (PI) node in the output. The `name` attribute provides the PI target (e.g., `xml-stylesheet`) and the content provides the PI data (the text after the target name).

The content must not contain the string `?>`, as that would prematurely end the processing instruction. If the content starts with whitespace, the leading whitespace is stripped.

The `name` attribute supports attribute value templates, allowing dynamic PI targets. However, the name `xml` (in any case variation) is reserved and will raise an error.

Processing instructions are commonly used for XML stylesheet associations, application-specific directives, and PHP/ASP-style output.

## Examples

### XML Stylesheet PI

```xml
<xsl:processing-instruction name="xml-stylesheet">
  <xsl:text>type="text/xsl" href="style.xsl"</xsl:text>
</xsl:processing-instruction>
<!-- Output: <?xml-stylesheet type="text/xsl" href="style.xsl"?> -->
```

### Dynamic PI

```xml
<xsl:processing-instruction name="{$pi-target}" select="$pi-data"/>
```

## Error Codes

- **XTDE0890**: The `name` is `xml` (case-insensitive).
- **XTDE0900**: The content contains `?>`.

## See Also

- [xsl:comment](xsl-comment.md)
- [xsl:text](xsl-text.md)
