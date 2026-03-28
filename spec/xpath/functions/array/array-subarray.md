---
name: array:subarray
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-subarray
---

# array:subarray

Returns a contiguous portion of an array.

## Signature

```
array:subarray($array as array(*), $start as xs:integer) as array(*)
array:subarray($array as array(*), $start as xs:integer, $length as xs:integer) as array(*)
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$array` | `array(*)` | The source array. |
| `$start` | `xs:integer` | The starting position (1-based). |
| `$length` | `xs:integer?` | The number of members to extract. If omitted, extracts to the end. |

## Return Type

`array(*)`

## Description

The `array:subarray` function returns a new array containing members from `$array` starting at position `$start`. If `$length` is specified, at most that many members are included. If `$length` is omitted, all members from `$start` to the end are included.

If `$start` is less than 1, or `$start + $length` exceeds the array size + 1, a dynamic error is raised.

## Examples

```xpath
array:subarray([1, 2, 3, 4, 5], 2)
(: Returns [2, 3, 4, 5] :)

array:subarray([1, 2, 3, 4, 5], 2, 3)
(: Returns [2, 3, 4] :)

array:subarray(['a', 'b', 'c'], 1, 1)
(: Returns ['a'] :)
```

## Error Codes

- `FOAY0001` — The start position is out of range.
- `FOAY0002` — The length is negative or extends beyond the array.

## See Also

- [array:remove](array-remove.md)
- [array:size](array-size.md)
