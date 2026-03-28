---
name: map:find
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-map-find
---

# map:find

Searches for a key in maps and arrays recursively and returns all matching values.

## Signature

```
map:find($input as item()*, $key as xs:anyAtomicType) as array(*)
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$input` | `item()*` | The items to search (maps and arrays are searched recursively). |
| `$key` | `xs:anyAtomicType` | The key to search for. |

## Return Type

`array(*)`

## Description

The `map:find` function searches recursively through maps and arrays in `$input`, collecting all values associated with the specified key. The result is an array of all found values, in the order they are encountered.

For each map in the input (including maps nested within arrays and other maps), if the map contains the specified key, its associated value is added to the result array. Arrays are searched recursively for contained maps.

This function is particularly useful for extracting values from deeply nested JSON-like structures where the same key may appear at multiple levels.

## Examples

```xpath
map:find(map { 'a': 1, 'b': map { 'a': 2 } }, 'a')
(: Returns [1, 2] :)

map:find([map { 'id': 1 }, map { 'id': 2 }], 'id')
(: Returns [1, 2] :)

map:find(map { 'x': 1 }, 'z')
(: Returns [] — empty array :)
```

## Error Codes

None.

## See Also

- [map:get](map-get.md)
- [map:contains](map-contains.md)
