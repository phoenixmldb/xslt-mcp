---
name: fn:uri-collection
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-uri-collection
---

# fn:uri-collection

Returns a sequence of URIs from a collection.

## Signature

```
fn:uri-collection() as xs:anyURI*
fn:uri-collection($arg as xs:string?) as xs:anyURI*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:string?` | The URI identifying the collection. |

## Return Type

`xs:anyURI*`

## Description

The `fn:uri-collection` function returns a sequence of URIs corresponding to the resources in the identified collection. Unlike `fn:collection`, which returns the actual documents, this function returns only their URIs. This is useful when you need to filter or process URIs before loading documents.

If no argument is supplied or the argument is the empty sequence, the function returns URIs from the default collection. The interpretation of the collection URI is implementation-defined.

This function is particularly useful for lazy document loading patterns where you want to inspect URIs before deciding which documents to retrieve with `fn:doc`.

## Examples

```xpath
uri-collection('file:///data/reports/')
(: Returns URIs like 'file:///data/reports/jan.xml', etc. :)

for $uri in uri-collection('file:///data/')
where ends-with($uri, '.xml')
return doc($uri)
(: Load only XML files from the collection :)
```

## Error Codes

- `FODC0002` — The collection URI cannot be resolved.
- `FODC0004` — Invalid collection URI.

## See Also

- [fn:collection](fn-collection.md)
- [fn:doc](fn-doc.md)
