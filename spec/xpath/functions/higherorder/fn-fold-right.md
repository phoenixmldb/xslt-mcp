---
name: fn:fold-right
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-fold-right
---

# fn:fold-right

Applies a function cumulatively to items in a sequence, from right to left.

## Signature

```
fn:fold-right($seq as item()*, $zero as item()*, $f as function(item(), item()*) as item()*) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$seq` | `item()*` | The sequence to fold over. |
| `$zero` | `item()*` | The initial accumulator value. |
| `$f` | `function(item(), item()*) as item()*` | A function that takes an item and the accumulator, returning a new accumulator. |

## Return Type

`item()*`

## Description

The `fn:fold-right` function processes a sequence from right to left, threading an accumulator through each step. For each item in `$seq` (starting from the last), the function `$f` is called with the item and the current accumulator value, producing a new accumulator. The final accumulator value is returned.

If `$seq` is empty, the function returns `$zero` without calling `$f`. The evaluation is equivalent to: `$f($seq[1], $f($seq[2], $f($seq[3], $zero)))`.

Note the parameter order difference from `fn:fold-left`: in `fn:fold-right`, the item comes first and the accumulator second. This function is useful when the order of operations matters (non-associative operations) or when building right-associated structures.

## Examples

```xpath
fold-right(('a', 'b', 'c'), '', function($x, $acc) { concat($x, $acc) })
(: Returns "abc" :)

fold-right(1 to 3, (), function($x, $acc) { ($x, $acc) })
(: Returns (1, 2, 3) — identity :)

fold-right(('a', 'b', 'c'), 'z', function($x, $acc) { concat('(', $x, '+', $acc, ')') })
(: Returns "(a+(b+(c+z)))" :)
```

## Error Codes

- `XPTY0004` — Type error in the function call.

## See Also

- [fn:fold-left](fn-fold-left.md)
- [fn:for-each](../sequence/fn-for-each.md)
