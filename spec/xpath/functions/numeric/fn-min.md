---
name: fn:min
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-min
---

# fn:min

Returns the minimum value in a sequence.

## Signature

```
fn:min($arg as xs:anyAtomicType*) as xs:anyAtomicType?
fn:min($arg as xs:anyAtomicType*, $collation as xs:string) as xs:anyAtomicType?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:anyAtomicType*` | Sequence of comparable values. |
| `$collation` | `xs:string` | Collation URI for string comparison. |

## Returns

The minimum value. Returns the empty sequence for an empty input. `NaN` is returned if any value is `NaN`.

## Examples

```xpath
min((3, 1, 4, 1, 5)) → 1
min(("apple", "banana", "cherry")) → "apple"
min(()) → ()
```

## Error Codes

- **FORG0006**: Values are not comparable.

## See Also

- [fn:max](fn-max.md)
- [fn:avg](fn-avg.md)
