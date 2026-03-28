---
name: fn:for-each
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-for-each
---

# fn:for-each

Applies a function to every item in a sequence and returns the concatenation of the results.

## Signature

```
fn:for-each($seq as item()*, $action as function(item()) as item()*) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$seq` | `item()*` | The input sequence. |
| `$action` | `function(item()) as item()*` | Function applied to each item. |

## Returns

The concatenation of the results of applying `$action` to each item in `$seq`.

## Examples

```xpath
for-each((1, 2, 3), function($x) { $x * $x }) → (1, 4, 9)
for-each(("a", "b", "c"), upper-case#1) → ("A", "B", "C")
for-each(//name, function($n) { lower-case($n) }) → (lowercased names)
```

## See Also

- [fn:filter](fn-filter.md)
- [fn:sort](fn-sort.md)
- [xsl:for-each](../../instructions/xsl-for-each.md)
