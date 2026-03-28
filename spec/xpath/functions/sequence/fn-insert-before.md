---
name: fn:insert-before
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-insert-before
---

# fn:insert-before

Returns a sequence with new items inserted at a specified position.

## Signature

```
fn:insert-before($target as item()*, $position as xs:integer, $inserts as item()*) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$target` | `item()*` | The source sequence. |
| `$position` | `xs:integer` | The position at which to insert (1-based). |
| `$inserts` | `item()*` | The items to insert. |

## Return Type

`item()*`

## Description

The `fn:insert-before` function returns a new sequence with the items in `$inserts` inserted into `$target` before the item at position `$position`. If `$position` is 1 or less, the items are inserted at the beginning. If `$position` is greater than the length of the target, the items are appended at the end.

The function does not modify the original sequence; it returns a new sequence. If `$inserts` is empty, the original sequence is returned unchanged.

This function is useful for building sequences incrementally or inserting computed values at specific positions.

## Examples

```xpath
insert-before((1, 2, 3), 2, 99)
(: Returns (1, 99, 2, 3) :)

insert-before(('a', 'c'), 2, 'b')
(: Returns ('a', 'b', 'c') :)

insert-before((1, 2, 3), 1, (0))
(: Returns (0, 1, 2, 3) — prepend :)

insert-before((1, 2, 3), 100, (4, 5))
(: Returns (1, 2, 3, 4, 5) — append :)
```

## Error Codes

None.

## See Also

- [fn:remove](fn-remove.md)
- [fn:subsequence](fn-subsequence.md)
