---
name: fn:sort
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-sort
---

# fn:sort

Sorts a sequence, returning the items in sorted order.

## Signature

```
fn:sort($input as item()*) as item()*
fn:sort($input as item()*, $collation as xs:string?) as item()*
fn:sort($input as item()*, $collation as xs:string?, $key as function(item()) as xs:anyAtomicType*) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$input` | `item()*` | The sequence to sort. |
| `$collation` | `xs:string?` | Collation URI for string comparison. Use `()` for default. |
| `$key` | `function(item()) as xs:anyAtomicType*` | Function to compute the sort key for each item. |

## Returns

The items sorted in ascending order of their sort keys.

## Examples

```xpath
sort((3, 1, 4, 1, 5)) → (1, 1, 3, 4, 5)
sort(("banana", "apple", "cherry")) → ("apple", "banana", "cherry")
sort(//employee, (), function($e) { $e/last-name }) → (employees sorted by last name)
```

## See Also

- [fn:reverse](fn-reverse.md)
- [xsl:sort](../../instructions/xsl-sort.md)
- [fn:distinct-values](fn-distinct-values.md)
