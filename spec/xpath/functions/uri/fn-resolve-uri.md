---
name: fn:resolve-uri
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-resolve-uri
---

# fn:resolve-uri

Resolves a relative URI against a base URI.

## Signature

```
fn:resolve-uri($relative as xs:string?) as xs:anyURI?
fn:resolve-uri($relative as xs:string?, $base as xs:string) as xs:anyURI?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$relative` | `xs:string?` | The relative URI to resolve. |
| `$base` | `xs:string?` | The base URI to resolve against. Defaults to the static base URI. |

## Return Type

`xs:anyURI?`

## Description

The `fn:resolve-uri` function resolves a relative URI reference against a base URI, producing an absolute URI. If `$relative` is already an absolute URI, it is returned unchanged. If `$relative` is the empty sequence, the function returns the empty sequence.

If no `$base` is provided, the static base URI from the static context is used. The resolution follows the algorithm defined in RFC 3986. If the base URI is not absolute, a dynamic error is raised.

This function is essential for constructing absolute URIs from relative references, particularly when processing documents that contain relative links.

## Examples

```xpath
resolve-uri('page.html', 'http://example.com/docs/')
(: Returns "http://example.com/docs/page.html" :)

resolve-uri('../images/logo.png', 'http://example.com/docs/guide/')
(: Returns "http://example.com/docs/images/logo.png" :)

resolve-uri('http://other.com/data', 'http://example.com/')
(: Returns "http://other.com/data" — already absolute :)
```

## Error Codes

- `FORG0002` — The base URI is not a valid absolute URI.
- `FORG0009` — The relative URI cannot be resolved against the base URI.

## See Also

- [fn:base-uri](../node/fn-base-uri.md)
- [fn:encode-for-uri](fn-encode-for-uri.md)
