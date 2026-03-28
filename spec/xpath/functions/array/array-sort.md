---
name: array:sort
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-sort
---

# array:sort

Returns an array with members sorted.

## Signature

```
array:sort($array as array(*)) as array(*)
array:sort($array as array(*), $collation as xs:string?) as array(*)
array:sort($array as array(*), $collation as xs:string?, $key as function(item()*) as xs:anyAtomicType*) as array(*)
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$array` | `array(*)` | The array to sort. |
| `$collation` | `xs:string?` | The collation for string comparisons. |
| `$key` | `function(item()*) as xs:anyAtomicType*?` | A function that computes the sort key for each member. |

## Return Type

`array(*)`

## Description

The `array:sort` function returns a new array with members sorted in ascending order. Without a key function, members are sorted by their atomized values. With a key function, each member is sorted by the value returned by the key function.

The collation determines string comparison order. If no collation is specified, the default collation is used. The sort is stable: members with equal sort keys retain their relative order.

## Examples

```xpath
array:sort([3, 1, 4, 1, 5])
(: Returns [1, 1, 3, 4, 5] :)

array:sort(['banana', 'apple', 'cherry'])
(: Returns ['apple', 'banana', 'cherry'] :)

array:sort([map{'name':'Bob','age':30}, map{'name':'Alice','age':25}],
           (), function($m) { $m?name })
(: Returns [map{'name':'Alice','age':25}, map{'name':'Bob','age':30}] :)
```

## Error Codes

- `FOCH0002` — The collation URI is not supported.

## See Also

- [array:reverse](array-reverse.md)
- [fn:sort](../sequence/fn-sort.md)
