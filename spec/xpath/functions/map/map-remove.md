---
name: map:remove
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-map-remove
---

# map:remove

Returns a map with specified entries removed.

## Signature

```
map:remove($map as map(*), $keys as xs:anyAtomicType*) as map(*)
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$map` | `map(*)` | The original map. |
| `$keys` | `xs:anyAtomicType*` | The key(s) to remove. |

## Return Type

`map(*)`

## Description

The `map:remove` function returns a new map containing all entries from `$map` except those whose keys match any value in `$keys`. If a key in `$keys` is not present in the map, it is silently ignored.

The original map is not modified; a new map is returned. If `$keys` is empty, the original map is returned unchanged.

## Examples

```xpath
map:remove(map { 'a': 1, 'b': 2, 'c': 3 }, 'b')
(: Returns map { 'a': 1, 'c': 3 } :)

map:remove(map { 'a': 1, 'b': 2 }, ('a', 'b'))
(: Returns map {} :)

map:remove(map { 'x': 1 }, 'z')
(: Returns map { 'x': 1 } — key not found, no change :)
```

## Error Codes

None.

## See Also

- [map:put](map-put.md)
- [map:keys](map-keys.md)
