---
name: xsl:matching-substring
category: instruction
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#analyze-string
---

# xsl:matching-substring

Processes portions of a string that match the regular expression in the parent `xsl:analyze-string`.

## Attributes

None.

## Content Model

Sequence constructor.

## Description

`xsl:matching-substring` appears as a child of `xsl:analyze-string` and provides the template for processing each substring that matches the regular expression. Within its body, the context item (`.`) is the matched substring as an `xs:string` value.

The function `fn:regex-group(N)` is available within this element to access captured groups from the regex. `regex-group(1)` returns the first parenthesized group, `regex-group(2)` the second, and so on. `regex-group(0)` returns the entire match (equivalent to `.`). If a group did not participate in the match, `regex-group()` returns the empty string.

## Examples

```xml
<xsl:analyze-string select="$date" regex="(\d{{4}})-(\d{{2}})-(\d{{2}})">
  <xsl:matching-substring>
    <date year="{regex-group(1)}"
          month="{regex-group(2)}"
          day="{regex-group(3)}"/>
  </xsl:matching-substring>
</xsl:analyze-string>
```

## Error Codes

- **XTSE0010**: Appears outside of `xsl:analyze-string`.

## See Also

- [xsl:analyze-string](xsl-analyze-string.md)
- [xsl:non-matching-substring](xsl-non-matching-substring.md)
