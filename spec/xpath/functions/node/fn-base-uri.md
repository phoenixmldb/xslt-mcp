---
name: fn:base-uri
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-base-uri
---

# fn:base-uri

Returns the base URI of a node.

## Signature

```
fn:base-uri() as xs:anyURI?
fn:base-uri($arg as node()?) as xs:anyURI?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `node()?` | The node whose base URI is returned. Defaults to the context item. |

## Return Type

`xs:anyURI?`

## Description

The `fn:base-uri` function returns the base URI of the specified node. The base URI is used to resolve relative URI references within the document. For document nodes, it is typically the URI from which the document was loaded. For elements with an `xml:base` attribute, that attribute determines the base URI.

If `$arg` is the empty sequence, the function returns the empty sequence. If the node has no base URI (e.g., a constructed node without a base URI), the function returns the empty sequence.

This function is essential for correctly resolving relative URIs in documents that use `xml:base` or that were loaded from known locations.

## Examples

```xpath
base-uri(/)
(: Returns the URI of the document, e.g., "file:///data/input.xml" :)

base-uri(//item[@xml:base='http://example.com/'])
(: Returns "http://example.com/" :)
```

## Error Codes

- `XPTY0004` — If the context item is not a node (when called without arguments).
- `FORG0001` — If the base URI is not a valid URI.

## See Also

- [fn:document-uri](fn-document-uri.md)
- [fn:resolve-uri](../uri/fn-resolve-uri.md)
