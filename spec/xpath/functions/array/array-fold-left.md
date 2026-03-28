---
name: array:fold-left
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-fold-left
---

# array:fold-left

Applies a function cumulatively to array members from left to right.

## Signature

```
array:fold-left($array as array(*), $zero as item()*, $function as function(item()*, item()*) as item()*) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$array` | `array(*)` | The array to fold. |
| `$zero` | `item()*` | The initial accumulator value. |
| `$function` | `function(item()*, item()*) as item()*` | A function taking (accumulator, member) and returning new accumulator. |

## Return Type

`item()*`

## Description

The `array:fold-left` function processes an array from left to right, threading an accumulator through each step. For each member, the function is called with the current accumulator and the member, producing a new accumulator. The final accumulator is returned.

If the array is empty, `$zero` is returned. This is the array equivalent of `fn:fold-left` for sequences.

## Examples

```xpath
array:fold-left([1, 2, 3, 4], 0, function($acc, $x) { $acc + $x })
(: Returns 10 :)

array:fold-left(['a', 'b', 'c'], '', function($acc, $x) { $acc || $x })
(: Returns "abc" :)
```

## Error Codes

- `XPTY0004` — Type error in the function call.

## See Also

- [array:fold-right](array-fold-right.md)
- [fn:fold-left](../higherorder/fn-fold-left.md)
