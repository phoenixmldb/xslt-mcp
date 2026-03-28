---
name: fn:max
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-max
---

# fn:max

Returns the maximum value in a sequence.

## Signature

```
fn:max($arg as xs:anyAtomicType*) as xs:anyAtomicType?
fn:max($arg as xs:anyAtomicType*, $collation as xs:string) as xs:anyAtomicType?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:anyAtomicType*` | Sequence of comparable values. |
| `$collation` | `xs:string` | Collation URI for string comparison. |

## Returns

The maximum value. Returns the empty sequence for an empty input. `NaN` is returned if any value is `NaN`.

## Examples

```xpath
max((3, 1, 4, 1, 5)) → 5
max(("apple", "banana", "cherry")) → "cherry"
max(()) → ()
```

## Error Codes

- **FORG0006**: Values are not comparable.

## See Also

- [fn:min](fn-min.md)
- [fn:avg](fn-avg.md)
