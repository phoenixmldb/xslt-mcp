---
name: fn:unordered
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-unordered
---

# fn:unordered

Returns a sequence in an implementation-dependent order, signaling that order is not significant.

## Signature

```
fn:unordered($sourceSeq as item()*) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$sourceSeq` | `item()*` | The sequence whose order is not significant. |

## Return Type

`item()*`

## Description

The `fn:unordered` function takes a sequence and returns the same items in an implementation-dependent order. It serves as an optimization hint to the processor, indicating that the caller does not depend on the order of items in the sequence. The processor may then choose a more efficient evaluation strategy.

The returned sequence contains exactly the same items as the input; no items are added or removed. The function may return items in any order, including the original order.

In practice, this function is rarely used in XSLT stylesheets but can be relevant in XQuery where the optimizer may benefit from the order-independence hint.

## Examples

```xpath
unordered(//employee)
(: Returns all employee elements in any order :)

unordered(1 to 100)
(: Returns integers 1-100 in implementation-dependent order :)
```

## Error Codes

None.

## See Also

- [fn:sort](fn-sort.md)
- [fn:reverse](fn-reverse.md)
