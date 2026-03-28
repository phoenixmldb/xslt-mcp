---
name: fn:current-grouping-key
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#function-current-grouping-key
---

# fn:current-grouping-key

Returns the grouping key for the current group within xsl:for-each-group.

## Signature

```
fn:current-grouping-key() as xs:anyAtomicType?
```

## Parameters

None.

## Return Type

`xs:anyAtomicType?`

## Description

The `fn:current-grouping-key` function returns the value of the grouping key for the current group during the processing of an `xsl:for-each-group` instruction with `group-by` or `group-adjacent`. The key is the value that all items in the current group share.

When `group-by` is used, each item may contribute multiple grouping key values, and the item appears in multiple groups. The `fn:current-grouping-key()` returns the key for the specific group being processed.

This function is only meaningful within the body of an `xsl:for-each-group` instruction that uses `group-by` or `group-adjacent`. For `group-starting-with` and `group-ending-with`, this function returns the empty sequence.

## Examples

```xpath
(: <xsl:for-each-group select="//employee" group-by="department"> :)
  current-grouping-key()
  (: Returns e.g., "Sales", "Engineering", etc. :)

  (: Use as heading: :)
  (: <h2>{current-grouping-key()}</h2> :)
(: </xsl:for-each-group> :)
```

## Error Codes

None.

## See Also

- [fn:current-group](fn-current-group.md)
- [fn:current](fn-current.md)
