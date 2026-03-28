---
name: fn:json-doc
category: function
since: "3.1"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-json-doc
---

# fn:json-doc

Reads a JSON document from a URI and returns it as an XDM value.

## Signature

```
fn:json-doc($href as xs:string?) as item()?
fn:json-doc($href as xs:string?, $options as map(*)) as item()?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$href` | `xs:string?` | The URI of the JSON resource. |
| `$options` | `map(*)?` | Options controlling the parsing behavior. |

## Return Type

`item()?`

## Description

The `fn:json-doc` function reads a JSON resource from the specified URI and parses it into XDM values. JSON objects become `map(*)`, JSON arrays become `array(*)`, strings become `xs:string`, numbers become `xs:double`, booleans become `xs:boolean`, and JSON null becomes the empty sequence.

If `$href` is the empty sequence, the function returns the empty sequence. The options map supports keys such as `liberal` (xs:boolean, allows non-standard JSON), `duplicates` (`reject`, `use-first`, `use-last`), `escape` (xs:boolean, controls backslash handling), and `fallback` (a function called for escaped characters).

This function is the JSON equivalent of `fn:doc` for XML. It is commonly used to integrate JSON APIs and data files into XSLT transformations.

## Examples

```xpath
json-doc('config.json')?settings?timeout
(: Reads config.json and navigates into settings/timeout :)

json-doc('data.json')?*
(: Returns all top-level values from a JSON object :)

json-doc('list.json')?*
(: Returns all members of a top-level JSON array :)
```

## Error Codes

- `FOJS0001` — The JSON input is not valid.
- `FOUT1170` — The resource cannot be retrieved.
- `FOJS0005` — Duplicate keys in the same object when `duplicates` is `reject`.

## See Also

- [fn:parse-json](fn-parse-json.md)
- [fn:json-to-xml](fn-json-to-xml.md)
- [fn:doc](../document/fn-doc.md)
