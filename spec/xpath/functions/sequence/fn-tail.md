---
name: fn:tail
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-tail
---

# fn:tail

Returns all items except the first in a sequence.

## Signature

```
fn:tail($arg as item()*) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `item()*` | The input sequence. |

## Returns

All items after the first. Returns the empty sequence if `$arg` has zero or one items. Equivalent to `subsequence($arg, 2)`.

## Examples

```xpath
tail((1, 2, 3, 4)) → (2, 3, 4)
tail(("only")) → ()
tail(()) → ()
```

## See Also

- [fn:head](fn-head.md)
- [fn:reverse](fn-reverse.md)
