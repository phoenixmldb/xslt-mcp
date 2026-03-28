---
name: fn:idref
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-idref
---

# fn:idref

Returns nodes that have an IDREF value matching the supplied ID values.

## Signature

```
fn:idref($arg as xs:string*) as node()*
fn:idref($arg as xs:string*, $node as node()) as node()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:string*` | ID values to look up in IDREF attributes. |
| `$node` | `node()?` | A node identifying the document to search. |

## Return Type

`node()*`

## Description

The `fn:idref` function returns the attribute or element nodes in a document that have an IDREF or IDREFS value matching one of the supplied ID values. This is the reverse lookup of `fn:id` — given an ID, it finds all IDREF attributes that reference that ID.

The IDREF values are determined by DTD or schema validation. Attributes of type `IDREF` or `IDREFS` (or `xs:IDREF`) are searched. The result is returned in document order with duplicates removed.

Like `fn:id`, this function requires validation to be effective. Without validation, IDREF attributes are not recognized as such.

## Examples

```xpath
idref('sec1')
(: Returns attribute/element nodes with IDREF value "sec1" :)

idref('item42', /)
(: Searches from the root document for IDREF references to "item42" :)
```

## Error Codes

- `XPTY0004` — If the context item is not a node (when called without arguments for `$node`).

## See Also

- [fn:id](fn-id.md)
