---
name: fn:element-available
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#function-element-available
---

# fn:element-available

Tests whether an XSLT instruction element is available.

## Signature

```
fn:element-available($element-name as xs:string) as xs:boolean
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$element-name` | `xs:string` | The name of the instruction as a lexical QName. |

## Return Type

`xs:boolean`

## Description

The `fn:element-available` function returns `true` if the XSLT instruction identified by `$element-name` is available. The element name is resolved as a QName using the namespace declarations in scope. It tests for XSLT instruction elements (like `xsl:for-each-group`, `xsl:try`) and extension instruction elements.

For standard XSLT elements, this function returns `true` if the processor supports that instruction. This is useful for testing version-specific features: an XSLT 2.0 processor would return `false` for `xsl:try` (an XSLT 3.0 instruction).

This function is commonly used with `xsl:fallback` and `[xsl:use-when]` to provide backward-compatible stylesheets.

## Examples

```xpath
element-available('xsl:for-each-group')
(: Returns true for XSLT 2.0+ processors :)

element-available('xsl:try')
(: Returns true for XSLT 3.0 processors :)

element-available('xsl:merge')
(: Returns true for XSLT 3.0 processors :)
```

## Error Codes

- `XTDE1440` — The element name is not a valid QName.

## See Also

- [fn:function-available](fn-function-available.md)
- [fn:system-property](fn-system-property.md)
