---
name: fn:serialize
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-serialize
---

# fn:serialize

Serializes a sequence of items as a string.

## Signature

```
fn:serialize($arg as item()*) as xs:string
fn:serialize($arg as item()*, $params as item()?) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `item()*` | The items to serialize. |
| `$params` | `item()?` | Serialization parameters as an `xsl:output` element or a map. |

## Return Type

`xs:string`

## Description

The `fn:serialize` function converts a sequence of items into a string representation according to the serialization parameters. Without parameters, it uses XML serialization with default settings.

The `$params` argument can be an element node in the `xsl:output` namespace (using the same attributes as `xsl:output`) or, in XPath 3.1, a map with serialization parameter keys from the `http://www.w3.org/2010/xslt-xquery-serialization` namespace. Supported parameters include `method` (xml, html, xhtml, text, json, adaptive), `indent`, `omit-xml-declaration`, `encoding`, and others.

This function is useful for producing string output in specific formats, creating embedded XML/HTML/JSON strings, or debugging by converting nodes to their serialized form.

## Examples

```xpath
serialize(<root><item>Hello</item></root>)
(: Returns '<?xml version="1.0" encoding="UTF-8"?><root><item>Hello</item></root>' :)

serialize($node, map { 'method': 'html', 'indent': true() })
(: Serializes as indented HTML :)

serialize($data, map { 'method': 'json' })
(: Serializes a map/array as JSON :)

serialize($node, map { 'method': 'xml', 'omit-xml-declaration': true() })
(: Serializes as XML without the declaration :)
```

## Error Codes

- `SENR0001` — The item to serialize includes an attribute or namespace node that is not attached to an element.
- `SEPM0016` — Invalid serialization parameter value.

## See Also

- [fn:parse-xml](fn-parse-xml.md)
- [fn:parse-xml-fragment](fn-parse-xml-fragment.md)
