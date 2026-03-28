---
name: fn:exists
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-exists
---

# fn:exists

Tests whether a sequence is non-empty.

## Signature

```
fn:exists($arg as item()*) as xs:boolean
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `item()*` | The sequence to test. |

## Returns

`true` if `$arg` contains one or more items; `false` otherwise.

## Examples

```xpath
exists((1, 2, 3)) → true()
exists(()) → false()
exists(//item) → true() (if any item elements exist)
```

## See Also

- [fn:empty](fn-empty.md)
- [fn:count](../numeric/fn-count.md)
