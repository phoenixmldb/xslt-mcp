---
name: map:get
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-map-get
---

# map:get

Returns the value associated with a key in a map.

## Signature

```
map:get($map as map(*), $key as xs:anyAtomicType) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$map` | `map(*)` | The map to look up. |
| `$key` | `xs:anyAtomicType` | The key to look up. |

## Return Type

`item()*`

## Description

The `map:get` function returns the value associated with `$key` in `$map`. If the key is not present in the map, the function returns the empty sequence. This is equivalent to the lookup operator `$map?key`.

Note that the empty sequence is returned both when the key is absent and when the key maps to the empty sequence. Use `map:contains` to distinguish these cases.

## Examples

```xpath
map:get(map { 'name': 'Alice', 'age': 30 }, 'name')
(: Returns "Alice" :)

map:get(map { 'x': 1 }, 'y')
(: Returns () — key not found :)

(: Equivalent to lookup operator: :)
$map?name
(: Same as map:get($map, 'name') :)
```

## Error Codes

None.

## See Also

- [map:contains](map-contains.md)
- [map:put](map-put.md)
- [map:keys](map-keys.md)
