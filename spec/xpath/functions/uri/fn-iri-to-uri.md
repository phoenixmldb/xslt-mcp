---
name: fn:iri-to-uri
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-iri-to-uri
---

# fn:iri-to-uri

Converts an IRI to a URI by percent-encoding characters not allowed in URIs.

## Signature

```
fn:iri-to-uri($iri as xs:string?) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$iri` | `xs:string?` | The IRI string to convert. |

## Return Type

`xs:string`

## Description

The `fn:iri-to-uri` function converts an IRI (Internationalized Resource Identifier) to a URI by percent-encoding characters that are allowed in IRIs but not in URIs. Characters that are valid in URIs (including reserved characters like `/`, `?`, `#`, `&`, `=`) are left unchanged. Only non-ASCII characters and certain other characters are encoded.

If `$iri` is the empty sequence, the function returns the zero-length string. This is less aggressive than `fn:encode-for-uri` — it preserves the URI structure while encoding only the internationalized characters.

Use this function when you have a complete IRI with non-ASCII characters (such as accented letters or CJK characters) and need to convert it to a valid ASCII URI.

## Examples

```xpath
iri-to-uri('http://example.com/résumé')
(: Returns "http://example.com/r%C3%A9sum%C3%A9" :)

iri-to-uri('http://example.com/path?q=hello')
(: Returns "http://example.com/path?q=hello" — ASCII URIs unchanged :)
```

## Error Codes

None.

## See Also

- [fn:encode-for-uri](fn-encode-for-uri.md)
- [fn:escape-html-uri](fn-escape-html-uri.md)
