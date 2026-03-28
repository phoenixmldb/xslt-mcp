---
name: fn:doc
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-doc
---

# fn:doc

Retrieves a document node using a URI.

## Signature

```
fn:doc($uri as xs:string?) as document-node()?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$uri` | `xs:string?` | The URI of the document to retrieve. |

## Return Type

`document-node()?`

## Description

The `fn:doc` function retrieves a document node identified by the given URI. If `$uri` is the empty sequence, the function returns the empty sequence. The URI is resolved against the static base URI if it is relative.

The function returns the document node at the specified URI. If the same URI is used in multiple calls within the same query execution, the same document node is returned each time (stability guarantee). This makes `fn:doc` suitable for cross-referencing within a single transformation.

If the resource cannot be retrieved or is not a well-formed XML document, a dynamic error is raised. Use `fn:doc-available` to test whether a document can be successfully retrieved before calling this function.

## Examples

```xpath
doc('catalog.xml')
(: Returns the document node of catalog.xml :)

doc('http://example.com/data.xml')//item
(: Retrieves remote document and selects all item elements :)

doc(())
(: Returns the empty sequence :)
```

## Error Codes

- `FODC0002` — Error retrieving the resource identified by the URI.
- `FODC0005` — The URI identifies a resource that is not a well-formed XML document.

## See Also

- [fn:doc-available](fn-doc-available.md)
- [fn:document](fn-document.md)
- [fn:collection](fn-collection.md)
