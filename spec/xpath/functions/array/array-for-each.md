---
name: array:for-each
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-for-each
---

# array:for-each

Applies a function to each member of an array and returns the results as a new array.

## Signature

```
array:for-each($array as array(*), $action as function(item()*) as item()*) as array(*)
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$array` | `array(*)` | The array to process. |
| `$action` | `function(item()*) as item()*` | The function to apply to each member. |

## Return Type

`array(*)`

## Description

The `array:for-each` function returns a new array where each member is the result of applying `$action` to the corresponding member of `$array`. The resulting array has the same size as the input.

This is the array equivalent of `fn:for-each` for sequences. Unlike `fn:for-each`, which concatenates results into a flat sequence, this function preserves the array structure.

## Examples

```xpath
array:for-each([1, 2, 3], function($x) { $x * 2 })
(: Returns [2, 4, 6] :)

array:for-each(['hello', 'world'], upper-case#1)
(: Returns ['HELLO', 'WORLD'] :)

array:for-each([], function($x) { $x + 1 })
(: Returns [] :)
```

## Error Codes

- `XPTY0004` — Type error in the function call.

## See Also

- [array:filter](array-filter.md)
- [fn:for-each](../sequence/fn-for-each.md)
