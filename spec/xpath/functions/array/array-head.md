---
name: array:head
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-head
---

# array:head

Returns the first member of an array.

## Signature

```
array:head($array as array(*)) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$array` | `array(*)` | The array. |

## Return Type

`item()*`

## Description

The `array:head` function returns the first member of the array. This is equivalent to `$array?1` or `array:get($array, 1)`. If the array is empty, a dynamic error is raised.

## Examples

```xpath
array:head([1, 2, 3])
(: Returns 1 :)

array:head(['first', 'second'])
(: Returns "first" :)
```

## Error Codes

- `FOAY0001` — The array is empty.

## See Also

- [array:tail](array-tail.md)
- [array:get](array-get.md)
