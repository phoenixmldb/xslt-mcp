---
name: fn:distinct-values
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-distinct-values
---

# fn:distinct-values

Returns a sequence with duplicate values removed.

## Signature

```
fn:distinct-values($arg as xs:anyAtomicType*) as xs:anyAtomicType*
fn:distinct-values($arg as xs:anyAtomicType*, $collation as xs:string) as xs:anyAtomicType*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:anyAtomicType*` | Sequence of atomic values. |
| `$collation` | `xs:string` | Collation URI for string comparison. |

## Returns

A sequence with duplicates removed. The order of retained items is implementation-dependent. Values are compared using the `eq` operator.

## Examples

```xpath
distinct-values((1, 2, 2, 3, 3, 3)) → (1, 2, 3)
distinct-values(("a", "b", "a")) → ("a", "b")
distinct-values(//item/@category) → (unique categories)
```

## See Also

- [fn:sort](fn-sort.md)
- [fn:count](../numeric/fn-count.md)
