---
name: map:keys
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-map-keys
---

# map:keys

Returns all keys present in a map.

## Signature

```
map:keys($map as map(*)) as xs:anyAtomicType*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$map` | `map(*)` | The map whose keys are returned. |

## Return Type

`xs:anyAtomicType*`

## Description

The `map:keys` function returns a sequence of all keys in the map. The order of keys is implementation-dependent. If the map is empty, the empty sequence is returned.

Keys can be of any atomic type. The returned sequence can be used with `map:get` or the lookup operator `?` to retrieve corresponding values.

## Examples

```xpath
map:keys(map { 'x': 1, 'y': 2, 'z': 3 })
(: Returns ('x', 'y', 'z') in implementation-dependent order :)

map:keys(map {})
(: Returns () :)

for $k in map:keys($map) return map:get($map, $k)
(: Iterates over all values :)
```

## Error Codes

None.

## See Also

- [map:get](map-get.md)
- [map:size](map-size.md)
- [map:for-each](map-for-each.md)
