---
name: array:reverse
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-reverse
---

# array:reverse

Returns an array with members in reverse order.

## Signature

```
array:reverse($array as array(*)) as array(*)
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$array` | `array(*)` | The array to reverse. |

## Return Type

`array(*)`

## Description

The `array:reverse` function returns a new array containing the same members as `$array` but in reverse order. The first member becomes the last, and vice versa.

## Examples

```xpath
array:reverse([1, 2, 3])
(: Returns [3, 2, 1] :)

array:reverse(['a'])
(: Returns ['a'] :)

array:reverse([])
(: Returns [] :)
```

## Error Codes

None.

## See Also

- [array:sort](array-sort.md)
- [fn:reverse](../sequence/fn-reverse.md)
