---
name: fn:parse-xml-fragment
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-parse-xml-fragment
---

# fn:parse-xml-fragment

Parses a string containing an XML fragment and returns a document node.

## Signature

```
fn:parse-xml-fragment($arg as xs:string?) as document-node()?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:string?` | A string containing well-formed XML content (possibly multiple elements, text, etc.). |

## Return Type

`document-node()?`

## Description

The `fn:parse-xml-fragment` function parses a string as an XML fragment, which may contain multiple top-level elements, text nodes, comments, and processing instructions. Unlike `fn:parse-xml`, the input does not need to have a single root element.

If `$arg` is the empty sequence, the function returns the empty sequence. If `$arg` is the empty string, the function returns a document node with no children. The fragment is parsed as the content of an anonymous element, so namespace declarations in scope must be handled carefully.

This function is particularly useful for parsing rich text content, HTML-like fragments, or any XML that does not conform to the single-root-element requirement of a complete XML document.

## Examples

```xpath
parse-xml-fragment('<a/>text<b/>')
(: Returns a document node with children: <a/>, text node, <b/> :)

parse-xml-fragment('')
(: Returns a document node with no children :)

parse-xml-fragment('Hello &amp; goodbye')
(: Returns a document node containing the text "Hello & goodbye" :)
```

## Error Codes

- `FODC0006` — The string is not well-formed XML content.

## See Also

- [fn:parse-xml](fn-parse-xml.md)
- [fn:serialize](fn-serialize.md)
