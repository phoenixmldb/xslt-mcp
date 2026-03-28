---
name: array:insert-before
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-insert-before
---

# array:insert-before

Returns an array with a new member inserted at a specified position.

## Signature

```
array:insert-before($array as array(*), $position as xs:integer, $member as item()*) as array(*)
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$array` | `array(*)` | The source array. |
| `$position` | `xs:integer` | The 1-based position at which to insert. |
| `$member` | `item()*` | The member to insert. |

## Return Type

`array(*)`

## Description

The `array:insert-before` function returns a new array with `$member` inserted before the existing member at position `$position`. Members at and after the insertion point are shifted to higher positions.

The position must be between 1 and `array:size($array) + 1` inclusive. Using `array:size($array) + 1` appends the member at the end (equivalent to `array:append`).

## Examples

```xpath
array:insert-before(['a', 'c'], 2, 'b')
(: Returns ['a', 'b', 'c'] :)

array:insert-before([1, 2, 3], 1, 0)
(: Returns [0, 1, 2, 3] :)
```

## Error Codes

- `FOAY0001` — The position is out of range.

## See Also

- [array:append](array-append.md)
- [array:remove](array-remove.md)
