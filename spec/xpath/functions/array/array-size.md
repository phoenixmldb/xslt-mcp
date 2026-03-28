---
name: array:size
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-size
---

# array:size

Returns the number of members in an array.

## Signature

```
array:size($array as array(*)) as xs:integer
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$array` | `array(*)` | The array to measure. |

## Return Type

`xs:integer`

## Description

The `array:size` function returns the number of members in the array. An empty array `[]` has size 0. Each member counts as one, regardless of whether it is a single item or a sequence.

## Examples

```xpath
array:size([1, 2, 3])
(: Returns 3 :)

array:size([])
(: Returns 0 :)

array:size([('a', 'b'), 'c'])
(: Returns 2 — two members, first is a sequence :)
```

## Error Codes

None.

## See Also

- [array:get](array-get.md)
