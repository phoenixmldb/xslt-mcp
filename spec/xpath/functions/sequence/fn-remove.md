---
name: fn:remove
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-remove
---

# fn:remove

Returns a sequence with one item removed at a specified position.

## Signature

```
fn:remove($target as item()*, $position as xs:integer) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$target` | `item()*` | The source sequence. |
| `$position` | `xs:integer` | The position of the item to remove (1-based). |

## Return Type

`item()*`

## Description

The `fn:remove` function returns a new sequence containing all items from `$target` except the item at position `$position`. If `$position` is less than 1 or greater than the length of the sequence, the original sequence is returned unchanged.

Positions are 1-based. The function does not modify the original sequence; it returns a new sequence. This is consistent with the functional, immutable nature of XPath sequences.

This function removes exactly one item. To remove multiple items, use `fn:filter` or a `for` expression with a predicate.

## Examples

```xpath
remove((1, 2, 3, 4), 2)
(: Returns (1, 3, 4) :)

remove(('a', 'b', 'c'), 1)
(: Returns ('b', 'c') :)

remove((1, 2, 3), 5)
(: Returns (1, 2, 3) — position out of range :)
```

## Error Codes

None.

## See Also

- [fn:insert-before](fn-insert-before.md)
- [fn:subsequence](fn-subsequence.md)
