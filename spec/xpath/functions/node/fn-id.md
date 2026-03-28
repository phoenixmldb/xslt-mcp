---
name: fn:id
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-id
---

# fn:id

Returns elements that have an ID value matching the supplied values.

## Signature

```
fn:id($arg as xs:string*) as element()*
fn:id($arg as xs:string*, $node as node()) as element()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:string*` | One or more IDREF values to look up. Each string may contain whitespace-separated IDs. |
| `$node` | `node()?` | A node identifying the document to search. Defaults to the context item's document. |

## Return Type

`element()*`

## Description

The `fn:id` function returns the elements in a document tree that have an ID value matching any of the supplied IDREF values. The ID values are determined by DTD validation or schema validation — attributes declared as type `ID` or `xs:ID`. Each string in `$arg` may contain whitespace-separated ID values.

If `$node` is provided, the document containing that node is searched. Otherwise, the document containing the context item is searched. The returned elements are in document order with duplicates removed.

This function requires that the document has been validated so that ID attributes are recognized. Without validation, no elements will be matched. For a more general lookup, consider using XPath predicates like `//*[@id = $value]`.

## Examples

```xpath
id('sec1')
(: Returns the element with ID "sec1" :)

id('sec1 sec2 sec3')
(: Returns elements with IDs "sec1", "sec2", or "sec3" :)

id($ref-values, /)
(: Searches from the root of the document :)
```

## Error Codes

- `XPTY0004` — If the context item is not a node (when called without arguments for `$node`).

## See Also

- [fn:idref](fn-idref.md)
- [fn:generate-id](fn-generate-id.md)
