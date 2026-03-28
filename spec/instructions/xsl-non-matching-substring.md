---
name: xsl:non-matching-substring
category: instruction
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#analyze-string
---

# xsl:non-matching-substring

Processes portions of a string that do not match the regular expression in the parent `xsl:analyze-string`.

## Attributes

None.

## Content Model

Sequence constructor.

## Description

`xsl:non-matching-substring` appears as a child of `xsl:analyze-string` and provides the template for processing each substring that falls between regex matches. Within its body, the context item (`.`) is the non-matching substring as an `xs:string` value.

This element is useful for preserving or transforming the text between matched patterns. For instance, when highlighting search results, the non-matching portions represent the normal text that should pass through unchanged.

## Examples

```xml
<xsl:analyze-string select="$text" regex="\{{(\w+)\}}">
  <xsl:matching-substring>
    <xsl:value-of select="map:get($vars, regex-group(1))"/>
  </xsl:matching-substring>
  <xsl:non-matching-substring>
    <xsl:value-of select="."/>
  </xsl:non-matching-substring>
</xsl:analyze-string>
```

## Error Codes

- **XTSE0010**: Appears outside of `xsl:analyze-string`.

## See Also

- [xsl:analyze-string](xsl-analyze-string.md)
- [xsl:matching-substring](xsl-matching-substring.md)
