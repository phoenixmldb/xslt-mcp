---
name: fn:prefix-from-QName
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-prefix-from-QName
---

# fn:prefix-from-QName

Returns the prefix component of a QName.

## Signature

```
fn:prefix-from-QName($arg as xs:QName?) as xs:NCName?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:QName?` | The QName. |

## Return Type

`xs:NCName?`

## Description

The `fn:prefix-from-QName` function returns the prefix part of the supplied QName. If the QName has no prefix, or if `$arg` is the empty sequence, the function returns the empty sequence.

Note that the prefix of a QName is not semantically significant — two QNames can be equal even if they have different prefixes, as long as their namespace URI and local name match.

## Examples

```xpath
prefix-from-QName(QName('http://example.com', 'ex:item'))
(: Returns "ex" :)

prefix-from-QName(QName('', 'item'))
(: Returns () — no prefix :)
```

## Error Codes

None.

## See Also

- [fn:local-name-from-QName](fn-local-name-from-QName.md)
- [fn:namespace-uri-from-QName](fn-namespace-uri-from-QName.md)
