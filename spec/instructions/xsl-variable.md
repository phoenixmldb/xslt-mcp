---
name: xsl:variable
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#variables
---

# xsl:variable

Declares a variable binding. Variables are immutable once assigned. Can appear as a top-level declaration or within a sequence constructor.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | Yes | `eqname` | The variable name. |
| `select` | No | `expression` | XPath expression defining the variable's value. |
| `as` | No | `sequence-type` | Required type of the variable. |
| `static` | No | `"yes" | "no"` | Whether the variable is evaluated statically. Default is `no`. (Since 3.0) |
| `visibility` | No | `"public" | "private" | "final" | "abstract"` | Visibility within a package. Only for top-level variables. (Since 3.0) |

## Content Model

When `select` is absent, the content is a sequence constructor that determines the value. When `select` is present, the element must be empty.

If both `select` and content are absent, the value is a zero-length string, or the empty sequence if `as` is specified.

## Examples

### Simple Variable

```xml
<xsl:variable name="count" select="count(//item)"/>
```

### Variable with Type

```xml
<xsl:variable name="total" as="xs:decimal"
    select="sum(order/item/price)"/>
```

### Variable with Content

```xml
<xsl:variable name="header" as="element()">
  <header>
    <h1><xsl:value-of select="$title"/></h1>
  </header>
</xsl:variable>
```

## Error Codes

- **XTSE0120**: Duplicate top-level variable declarations with the same name and no overriding import precedence.
- **XTTE0570**: The value does not match the required type specified in `as`.
- **XTDE0640**: Circular variable definition detected.

## See Also

- [xsl:param](xsl-param.md)
- [xsl:sequence](xsl-sequence.md)
