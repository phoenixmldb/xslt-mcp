---
name: fn:subsequence
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-subsequence
---

# fn:subsequence

Returns a contiguous range of items from a sequence.

## Signature

```
fn:subsequence($sourceSeq as item()*, $startingLoc as xs:double) as item()*
fn:subsequence($sourceSeq as item()*, $startingLoc as xs:double, $length as xs:double) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$sourceSeq` | `item()*` | The source sequence. |
| `$startingLoc` | `xs:double` | The starting position (1-based). |
| `$length` | `xs:double?` | The number of items to return. If omitted, returns all remaining items. |

## Return Type

`item()*`

## Description

The `fn:subsequence` function returns a contiguous portion of a sequence, starting at position `$startingLoc`. Positions are 1-based. If `$length` is provided, at most that many items are returned. If `$length` is omitted, all items from the starting position to the end are returned.

The starting position is rounded to the nearest integer using the rules for `fn:round`. Items are included if their position `p` satisfies `$startingLoc <= p < $startingLoc + $length`. If the starting position is less than 1, items before position 1 are excluded. If `NaN` is used for `$startingLoc`, the result is empty.

This function is commonly used for pagination, windowing, and extracting portions of sequences.

## Examples

```xpath
subsequence((1, 2, 3, 4, 5), 2)
(: Returns (2, 3, 4, 5) :)

subsequence((1, 2, 3, 4, 5), 2, 3)
(: Returns (2, 3, 4) :)

subsequence(('a', 'b', 'c'), 1, 1)
(: Returns "a" :)

subsequence(1 to 100, 51, 10)
(: Returns (51, 52, ..., 60) — pagination :)
```

## Error Codes

None.

## See Also

- [fn:remove](fn-remove.md)
- [fn:insert-before](fn-insert-before.md)
