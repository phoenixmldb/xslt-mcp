---
name: map:for-each
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-map-for-each
---

# map:for-each

Applies a function to every entry in a map and returns the concatenated results.

## Signature

```
map:for-each($map as map(*), $action as function(xs:anyAtomicType, item()*) as item()*) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$map` | `map(*)` | The map to iterate. |
| `$action` | `function(xs:anyAtomicType, item()*) as item()*` | A function called with each key and value. |

## Return Type

`item()*`

## Description

The `map:for-each` function applies the `$action` function to each entry in the map. The function receives the key as its first argument and the associated value as its second argument. The results from all invocations are concatenated into a single sequence.

The order in which entries are processed is implementation-dependent. This function is the map equivalent of `fn:for-each` for sequences and is useful for transforming map entries into other structures.

## Examples

```xpath
map:for-each(map { 'a': 1, 'b': 2 }, function($k, $v) { $k || '=' || $v })
(: Returns e.g., ('a=1', 'b=2') :)

map:for-each(map { 'x': 10, 'y': 20 }, function($k, $v) { map:entry($k, $v * 2) })
=> map:merge()
(: Returns map { 'x': 20, 'y': 40 } :)

map:for-each($map, function($k, $v) { <entry key="{$k}">{$v}</entry> })
(: Converts map entries to XML elements :)
```

## Error Codes

- `XPTY0004` — Type error in the function call.

## See Also

- [map:keys](map-keys.md)
- [fn:for-each](../sequence/fn-for-each.md)
