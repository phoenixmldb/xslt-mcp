---
name: fn:QName
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-QName
---

# fn:QName

Constructs an xs:QName from a namespace URI and a lexical QName.

## Signature

```
fn:QName($paramURI as xs:string?, $paramQName as xs:string) as xs:QName
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$paramURI` | `xs:string?` | The namespace URI (empty string or empty sequence for no namespace). |
| `$paramQName` | `xs:string` | The lexical QName (e.g., `"prefix:local"` or `"local"`). |

## Return Type

`xs:QName`

## Description

The `fn:QName` function constructs an `xs:QName` value from a namespace URI and a lexical QName string. If `$paramQName` contains a prefix, it becomes the prefix of the resulting QName. The namespace URI `$paramURI` becomes the namespace.

If `$paramURI` is the empty sequence or empty string, the QName has no namespace. If `$paramQName` has a prefix but `$paramURI` is empty, a dynamic error is raised (prefixed names must have a namespace).

This function is commonly used with `fn:error` to construct error codes and in dynamic element/attribute construction.

## Examples

```xpath
QName('http://www.w3.org/1999/xhtml', 'html:div')
(: Returns xs:QName with namespace, prefix "html", local "div" :)

QName('', 'local')
(: Returns xs:QName with no namespace, local name "local" :)

QName('http://example.com', 'ex:error-code')
(: Constructs a QName for use with fn:error :)
```

## Error Codes

- `FOCA0002` — The QName string is not a valid lexical QName.
- `FONS0004` — A prefix is present but the namespace URI is empty.

## See Also

- [fn:resolve-QName](fn-resolve-QName.md)
- [fn:local-name-from-QName](fn-local-name-from-QName.md)
- [fn:namespace-uri-from-QName](fn-namespace-uri-from-QName.md)
