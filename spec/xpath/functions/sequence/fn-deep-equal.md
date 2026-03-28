---
name: fn:deep-equal
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-deep-equal
---

# fn:deep-equal

Tests whether two sequences are deep-equal.

## Signature

```
fn:deep-equal($parameter1 as item()*, $parameter2 as item()*) as xs:boolean
fn:deep-equal($parameter1 as item()*, $parameter2 as item()*, $collation as xs:string) as xs:boolean
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$parameter1` | `item()*` | The first sequence. |
| `$parameter2` | `item()*` | The second sequence. |
| `$collation` | `xs:string?` | The collation for string comparisons. |

## Return Type

`xs:boolean`

## Description

The `fn:deep-equal` function performs a deep comparison of two sequences. Two sequences are deep-equal if they have the same number of items and each pair of corresponding items is deep-equal. Atomic values are compared using the `eq` operator. Nodes are compared by comparing their node kind, name, and children recursively.

For element nodes, deep equality requires matching names, matching attributes (regardless of order), and deep-equal children. For document nodes, the children are compared. For text, comment, and processing instruction nodes, the string values are compared.

This function is essential for testing and validation, allowing comparison of complex tree structures. Maps and arrays (in XPath 3.1) are also compared recursively.

## Examples

```xpath
deep-equal((1, 2, 3), (1, 2, 3))
(: Returns true :)

deep-equal((1, 2, 3), (1, 2, 4))
(: Returns false :)

deep-equal(<a x="1"/>, <a x="1"/>)
(: Returns true — structurally identical :)

deep-equal((), ())
(: Returns true :)
```

## Error Codes

- `FOTY0015` — If an item is a function that is not a map or array.

## See Also

- [fn:compare](../string/fn-compare.md)
