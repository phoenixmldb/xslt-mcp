---
name: map:put
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-map-put
---

# map:put

Returns a map with an added or replaced entry.

## Signature

```
map:put($map as map(*), $key as xs:anyAtomicType, $value as item()*) as map(*)
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$map` | `map(*)` | The original map. |
| `$key` | `xs:anyAtomicType` | The key for the entry. |
| `$value` | `item()*` | The value for the entry. |

## Return Type

`map(*)`

## Description

The `map:put` function returns a new map that contains all entries from `$map` plus an entry with the given `$key` and `$value`. If `$map` already contains an entry with key equal to `$key`, the existing entry is replaced with the new value.

The original map is not modified; a new map is returned. This is consistent with XPath's immutable data model. This function is useful for building maps incrementally or updating individual entries.

## Examples

```xpath
map:put(map { 'a': 1 }, 'b', 2)
(: Returns map { 'a': 1, 'b': 2 } :)

map:put(map { 'a': 1 }, 'a', 99)
(: Returns map { 'a': 99 } — replaces existing :)

map:put(map {}, 'key', 'value')
(: Returns map { 'key': 'value' } :)
```

## Error Codes

None.

## See Also

- [map:get](map-get.md)
- [map:remove](map-remove.md)
- [map:merge](map-merge.md)
