---
name: map:merge
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-map-merge
---

# map:merge

Merges a sequence of maps into a single map.

## Signature

```
map:merge($maps as map(*)*) as map(*)
map:merge($maps as map(*)*, $options as map(*)) as map(*)
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$maps` | `map(*)*` | The sequence of maps to merge. |
| `$options` | `map(*)?` | Options controlling duplicate key handling. |

## Return Type

`map(*)`

## Description

The `map:merge` function combines multiple maps into a single map. If the same key appears in multiple input maps, the `duplicates` option controls the behavior. The default is `use-first`, which keeps the value from the first map containing that key.

The `$options` map supports the key `duplicates` with values: `use-first` (default, keeps first occurrence), `use-last` (keeps last occurrence), `combine` (combines values into a sequence), `reject` (raises an error on duplicates), and `use-any` (implementation chooses).

If `$maps` is empty, an empty map is returned. The resulting map contains all entries from all input maps, with duplicates resolved according to the options.

## Examples

```xpath
map:merge((map { 'a': 1, 'b': 2 }, map { 'b': 3, 'c': 4 }))
(: Returns map { 'a': 1, 'b': 2, 'c': 4 } — use-first default :)

map:merge((map { 'a': 1 }, map { 'a': 2 }), map { 'duplicates': 'use-last' })
(: Returns map { 'a': 2 } :)

map:merge((map { 'a': 1 }, map { 'a': 2 }), map { 'duplicates': 'combine' })
(: Returns map { 'a': (1, 2) } :)

map:merge(())
(: Returns map {} :)
```

## Error Codes

- `FOJS0003` — Duplicate keys when `duplicates` is `reject`.

## See Also

- [map:put](map-put.md)
- [map:entry](map-entry.md)
