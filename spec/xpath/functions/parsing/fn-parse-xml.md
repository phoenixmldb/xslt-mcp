---
name: fn:parse-xml
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-parse-xml
---

# fn:parse-xml

Parses a string containing a well-formed XML document and returns the document node.

## Signature

```
fn:parse-xml($arg as xs:string?) as document-node(element(*))?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:string?` | A string containing a well-formed XML document. |

## Return Type

`document-node(element(*))?`

## Description

The `fn:parse-xml` function parses a string as a complete, well-formed XML document and returns the resulting document node. The string must contain exactly one top-level element (a complete XML document). If `$arg` is the empty sequence, the function returns the empty sequence.

The base URI of the resulting document is the static base URI of the calling expression. The document will have a single element node as the child of the document node, along with optional processing instructions and comments.

This function is useful when XML content is received as a string parameter, stored in a database field, or returned from an external service. For XML fragments (content without a single root element), use `fn:parse-xml-fragment` instead.

## Examples

```xpath
parse-xml('<root><item>Hello</item></root>')//item
(: Returns the <item> element :)

parse-xml($xml-string)/*/name()
(: Returns the name of the root element :)
```

## Error Codes

- `FODC0006` — The string is not a well-formed XML document.

## See Also

- [fn:parse-xml-fragment](fn-parse-xml-fragment.md)
- [fn:serialize](fn-serialize.md)
