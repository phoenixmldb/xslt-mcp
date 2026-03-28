---
name: fn:json-to-xml
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-json-to-xml
---

# fn:json-to-xml

Converts a JSON string to an XML representation.

## Signature

```
fn:json-to-xml($json-text as xs:string?) as document-node()?
fn:json-to-xml($json-text as xs:string?, $options as map(*)) as document-node()?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$json-text` | `xs:string?` | The JSON text to convert. |
| `$options` | `map(*)?` | Options controlling the conversion. |

## Return Type

`document-node()?`

## Description

The `fn:json-to-xml` function takes a JSON string and produces an XML representation using elements in the namespace `http://www.w3.org/2005/xpath-functions`. JSON objects become `<map>` elements, arrays become `<array>` elements, strings become `<string>`, numbers become `<number>`, booleans become `<boolean>`, and null becomes `<null>`. Object keys appear as `@key` attributes.

If `$json-text` is the empty sequence, the function returns the empty sequence. This XML representation can be processed using standard XSLT template matching patterns, which is often more natural than navigating maps and arrays.

The options map supports the same keys as `fn:parse-json`. This function is the inverse of `fn:xml-to-json`.

## Examples

```xpath
json-to-xml('{"name": "Alice", "scores": [95, 87]}')
(: Returns:
  <map xmlns="http://www.w3.org/2005/xpath-functions">
    <string key="name">Alice</string>
    <array key="scores">
      <number>95</number>
      <number>87</number>
    </array>
  </map>
:)

json-to-xml('{"active": true}')//fn:boolean[@key='active']
(: Selects the boolean element :)
```

## Error Codes

- `FOJS0001` — The JSON input is not valid.
- `FOJS0005` — Duplicate keys when `duplicates` is `reject`.

## See Also

- [fn:xml-to-json](fn-xml-to-json.md)
- [fn:parse-json](fn-parse-json.md)
