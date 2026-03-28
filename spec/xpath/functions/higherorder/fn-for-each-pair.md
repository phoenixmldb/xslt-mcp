---
name: fn:for-each-pair
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-for-each-pair
---

# fn:for-each-pair

Applies a function to corresponding items from two sequences.

## Signature

```
fn:for-each-pair($seq1 as item()*, $seq2 as item()*, $f as function(item(), item()) as item()*) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$seq1` | `item()*` | The first sequence. |
| `$seq2` | `item()*` | The second sequence. |
| `$f` | `function(item(), item()) as item()*` | A function applied to each pair of items. |

## Return Type

`item()*`

## Description

The `fn:for-each-pair` function applies a function to pairs of items taken from two sequences in parallel. The first item of `$seq1` is paired with the first item of `$seq2`, the second with the second, and so on. The results from each function call are concatenated into a single sequence.

Processing stops when either sequence is exhausted — if the sequences have different lengths, extra items in the longer sequence are ignored. If either sequence is empty, the result is the empty sequence.

This function is the XPath equivalent of a "zip with" operation in functional programming. It is useful for parallel processing of corresponding data, computing differences between sequences, or combining data from two aligned sources.

## Examples

```xpath
for-each-pair(('a', 'b', 'c'), (1, 2, 3), concat#2)
(: Returns ("a1", "b2", "c3") :)

for-each-pair((2, 3, 5), (1, 2, 4), function($a, $b) { $a - $b })
(: Returns (1, 1, 1) :)

for-each-pair(1 to 5, 2 to 6, function($a, $b) { $a * $b })
(: Returns (2, 6, 12, 20, 30) :)
```

## Error Codes

- `XPTY0004` — Type error in the function call.

## See Also

- [fn:for-each](../sequence/fn-for-each.md)
- [fn:fold-left](fn-fold-left.md)
