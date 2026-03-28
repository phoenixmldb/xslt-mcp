---
name: fn:collection
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-collection
---

# fn:collection

Returns a sequence of items identified by a collection URI.

## Signature

```
fn:collection() as item()*
fn:collection($arg as xs:string?) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:string?` | The URI identifying the collection. |

## Return Type

`item()*`

## Description

The `fn:collection` function returns the sequence of items (typically document nodes) identified by the given collection URI. The interpretation of the collection URI is implementation-defined. If no argument is provided or the argument is the empty sequence, the default collection is returned.

Collections provide a way to access groups of related documents without specifying each URI individually. The exact mechanism for defining collections varies by processor. Common implementations map collection URIs to directories or database queries.

The order and composition of the returned sequence is stable within a single query execution but may vary between executions.

## Examples

```xpath
collection('file:///data/reports/')
(: Returns all documents in the reports directory :)

collection()
(: Returns the default collection :)

collection('http://example.com/dataset')//record
(: Selects record elements from all documents in collection :)
```

## Error Codes

- `FODC0002` — The collection URI cannot be resolved or the collection is not available.
- `FODC0004` — Invalid collection URI.

## See Also

- [fn:uri-collection](fn-uri-collection.md)
- [fn:doc](fn-doc.md)
