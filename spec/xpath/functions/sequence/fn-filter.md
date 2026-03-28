---
name: fn:filter
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-filter
---

# fn:filter

Returns the items from a sequence for which a predicate function returns `true`.

## Signature

```
fn:filter($seq as item()*, $f as function(item()) as xs:boolean) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$seq` | `item()*` | The input sequence. |
| `$f` | `function(item()) as xs:boolean` | Predicate function applied to each item. |

## Returns

The items from `$seq` for which `$f` returns `true`, preserving order.

## Examples

```xpath
filter(1 to 10, function($x) { $x mod 2 = 0 }) → (2, 4, 6, 8, 10)
filter(("apple", "", "cherry"), boolean#1) → ("apple", "cherry")
filter(//item, function($i) { $i/@price > 10 }) → (items with price > 10)
```

## See Also

- [fn:for-each](fn-for-each.md)
- [fn:sort](fn-sort.md)
