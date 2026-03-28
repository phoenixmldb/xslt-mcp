---
name: fn:index-of
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-index-of
---

# fn:index-of

Returns the positions of items in a sequence that match a search value.

## Signature

```
fn:index-of($seq as xs:anyAtomicType*, $search as xs:anyAtomicType) as xs:integer*
fn:index-of($seq as xs:anyAtomicType*, $search as xs:anyAtomicType, $collation as xs:string) as xs:integer*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$seq` | `xs:anyAtomicType*` | The sequence to search. |
| `$search` | `xs:anyAtomicType` | The value to search for. |
| `$collation` | `xs:string?` | The collation to use for string comparisons. |

## Return Type

`xs:integer*`

## Description

The `fn:index-of` function returns a sequence of integer positions indicating where `$search` occurs in `$seq`. Positions are 1-based. Items are compared using the `eq` operator. If no items match, the empty sequence is returned.

For string comparisons, the collation determines how strings are compared. If no collation is specified, the default collation is used. Values of different types that cannot be compared are skipped (they do not cause errors).

This function is particularly useful for finding the position of values in lookup sequences and for implementing index-based algorithms.

## Examples

```xpath
index-of((10, 20, 30, 20, 10), 20)
(: Returns (2, 4) :)

index-of(('a', 'b', 'c'), 'b')
(: Returns 2 :)

index-of((1, 2, 3), 99)
(: Returns () — empty sequence :)
```

## Error Codes

- `FOTY0012` — If an item in the sequence is a function.

## See Also

- [fn:subsequence](fn-subsequence.md)
- [fn:distinct-values](fn-distinct-values.md)
