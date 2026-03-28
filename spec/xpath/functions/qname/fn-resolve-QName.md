---
name: fn:resolve-QName
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-resolve-QName
---

# fn:resolve-QName

Resolves a lexical QName using the namespace declarations of an element.

## Signature

```
fn:resolve-QName($qname as xs:string?, $element as element()) as xs:QName?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$qname` | `xs:string?` | A lexical QName (e.g., `"prefix:local"` or `"local"`). |
| `$element` | `element()` | An element whose in-scope namespace declarations are used for resolution. |

## Return Type

`xs:QName?`

## Description

The `fn:resolve-QName` function takes a lexical QName string and resolves it to an `xs:QName` using the namespace declarations that are in scope on the given element. If the string contains a prefix, that prefix is resolved to a namespace URI using the element's namespace bindings. If no prefix is present, the default element namespace is used.

If `$qname` is the empty sequence, the function returns the empty sequence. If the prefix cannot be resolved, a dynamic error is raised.

This function is useful for resolving QName-valued attributes in XML documents where namespace prefixes need to be interpreted in context.

## Examples

```xpath
resolve-QName('svg:rect', /html/body)
(: Resolves "svg" prefix using namespace declarations on body element :)

resolve-QName('item', /root)
(: Returns QName with local name "item" in the default namespace :)
```

## Error Codes

- `FONS0004` — The prefix in the QName cannot be resolved.

## See Also

- [fn:QName](fn-QName.md)
- [fn:prefix-from-QName](fn-prefix-from-QName.md)
