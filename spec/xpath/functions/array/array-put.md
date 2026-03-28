---
name: array:put
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-put
---

# array:put

Returns an array with one member replaced.

## Signature

```
array:put($array as array(*), $position as xs:integer, $member as item()*) as array(*)
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$array` | `array(*)` | The original array. |
| `$position` | `xs:integer` | The 1-based position to replace. |
| `$member` | `item()*` | The new member value. |

## Return Type

`array(*)`

## Description

The `array:put` function returns a new array that is a copy of `$array` with the member at `$position` replaced by `$member`. The original array is not modified.

If the position is out of range, a dynamic error is raised.

## Examples

```xpath
array:put(['a', 'b', 'c'], 2, 'B')
(: Returns ['a', 'B', 'c'] :)

array:put([1, 2, 3], 1, 99)
(: Returns [99, 2, 3] :)
```

## Error Codes

- `FOAY0001` — The position is out of range.

## See Also

- [array:get](array-get.md)
- [array:append](array-append.md)
