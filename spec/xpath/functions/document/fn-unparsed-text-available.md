---
name: fn:unparsed-text-available
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-unparsed-text-available
---

# fn:unparsed-text-available

Tests whether a text resource can be successfully read.

## Signature

```
fn:unparsed-text-available($href as xs:string?) as xs:boolean
fn:unparsed-text-available($href as xs:string?, $encoding as xs:string) as xs:boolean
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$href` | `xs:string?` | The URI of the text resource. |
| `$encoding` | `xs:string?` | The character encoding to test. |

## Return Type

`xs:boolean`

## Description

The `fn:unparsed-text-available` function returns `true` if a call to `fn:unparsed-text` with the same arguments would succeed without error. It returns `false` if the resource cannot be retrieved or cannot be decoded using the specified encoding.

If `$href` is the empty sequence, the function returns `false`. This function never raises an error; it is designed as a safe guard before calling `fn:unparsed-text`.

This function is useful for conditionally loading optional text resources or providing fallback values.

## Examples

```xpath
if (unparsed-text-available('config.txt'))
then unparsed-text('config.txt')
else 'default-config'

unparsed-text-available('data.txt', 'utf-8')
(: true if the file exists and is valid UTF-8 :)
```

## Error Codes

None. This function does not raise errors.

## See Also

- [fn:unparsed-text](fn-unparsed-text.md)
- [fn:doc-available](fn-doc-available.md)
