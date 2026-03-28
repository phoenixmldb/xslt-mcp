---
name: fn:escape-html-uri
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-escape-html-uri
---

# fn:escape-html-uri

Escapes a URI in the manner of HTML user agents.

## Signature

```
fn:escape-html-uri($uri as xs:string?) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$uri` | `xs:string?` | The URI string to escape. |

## Return Type

`xs:string`

## Description

The `fn:escape-html-uri` function escapes a URI as would an HTML user agent, encoding characters above U+007E (tilde) and below U+0020 (space) with percent-encoding. ASCII printable characters (U+0020 through U+007E) are left unchanged, including spaces, which are not percent-encoded.

If `$uri` is the empty sequence, the function returns the zero-length string. This is the least aggressive of the three URI encoding functions — it only encodes non-ASCII and control characters.

Use this function when generating URIs for HTML output where the behavior should match what an HTML browser would do when submitting a form or following a link.

## Examples

```xpath
escape-html-uri('http://example.com/résumé')
(: Returns "http://example.com/r%C3%A9sum%C3%A9" :)

escape-html-uri('http://example.com/test page')
(: Returns "http://example.com/test page" — spaces preserved :)

escape-html-uri('javascript:void(0)')
(: Returns "javascript:void(0)" — all ASCII :)
```

## Error Codes

None.

## See Also

- [fn:encode-for-uri](fn-encode-for-uri.md)
- [fn:iri-to-uri](fn-iri-to-uri.md)
