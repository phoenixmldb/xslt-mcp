---
name: array:fold-right
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-fold-right
---

# array:fold-right

Applies a function cumulatively to array members from right to left.

## Signature

```
array:fold-right($array as array(*), $zero as item()*, $function as function(item()*, item()*) as item()*) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$array` | `array(*)` | The array to fold. |
| `$zero` | `item()*` | The initial accumulator value. |
| `$function` | `function(item()*, item()*) as item()*` | A function taking (member, accumulator) and returning new accumulator. |

## Return Type

`item()*`

## Description

The `array:fold-right` function processes an array from right to left, threading an accumulator through each step. For each member (starting from the last), the function is called with the member and the current accumulator, producing a new accumulator. The final accumulator is returned.

If the array is empty, `$zero` is returned. This is the array equivalent of `fn:fold-right` for sequences.

## Examples

```xpath
array:fold-right(['a', 'b', 'c'], '', function($x, $acc) { $x || $acc })
(: Returns "abc" :)

array:fold-right([1, 2, 3], 0, function($x, $acc) { $x + $acc })
(: Returns 6 :)
```

## Error Codes

- `XPTY0004` — Type error in the function call.

## See Also

- [array:fold-left](array-fold-left.md)
- [fn:fold-right](../higherorder/fn-fold-right.md)
