---
name: array:flatten
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-array-flatten
---

# array:flatten

Recursively flattens arrays into a sequence of non-array items.

## Signature

```
array:flatten($input as item()*) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$input` | `item()*` | The items to flatten. |

## Return Type

`item()*`

## Description

The `array:flatten` function recursively replaces any array in the input with its members, and continues until no arrays remain. Non-array items pass through unchanged. The result is a flat sequence with no arrays.

This function processes nested arrays at any depth. It is useful for converting hierarchical array structures into flat sequences for processing with sequence-based functions.

## Examples

```xpath
array:flatten([1, [2, 3], [4, [5]]])
(: Returns (1, 2, 3, 4, 5) :)

array:flatten([1, 2, 3])
(: Returns (1, 2, 3) :)

array:flatten(('a', ['b', 'c'], 'd'))
(: Returns ('a', 'b', 'c', 'd') :)

array:flatten([])
(: Returns () :)
```

## Error Codes

None.

## See Also

- [array:join](array-join.md)
