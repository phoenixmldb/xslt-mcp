---
name: map:contains
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-map-contains
---

# map:contains

Tests whether a map contains an entry with a given key.

## Signature

```
map:contains($map as map(*), $key as xs:anyAtomicType) as xs:boolean
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$map` | `map(*)` | The map to search. |
| `$key` | `xs:anyAtomicType` | The key to test for. |

## Return Type

`xs:boolean`

## Description

The `map:contains` function returns `true` if the map contains an entry with the specified key, and `false` otherwise. This is useful for testing key existence before attempting retrieval, especially when the associated value might legitimately be the empty sequence.

Key comparison uses the same rules as the `eq` operator for the key types involved. Note that `map:get` returns the empty sequence both when a key is absent and when the key maps to the empty sequence; `map:contains` distinguishes these cases.

## Examples

```xpath
map:contains(map { 'name': 'Alice', 'age': 30 }, 'name')
(: Returns true :)

map:contains(map { 'name': 'Alice' }, 'email')
(: Returns false :)

if (map:contains($config, 'timeout'))
then $config?timeout
else 30
```

## Error Codes

None.

## See Also

- [map:get](map-get.md)
- [map:keys](map-keys.md)
