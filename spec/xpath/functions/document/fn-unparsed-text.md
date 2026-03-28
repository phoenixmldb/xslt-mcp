---
name: fn:unparsed-text
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-unparsed-text
---

# fn:unparsed-text

Reads an external resource as a text string.

## Signature

```
fn:unparsed-text($href as xs:string?) as xs:string?
fn:unparsed-text($href as xs:string?, $encoding as xs:string) as xs:string?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$href` | `xs:string?` | The URI of the text resource. |
| `$encoding` | `xs:string?` | The character encoding of the resource (e.g., `"utf-8"`, `"iso-8859-1"`). |

## Return Type

`xs:string?`

## Description

The `fn:unparsed-text` function reads an external resource (such as a plain text file, CSV, or any non-XML file) and returns its contents as a string. If `$href` is the empty sequence, the function returns the empty sequence. Relative URIs are resolved against the static base URI.

If no encoding is specified, the processor determines the encoding from the media type, BOM, or defaults to UTF-8. If an encoding is specified, the resource is decoded using that encoding.

This function is commonly used to load CSV data, JSON text, configuration files, or other non-XML resources into an XSLT transformation. For line-by-line processing, use `fn:unparsed-text-lines` instead.

## Examples

```xpath
unparsed-text('data.csv')
(: Returns the entire contents of data.csv as a string :)

unparsed-text('legacy.txt', 'iso-8859-1')
(: Reads a file with Latin-1 encoding :)

tokenize(unparsed-text('data.csv'), '\n')
(: Splits the text file into lines :)
```

## Error Codes

- `FOUT1170` — The resource cannot be retrieved.
- `FOUT1190` — The resource cannot be decoded with the specified encoding.
- `FOUT1200` — The decoded text contains characters not permitted in XML.

## See Also

- [fn:unparsed-text-available](fn-unparsed-text-available.md)
- [fn:unparsed-text-lines](fn-unparsed-text-lines.md)
- [fn:doc](fn-doc.md)
