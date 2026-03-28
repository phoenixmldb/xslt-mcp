---
name: fn:count
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-count
---

# fn:count

Returns the number of items in a sequence.

## Signature

```
fn:count($arg as item()*) as xs:integer
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `item()*` | The sequence to count. |

## Returns

The number of items in `$arg`. Returns 0 for the empty sequence.

## Examples

```xpath
count((1, 2, 3)) → 3
count(//item) → (number of item elements)
count(()) → 0
count("hello") → 1
```

## See Also

- [fn:sum](fn-sum.md)
- [fn:empty](../sequence/fn-empty.md)
- [fn:exists](../sequence/fn-exists.md)
