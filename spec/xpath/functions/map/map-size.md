---
name: map:size
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-map-size
---

# map:size

Returns the number of entries in a map.

## Signature

```
map:size($map as map(*)) as xs:integer
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$map` | `map(*)` | The map to measure. |

## Return Type

`xs:integer`

## Description

The `map:size` function returns the number of key-value entries in the map. An empty map has size 0.

This function is useful for checking whether a map is empty, validating expected sizes, and iterating logic.

## Examples

```xpath
map:size(map { 'a': 1, 'b': 2, 'c': 3 })
(: Returns 3 :)

map:size(map {})
(: Returns 0 :)
```

## Error Codes

None.

## See Also

- [map:keys](map-keys.md)
- [map:contains](map-contains.md)
