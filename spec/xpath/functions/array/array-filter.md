---
name: array:filter
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-filter
---

# array:filter

Returns an array containing only members that satisfy a predicate.

## Signature

```
array:filter($array as array(*), $function as function(item()*) as xs:boolean) as array(*)
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$array` | `array(*)` | The array to filter. |
| `$function` | `function(item()*) as xs:boolean` | A predicate function applied to each member. |

## Return Type

`array(*)`

## Description

The `array:filter` function returns a new array containing only those members of `$array` for which the predicate function returns `true`. The relative order of retained members is preserved.

This is the array equivalent of `fn:filter` for sequences.

## Examples

```xpath
array:filter([1, 2, 3, 4, 5], function($x) { $x mod 2 = 0 })
(: Returns [2, 4] :)

array:filter(['apple', 'banana', 'cherry'], function($s) { string-length($s) > 5 })
(: Returns ['banana', 'cherry'] :)

array:filter([1, 2, 3], function($x) { false() })
(: Returns [] :)
```

## Error Codes

- `XPTY0004` — Type error in the function call.

## See Also

- [array:for-each](array-for-each.md)
- [fn:filter](../sequence/fn-filter.md)
