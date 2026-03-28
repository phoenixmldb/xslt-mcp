---
name: array:get
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-get
---

# array:get

Returns the member at a given position in an array.

## Signature

```
array:get($array as array(*), $position as xs:integer) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$array` | `array(*)` | The array. |
| `$position` | `xs:integer` | The 1-based position. |

## Return Type

`item()*`

## Description

The `array:get` function returns the member at position `$position` in the array. Positions are 1-based. This is equivalent to the lookup expression `$array?position` or `$array($position)`.

If the position is less than 1 or greater than the array size, a dynamic error is raised.

## Examples

```xpath
array:get(['a', 'b', 'c'], 2)
(: Returns "b" :)

array:get([10, 20, 30], 1)
(: Returns 10 :)

(: Equivalent: :)
['a', 'b', 'c']?2
(: Returns "b" :)
```

## Error Codes

- `FOAY0001` — The position is out of range.

## See Also

- [array:put](array-put.md)
- [array:size](array-size.md)
