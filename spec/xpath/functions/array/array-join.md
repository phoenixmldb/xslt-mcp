---
name: array:join
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-join
---

# array:join

Concatenates a sequence of arrays into a single array.

## Signature

```
array:join($arrays as array(*)*) as array(*)
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arrays` | `array(*)*` | The arrays to concatenate. |

## Return Type

`array(*)`

## Description

The `array:join` function returns a new array containing all members from all arrays in `$arrays`, in order. The first array's members come first, followed by the second array's members, and so on.

If `$arrays` is empty, an empty array is returned. If `$arrays` contains a single array, that array is returned.

## Examples

```xpath
array:join(([1, 2], [3, 4], [5]))
(: Returns [1, 2, 3, 4, 5] :)

array:join(([1, 2], []))
(: Returns [1, 2] :)

array:join(())
(: Returns [] :)
```

## Error Codes

None.

## See Also

- [array:append](array-append.md)
- [array:subarray](array-subarray.md)
