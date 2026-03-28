---
name: array:for-each-pair
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-for-each-pair
---

# array:for-each-pair

Applies a function to corresponding members from two arrays.

## Signature

```
array:for-each-pair($array1 as array(*), $array2 as array(*), $function as function(item()*, item()*) as item()*) as array(*)
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$array1` | `array(*)` | The first array. |
| `$array2` | `array(*)` | The second array. |
| `$function` | `function(item()*, item()*) as item()*` | A function applied to each pair of members. |

## Return Type

`array(*)`

## Description

The `array:for-each-pair` function returns a new array where each member is the result of applying `$function` to corresponding members of `$array1` and `$array2`. The result array has size equal to the minimum of the two input array sizes.

This is the array equivalent of `fn:for-each-pair` for sequences.

## Examples

```xpath
array:for-each-pair([1, 2, 3], [4, 5, 6], function($a, $b) { $a + $b })
(: Returns [5, 7, 9] :)

array:for-each-pair(['a', 'b'], [1, 2], function($s, $n) { $s || $n })
(: Returns ['a1', 'b2'] :)
```

## Error Codes

- `XPTY0004` — Type error in the function call.

## See Also

- [array:for-each](array-for-each.md)
- [fn:for-each-pair](../higherorder/fn-for-each-pair.md)
