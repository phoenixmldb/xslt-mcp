---
name: map:entry
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-map-entry
---

# map:entry

Creates a map with a single entry.

## Signature

```
map:entry($key as xs:anyAtomicType, $value as item()*) as map(*)
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$key` | `xs:anyAtomicType` | The key for the entry. |
| `$value` | `item()*` | The value for the entry. |

## Return Type

`map(*)`

## Description

The `map:entry` function creates a new map containing exactly one entry with the given key and value. This function is particularly useful when constructing maps dynamically using `map:merge`, where each entry is generated separately and then combined.

The function provides a convenient way to create single-entry maps for use in merge operations or as building blocks for more complex map constructions.

## Examples

```xpath
map:entry('name', 'Alice')
(: Returns map { 'name': 'Alice' } :)

map:merge(for $x in 1 to 3 return map:entry($x, $x * $x))
(: Returns map { 1: 1, 2: 4, 3: 9 } :)

map:merge((map:entry('a', 1), map:entry('b', 2)))
(: Returns map { 'a': 1, 'b': 2 } :)
```

## Error Codes

None.

## See Also

- [map:merge](map-merge.md)
- [map:put](map-put.md)
