---
name: fn:parse-json
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-parse-json
---

# fn:parse-json

Parses a JSON string into an XDM value.

## Signature

```
fn:parse-json($json-text as xs:string?) as item()?
fn:parse-json($json-text as xs:string?, $options as map(*)) as item()?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$json-text` | `xs:string?` | The JSON text to parse. |
| `$options` | `map(*)?` | Options controlling the parsing behavior. |

## Return Type

`item()?`

## Description

The `fn:parse-json` function parses a string containing JSON and converts it to the corresponding XDM representation. JSON objects are converted to `map(*)`, arrays to `array(*)`, strings to `xs:string`, numbers to `xs:double`, booleans to `xs:boolean`, and JSON null to the empty sequence.

If `$json-text` is the empty sequence, the function returns the empty sequence. The options map accepts the same keys as `fn:json-doc`: `liberal`, `duplicates`, `escape`, and `fallback`.

This function is essential when JSON data arrives as a string parameter, HTTP response body, or is embedded within XML content rather than being loaded from a file.

## Examples

```xpath
parse-json('{"name": "Alice", "age": 30}')
(: Returns map {"name": "Alice", "age": 30} :)

parse-json('[1, 2, 3]')?*
(: Returns the sequence (1, 2, 3) :)

let $json := parse-json('{"items": [{"id": 1}, {"id": 2}]}')
return $json?items?*?id
(: Returns (1, 2) :)
```

## Error Codes

- `FOJS0001` — The JSON input is not valid.
- `FOJS0005` — Duplicate keys when `duplicates` is `reject`.

## See Also

- [fn:json-doc](fn-json-doc.md)
- [fn:json-to-xml](fn-json-to-xml.md)
- [fn:xml-to-json](fn-xml-to-json.md)
