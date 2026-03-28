---
name: fn:unparsed-text-lines
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-unparsed-text-lines
---

# fn:unparsed-text-lines

Reads an external resource and returns its content as a sequence of lines.

## Signature

```
fn:unparsed-text-lines($href as xs:string?) as xs:string*
fn:unparsed-text-lines($href as xs:string?, $encoding as xs:string) as xs:string*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$href` | `xs:string?` | The URI of the text resource. |
| `$encoding` | `xs:string?` | The character encoding of the resource. |

## Return Type

`xs:string*`

## Description

The `fn:unparsed-text-lines` function reads an external text resource and returns its content as a sequence of strings, one per line. Lines are separated by newline characters (CR, LF, or CRLF). The line terminators are not included in the returned strings.

If `$href` is the empty sequence, the function returns the empty sequence. This function is equivalent to calling `fn:tokenize(fn:unparsed-text($href), '\r\n|\r|\n')` but is more convenient and potentially more efficient.

This function is particularly useful for processing CSV files, log files, and other line-oriented text data within XSLT transformations.

## Examples

```xpath
unparsed-text-lines('data.csv')
(: Returns each line of the CSV as a separate string :)

for $line in unparsed-text-lines('log.txt')
where contains($line, 'ERROR')
return $line
(: Filters lines containing ERROR :)

count(unparsed-text-lines('data.txt'))
(: Counts the number of lines :)
```

## Error Codes

- `FOUT1170` — The resource cannot be retrieved.
- `FOUT1190` — The resource cannot be decoded with the specified encoding.
- `FOUT1200` — The decoded text contains characters not permitted in XML.

## See Also

- [fn:unparsed-text](fn-unparsed-text.md)
- [fn:unparsed-text-available](fn-unparsed-text-available.md)
