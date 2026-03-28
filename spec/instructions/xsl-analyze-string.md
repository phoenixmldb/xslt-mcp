---
name: xsl:analyze-string
category: instruction
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#analyze-string
---

# xsl:analyze-string

Analyzes a string using a regular expression, splitting it into matching and non-matching substrings.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | Yes | `expression` | Expression returning the string to analyze. |
| `regex` | Yes | `string` | The regular expression pattern. Attribute value template allowed. |
| `flags` | No | `string` | Regular expression flags: `s` (dot-all), `m` (multi-line), `i` (case-insensitive), `x` (comments/whitespace). |

## Content Model

( `xsl:matching-substring`?, `xsl:non-matching-substring`?, `xsl:fallback`* )

At least one of `xsl:matching-substring` or `xsl:non-matching-substring` must be present.

## Description

`xsl:analyze-string` applies a regular expression to the input string and processes each portion. The string is divided into alternating substrings: those that match the regex and those that do not. The `xsl:matching-substring` child handles matching portions and `xsl:non-matching-substring` handles the rest.

Within `xsl:matching-substring`, the function `fn:regex-group(N)` returns the string captured by the Nth parenthesized group in the regex. Group 0 is the entire match. This makes `xsl:analyze-string` a powerful tool for parsing structured text, tokenizing input, or performing complex string transformations.

The regex syntax follows XPath regular expressions (XML Schema-based with XPath extensions). The `flags` attribute controls regex behavior: `i` for case-insensitive matching, `s` to make `.` match newlines, `m` for multi-line mode where `^` and `$` match line boundaries, and `x` to allow whitespace and comments in the pattern.

If the regex matches a zero-length string, the processor advances past it to avoid infinite loops.

## Examples

### Parsing Key-Value Pairs

```xml
<xsl:analyze-string select="$input" regex="(\w+)=(\w+)">
  <xsl:matching-substring>
    <entry key="{regex-group(1)}" value="{regex-group(2)}"/>
  </xsl:matching-substring>
  <xsl:non-matching-substring>
    <xsl:if test="normalize-space(.)">
      <text><xsl:value-of select="."/></text>
    </xsl:if>
  </xsl:non-matching-substring>
</xsl:analyze-string>
```

### Highlighting Search Terms

```xml
<xsl:analyze-string select="." regex="{$search-term}" flags="i">
  <xsl:matching-substring>
    <span class="highlight"><xsl:value-of select="."/></span>
  </xsl:matching-substring>
  <xsl:non-matching-substring>
    <xsl:value-of select="."/>
  </xsl:non-matching-substring>
</xsl:analyze-string>
```

### Tokenizing CSV

```xml
<xsl:analyze-string select="$line" regex='("[^"]*"|[^,]+)'>
  <xsl:matching-substring>
    <field><xsl:value-of select="replace(., '^&quot;|&quot;$', '')"/></field>
  </xsl:matching-substring>
</xsl:analyze-string>
```

## Error Codes

- **XTDE1140**: The regex is invalid.
- **XTDE1150**: The regex matches a zero-length string.
- **FORX0001**: Invalid regular expression flags.
- **FORX0002**: Invalid regular expression.

## See Also

- [xsl:matching-substring](xsl-matching-substring.md)
- [xsl:non-matching-substring](xsl-non-matching-substring.md)
