---
name: array:append
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-append
---

# array:append

Returns an array with a new member added at the end.

## Signature

```
array:append($array as array(*), $appendage as item()*) as array(*)
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$array` | `array(*)` | The original array. |
| `$appendage` | `item()*` | The member to append. |

## Return Type

`array(*)`

## Description

The `array:append` function returns a new array consisting of all members of `$array` followed by `$appendage` as an additional member. The original array is not modified.

The appended item becomes a single member of the array, even if it is a sequence. The resulting array has a size one greater than the input.

## Examples

```xpath
array:append([1, 2, 3], 4)
(: Returns [1, 2, 3, 4] :)

array:append([], 'first')
(: Returns ['first'] :)

array:append(['a'], ('b', 'c'))
(: Returns ['a', ('b', 'c')] — sequence becomes one member :)
```

## Error Codes

None.

## See Also

- [array:insert-before](array-insert-before.md)
- [array:subarray](array-subarray.md)
