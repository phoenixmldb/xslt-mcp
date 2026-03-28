---
name: fn:regex-group
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#function-regex-group
---

# fn:regex-group

Returns a captured group from a regex match within xsl:analyze-string.

## Signature

```
fn:regex-group($group-number as xs:integer) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$group-number` | `xs:integer` | The number of the captured group (1-based). 0 returns the entire match. |

## Return Type

`xs:string`

## Description

The `fn:regex-group` function returns the substring captured by a specific parenthesized group in the regular expression being processed by `xsl:analyze-string`. Group 0 returns the entire matched substring. Group 1 returns the first captured group, and so on.

If the specified group did not participate in the match (e.g., it was on an unmatched alternative), the function returns the zero-length string. If the group number is greater than the number of groups in the pattern, the function also returns the zero-length string.

This function is only meaningful within the `xsl:matching-substring` element of an `xsl:analyze-string` instruction.

## Examples

```xpath
(: <xsl:analyze-string select="'2024-03-15'" regex="(\d{{4}})-(\d{{2}})-(\d{{2}})"> :)
(: <xsl:matching-substring> :)
  regex-group(0) (: Returns "2024-03-15" :)
  regex-group(1) (: Returns "2024" :)
  regex-group(2) (: Returns "03" :)
  regex-group(3) (: Returns "15" :)
(: </xsl:matching-substring> :)
(: </xsl:analyze-string> :)
```

## Error Codes

None.

## See Also

- [fn:matches](../string/fn-matches.md)
- [fn:replace](../string/fn-replace.md)
