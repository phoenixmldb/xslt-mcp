---
name: fn:system-property
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#function-system-property
---

# fn:system-property

Returns information about the XSLT processor and its capabilities.

## Signature

```
fn:system-property($property-name as xs:string) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$property-name` | `xs:string` | The name of the system property as a lexical QName. |

## Return Type

`xs:string`

## Description

The `fn:system-property` function returns the value of the named system property. The property name is a QName resolved against the namespace declarations in scope. Properties in the XSLT namespace (`http://www.w3.org/1999/XSL/Transform`) are defined by the specification:

- `xsl:version` — The XSLT version supported (e.g., "3.0").
- `xsl:vendor` — The name of the XSLT processor vendor.
- `xsl:vendor-url` — The URL of the vendor.
- `xsl:product-name` — The name of the XSLT product.
- `xsl:product-version` — The version of the product.
- `xsl:is-schema-aware` — "yes" or "no" indicating schema awareness.
- `xsl:supports-serialization` — "yes" or "no".
- `xsl:supports-backwards-compatibility` — "yes" or "no".

For properties not recognized, the function returns the zero-length string.

## Examples

```xpath
system-property('xsl:version')
(: Returns "3.0" for an XSLT 3.0 processor :)

system-property('xsl:vendor')
(: Returns e.g., "Saxonica" or "PhoenixML" :)

system-property('xsl:product-name')
(: Returns the product name :)

system-property('xsl:is-schema-aware')
(: Returns "yes" or "no" :)
```

## Error Codes

- `XTDE1390` — The property name is not a valid QName.

## See Also

- [fn:function-available](fn-function-available.md)
- [fn:element-available](fn-element-available.md)
