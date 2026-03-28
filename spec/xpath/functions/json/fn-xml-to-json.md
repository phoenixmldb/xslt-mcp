---
name: fn:xml-to-json
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-xml-to-json
---

# fn:xml-to-json

Converts an XML representation of JSON back to a JSON string.

## Signature

```
fn:xml-to-json($input as node()?) as xs:string?
fn:xml-to-json($input as node()?, $options as map(*)) as xs:string?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$input` | `node()?` | The XML representation to convert (using the XPath functions namespace). |
| `$options` | `map(*)?` | Options controlling serialization (e.g., `indent` as xs:boolean). |

## Return Type

`xs:string?`

## Description

The `fn:xml-to-json` function converts an XML document using the JSON-XML representation (namespace `http://www.w3.org/2005/xpath-functions`) back into a JSON string. This is the inverse of `fn:json-to-xml`.

The input must conform to the schema for the JSON XML representation: `<map>`, `<array>`, `<string>`, `<number>`, `<boolean>`, and `<null>` elements with optional `@key` attributes. If `$input` is the empty sequence, the function returns the empty sequence.

The options map supports the key `indent` (xs:boolean) which controls whether the output JSON is pretty-printed. This function enables a workflow where JSON is loaded as XML, transformed using XSLT, and then serialized back to JSON.

## Examples

```xpath
xml-to-json(json-to-xml('{"name": "Alice"}'))
(: Returns '{"name":"Alice"}' :)

let $xml :=
  <map xmlns="http://www.w3.org/2005/xpath-functions">
    <string key="greeting">Hello</string>
    <number key="count">42</number>
  </map>
return xml-to-json($xml)
(: Returns '{"greeting":"Hello","count":42}' :)
```

## Error Codes

- `FOJS0006` — The input does not conform to the expected XML representation of JSON.

## See Also

- [fn:json-to-xml](fn-json-to-xml.md)
- [fn:serialize](../parsing/fn-serialize.md)
