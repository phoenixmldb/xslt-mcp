---
name: fn:avg
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-avg
---

# fn:avg

Returns the average of the values in a sequence.

## Signature

```
fn:avg($arg as xs:anyAtomicType*) as xs:anyAtomicType?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:anyAtomicType*` | Sequence of numeric values. |

## Returns

The average (sum divided by count). Returns the empty sequence for an empty input.

## Examples

```xpath
avg((1, 2, 3, 4, 5)) → 3
avg((10.0, 20.0)) → 15.0
avg(()) → ()
```

## Error Codes

- **FORG0006**: Values cannot be summed (incompatible types).

## See Also

- [fn:sum](fn-sum.md)
- [fn:count](fn-count.md)
- [fn:min](fn-min.md)
- [fn:max](fn-max.md)
