---
name: fn:fold-left
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-fold-left
---

# fn:fold-left

Applies a function cumulatively to items in a sequence, from left to right.

## Signature

```
fn:fold-left($seq as item()*, $zero as item()*, $f as function(item()*, item()) as item()*) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$seq` | `item()*` | The sequence to fold over. |
| `$zero` | `item()*` | The initial accumulator value. |
| `$f` | `function(item()*, item()) as item()*` | A function that takes the accumulator and an item, returning a new accumulator. |

## Return Type

`item()*`

## Description

The `fn:fold-left` function processes a sequence from left to right, threading an accumulator through each step. For each item in `$seq`, the function `$f` is called with the current accumulator value and the item, producing a new accumulator. The final accumulator value is returned.

If `$seq` is empty, the function returns `$zero` without calling `$f`. The evaluation is equivalent to: `$f($f($f($zero, $seq[1]), $seq[2]), $seq[3])` and so on.

This function is the standard functional programming left fold. It can implement any sequential aggregation: sums, products, string concatenation, running totals, or building complex data structures.

## Examples

```xpath
fold-left(1 to 5, 0, function($acc, $x) { $acc + $x })
(: Returns 15 — sum of 1 through 5 :)

fold-left(('a', 'b', 'c'), '', function($acc, $x) { concat($acc, $x) })
(: Returns "abc" :)

fold-left(1 to 5, 1, function($acc, $x) { $acc * $x })
(: Returns 120 — factorial of 5 :)

fold-left((true(), true(), false()), true(), function($a, $b) { $a and $b })
(: Returns false :)
```

## Error Codes

- `XPTY0004` — Type error in the function call.

## See Also

- [fn:fold-right](fn-fold-right.md)
- [fn:for-each](../sequence/fn-for-each.md)
- [fn:for-each-pair](fn-for-each-pair.md)
