---
name: fn:head
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-head
---

# fn:head

Returns the first item in a sequence.

## Signature

```
fn:head($arg as item()*) as item()?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `item()*` | The input sequence. |

## Returns

The first item, or the empty sequence if `$arg` is empty. Equivalent to `$arg[1]`.

## Examples

```xpath
head((1, 2, 3)) → 1
head("only") → "only"
head(()) → ()
```

## See Also

- [fn:tail](fn-tail.md)
- [fn:subsequence](fn-subsequence.md)
