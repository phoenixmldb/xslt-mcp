---
name: fn:document-uri
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-document-uri
---

# fn:document-uri

Returns the document URI of a node.

## Signature

```
fn:document-uri() as xs:anyURI?
fn:document-uri($arg as node()?) as xs:anyURI?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `node()?` | The node whose document URI is returned. Defaults to the context item. |

## Return Type

`xs:anyURI?`

## Description

The `fn:document-uri` function returns the document URI of the specified node. The document URI is the URI that was used to retrieve the document (via `fn:doc`, for example). Only document nodes have document URIs; for all other node types, the function returns the empty sequence.

If `$arg` is the empty sequence, the function returns the empty sequence. The document URI, when available, satisfies the identity `doc(document-uri($doc)) is $doc`.

This function is useful for identifying which document a node belongs to, especially when processing multiple documents.

## Examples

```xpath
document-uri(/)
(: Returns the URI of the current document :)

document-uri(doc('data.xml'))
(: Returns the resolved URI of data.xml :)

document-uri(<constructed/>)
(: Returns the empty sequence — constructed nodes have no document URI :)
```

## Error Codes

- `XPTY0004` — If the context item is not a node (when called without arguments).

## See Also

- [fn:base-uri](fn-base-uri.md)
- [fn:doc](../document/fn-doc.md)
