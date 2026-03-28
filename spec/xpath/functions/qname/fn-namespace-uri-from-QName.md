---
name: fn:namespace-uri-from-QName
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-namespace-uri-from-QName
---

# fn:namespace-uri-from-QName

Returns the namespace URI component of a QName.

## Signature

```
fn:namespace-uri-from-QName($arg as xs:QName?) as xs:anyURI?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:QName?` | The QName. |

## Return Type

`xs:anyURI?`

## Description

The `fn:namespace-uri-from-QName` function returns the namespace URI of the supplied QName. If `$arg` is the empty sequence, the function returns the empty sequence. If the QName has no namespace, the function returns the zero-length `xs:anyURI`.

This function is similar to `fn:namespace-uri` but operates on QName values rather than nodes.

## Examples

```xpath
namespace-uri-from-QName(QName('http://example.com', 'ex:item'))
(: Returns "http://example.com" :)

namespace-uri-from-QName(QName('', 'local'))
(: Returns "" :)
```

## Error Codes

None.

## See Also

- [fn:local-name-from-QName](fn-local-name-from-QName.md)
- [fn:prefix-from-QName](fn-prefix-from-QName.md)
- [fn:namespace-uri](../node/fn-namespace-uri.md)
