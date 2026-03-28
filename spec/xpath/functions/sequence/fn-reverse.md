---
name: fn:reverse
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-reverse
---

# fn:reverse

Returns the items in a sequence in reverse order.

## Signature

```
fn:reverse($arg as item()*) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `item()*` | The sequence to reverse. |

## Returns

The sequence in reverse order.

## Examples

```xpath
reverse((1, 2, 3, 4)) → (4, 3, 2, 1)
reverse("single") → "single"
reverse(()) → ()
```

## See Also

- [fn:sort](fn-sort.md)
- [fn:head](fn-head.md)
- [fn:tail](fn-tail.md)
