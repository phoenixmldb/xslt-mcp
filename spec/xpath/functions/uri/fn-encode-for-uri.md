---
name: fn:encode-for-uri
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-encode-for-uri
---

# fn:encode-for-uri

Percent-encodes a string for use as a URI path segment or query parameter.

## Signature

```
fn:encode-for-uri($uri-part as xs:string?) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$uri-part` | `xs:string?` | The string to encode. |

## Return Type

`xs:string`

## Description

The `fn:encode-for-uri` function applies URI percent-encoding to a string. It encodes all characters that are not unreserved characters in RFC 3986 (letters, digits, hyphen, period, underscore, tilde). This includes encoding characters like `/`, `?`, `#`, `&`, and `=` which have special meaning in URIs.

If `$uri-part` is the empty sequence, the function returns the zero-length string. This function is the most aggressive of the three URI encoding functions — it encodes everything except unreserved characters.

Use this function when inserting user-provided text into a URI component where all special characters must be escaped, such as a query parameter value or a path segment.

## Examples

```xpath
encode-for-uri('hello world')
(: Returns "hello%20world" :)

encode-for-uri('100% guaranteed')
(: Returns "100%25%20guaranteed" :)

concat('http://example.com/search?q=', encode-for-uri('a&b=c'))
(: Returns "http://example.com/search?q=a%26b%3Dc" :)
```

## Error Codes

None.

## See Also

- [fn:iri-to-uri](fn-iri-to-uri.md)
- [fn:escape-html-uri](fn-escape-html-uri.md)
- [fn:resolve-uri](fn-resolve-uri.md)
