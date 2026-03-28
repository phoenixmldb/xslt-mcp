---
name: fn:current-merge-key
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#function-current-merge-key
---

# fn:current-merge-key

Returns the merge key for the current merge group within xsl:merge.

## Signature

```
fn:current-merge-key() as xs:anyAtomicType*
```

## Parameters

None.

## Return Type

`xs:anyAtomicType*`

## Description

The `fn:current-merge-key` function returns the merge key value(s) for the current merge group during the processing of an `xsl:merge` instruction. The merge key is defined by `xsl:merge-key` elements within each `xsl:merge-source`.

If the merge uses composite keys (multiple `xsl:merge-key` elements), this function returns a sequence of atomic values corresponding to each key component.

This function is only meaningful within the body of the `xsl:merge-action` element of an `xsl:merge` instruction.

## Examples

```xpath
(: Within xsl:merge-action: :)
current-merge-key()
(: Returns the current merge key value :)

(: Use as heading for merged output: :)
(: <h2>Customer: {current-merge-key()}</h2> :)
```

## Error Codes

None.

## See Also

- [fn:current-merge-group](fn-current-merge-group.md)
- [fn:current-grouping-key](fn-current-grouping-key.md)
