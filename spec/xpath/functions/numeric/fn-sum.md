---
name: fn:sum
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-sum
---

# fn:sum

Returns the sum of the values in a sequence.

## Signature

```
fn:sum($arg as xs:anyAtomicType*) as xs:anyAtomicType
fn:sum($arg as xs:anyAtomicType*, $zero as xs:anyAtomicType?) as xs:anyAtomicType?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:anyAtomicType*` | Sequence of values to sum. Values are cast to a common type. |
| `$zero` | `xs:anyAtomicType?` | Value returned when `$arg` is empty. Default is `0` (xs:integer). |

## Returns

The sum of all values. For an empty sequence, returns `$zero`.

## Examples

```xpath
sum((1, 2, 3, 4, 5)) → 15
sum(order/item/price) → (sum of all prices)
sum((), 0.0) → 0.0
sum(//amount) → (sum of all amount elements)
```

## Error Codes

- **FORG0006**: Values cannot be added (incompatible types).

## See Also

- [fn:count](fn-count.md)
- [fn:avg](fn-avg.md)
