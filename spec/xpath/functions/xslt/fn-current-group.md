---
name: fn:current-group
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#function-current-group
---

# fn:current-group

Returns the items in the current group within xsl:for-each-group.

## Signature

```
fn:current-group() as item()*
```

## Parameters

None.

## Return Type

`item()*`

## Description

The `fn:current-group` function returns the sequence of items that form the current group during the processing of an `xsl:for-each-group` instruction. Each iteration of `xsl:for-each-group` processes one group, and `fn:current-group()` returns all items in that group.

The context item (`.`) within `xsl:for-each-group` is set to the first item of each group. Use `fn:current-group()` to access all items in the group, not just the first.

This function is only meaningful within the body of an `xsl:for-each-group` instruction. If called outside that context, the result is implementation-dependent.

## Examples

```xpath
(: Group employees by department :)
(: <xsl:for-each-group select="//employee" group-by="department"> :)
  current-group()
  (: Returns all employees in the current department group :)

  count(current-group())
  (: Returns the number of employees in this group :)

  current-group()/@name
  (: Returns names of all employees in this group :)
(: </xsl:for-each-group> :)
```

## Error Codes

None.

## See Also

- [fn:current-grouping-key](fn-current-grouping-key.md)
- [fn:current](fn-current.md)
