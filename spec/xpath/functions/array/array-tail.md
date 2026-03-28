---
name: array:tail
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-tail
---

# array:tail

Returns all members of an array except the first.

## Signature

```
array:tail($array as array(*)) as array(*)
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$array` | `array(*)` | The array. |

## Return Type

`array(*)`

## Description

The `array:tail` function returns a new array containing all members of `$array` except the first. If the array has one member, an empty array is returned. If the array is empty, a dynamic error is raised.

This function, combined with `array:head`, enables recursive processing of arrays in a functional style.

## Examples

```xpath
array:tail([1, 2, 3])
(: Returns [2, 3] :)

array:tail(['only'])
(: Returns [] :)
```

## Error Codes

- `FOAY0001` — The array is empty.

## See Also

- [array:head](array-head.md)
- [array:subarray](array-subarray.md)
