---
name: array:remove
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-remove
---

# array:remove

Returns an array with members at specified positions removed.

## Signature

```
array:remove($array as array(*), $positions as xs:integer*) as array(*)
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$array` | `array(*)` | The source array. |
| `$positions` | `xs:integer*` | The 1-based positions to remove. |

## Return Type

`array(*)`

## Description

The `array:remove` function returns a new array with members at the specified positions removed. Multiple positions can be specified to remove several members at once. The positions refer to the original array, not to intermediate states.

If any position is out of range, a dynamic error is raised.

## Examples

```xpath
array:remove([1, 2, 3, 4], 2)
(: Returns [1, 3, 4] :)

array:remove(['a', 'b', 'c', 'd'], (1, 3))
(: Returns ['b', 'd'] :)

array:remove([1], 1)
(: Returns [] :)
```

## Error Codes

- `FOAY0001` — A position is out of range.

## See Also

- [array:insert-before](array-insert-before.md)
- [array:subarray](array-subarray.md)
