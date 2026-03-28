---
name: fn:doc-available
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-doc-available
---

# fn:doc-available

Tests whether a document is available for retrieval via fn:doc.

## Signature

```
fn:doc-available($uri as xs:string?) as xs:boolean
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$uri` | `xs:string?` | The URI of the document to test. |

## Return Type

`xs:boolean`

## Description

The `fn:doc-available` function returns `true` if a call to `fn:doc` with the same argument would return a document node. It returns `false` if the document cannot be retrieved or is not well-formed XML.

If `$uri` is the empty sequence, the function returns `false`. This function is useful as a guard before calling `fn:doc` to avoid dynamic errors when the document may not exist.

The function never raises an error; it returns `false` for any URI that would cause `fn:doc` to fail. This makes it safe to use in conditional expressions.

## Examples

```xpath
if (doc-available('config.xml'))
then doc('config.xml')/config/setting
else 'default'

doc-available(()) (: false :)

doc-available('http://example.com/missing.xml') (: false if not retrievable :)
```

## Error Codes

None. This function does not raise errors.

## See Also

- [fn:doc](fn-doc.md)
- [fn:unparsed-text-available](fn-unparsed-text-available.md)
