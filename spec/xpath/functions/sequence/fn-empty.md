---
name: fn:empty
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-empty
---

# fn:empty

Tests whether a sequence is empty.

## Signature

```
fn:empty($arg as item()*) as xs:boolean
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `item()*` | The sequence to test. |

## Returns

`true` if `$arg` contains zero items; `false` otherwise.

## Examples

```xpath
empty(()) → true()
empty((1, 2, 3)) → false()
empty(//nonexistent) → true()
```

## See Also

- [fn:exists](fn-exists.md)
- [fn:count](../numeric/fn-count.md)
