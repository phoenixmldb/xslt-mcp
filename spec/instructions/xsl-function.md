---
name: xsl:function
category: instruction
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#stylesheet-functions
---

# xsl:function

Declares a stylesheet function that can be called from XPath expressions. The function body is a sequence constructor.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | Yes | `eqname` | The function name. Must be in a non-null namespace (must use a prefixed name). |
| `as` | No | `sequence-type` | Return type. Default is `item()*`. |
| `visibility` | No | `"public" \| "private" \| "final" \| "abstract"` | Visibility within a package. (Since 3.0) |
| `override-extension-function` | No | `"yes" \| "no"` | Whether this function overrides extension functions. Default is `yes`. |
| `identity-sensitive` | No | `"yes" \| "no"` | Whether the function depends on node identity. Default is `no`. (Since 3.0) |
| `cache` | No | `"yes" \| "no" \| "partial"` | Memoization hint. (Since 3.0) |
| `new-each-time` | No | `"yes" \| "no" \| "maybe"` | Whether the function can return different results each time. (Since 3.0) |

## Content Model

( `xsl:param`*, _sequence-constructor_ )

Parameters are positional (unlike template parameters). The number of `xsl:param` children determines the function arity.

## Examples

### Simple Function

```xml
<xsl:function name="my:full-name" as="xs:string">
  <xsl:param name="person" as="element(person)"/>
  <xsl:sequence select="concat($person/first, ' ', $person/last)"/>
</xsl:function>

<!-- Usage: my:full-name(person[1]) -->
```

### Function with Multiple Arities

```xml
<xsl:function name="my:format-price" as="xs:string">
  <xsl:param name="amount" as="xs:decimal"/>
  <xsl:sequence select="format-number($amount, '$#,##0.00')"/>
</xsl:function>

<xsl:function name="my:format-price" as="xs:string">
  <xsl:param name="amount" as="xs:decimal"/>
  <xsl:param name="currency" as="xs:string"/>
  <xsl:sequence select="concat($currency, format-number($amount, '#,##0.00'))"/>
</xsl:function>
```

## Error Codes

- **XTSE0740**: The function name is not in a namespace.
- **XTSE0770**: Two functions have the same expanded QName and the same arity.
- **XTTE0780**: The return value does not match the type declared in `as`.
- **XTSE0010**: Content other than `xsl:param` appears before the first non-`xsl:param` child.

## See Also

- [xsl:param](xsl-param.md)
- [xsl:sequence](xsl-sequence.md)
