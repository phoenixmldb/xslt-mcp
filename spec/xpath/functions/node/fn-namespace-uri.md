---
name: fn:namespace-uri
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-namespace-uri
---

# fn:namespace-uri

Returns the namespace URI of a node.

## Signature

```
fn:namespace-uri() as xs:anyURI
fn:namespace-uri($arg as node()?) as xs:anyURI
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `node()?` | The node whose namespace URI is returned. Defaults to the context item. |

## Return Type

`xs:anyURI`

## Description

The `fn:namespace-uri` function returns the namespace URI part of the expanded QName of the specified node. For elements and attributes in a namespace, this returns the namespace URI. For nodes not in a namespace, or for node types that do not have names (text, comment, document), it returns the zero-length string.

If `$arg` is the empty sequence, the function returns the zero-length `xs:anyURI`. If no argument is provided, the context item is used.

This function is essential for namespace-aware processing, particularly when matching elements by namespace rather than by prefix.

## Examples

```xpath
namespace-uri(<svg xmlns="http://www.w3.org/2000/svg"/>)
(: Returns "http://www.w3.org/2000/svg" :)

namespace-uri(<local/>)
(: Returns "" :)

//*[namespace-uri() = 'http://www.w3.org/1999/xhtml']
(: Selects all XHTML elements :)
```

## Error Codes

- `XPTY0004` — If the context item is not a node (when called without arguments).

## See Also

- [fn:local-name](fn-local-name.md)
- [fn:name](fn-name.md)
- [fn:namespace-uri-from-QName](../qname/fn-namespace-uri-from-QName.md)
