---
name: xsl:sequence
category: instruction
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#sequence
---

# xsl:sequence

Returns a sequence of items (nodes and/or atomic values) without copying. Unlike `xsl:copy-of`, nodes are returned by reference rather than by deep copy.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | No | `expression` | Expression returning the sequence. |
| `on-empty` | No | `expression` | Value to return if the sequence is empty. (Since 3.0) |

## Content Model

When `select` is absent, the content is a sequence constructor. When `select` is present, the element must be empty.

## Examples

### Return a Sequence

```xml
<xsl:function name="my:get-items" as="element(item)*">
  <xsl:param name="doc" as="document-node()"/>
  <xsl:sequence select="$doc//item[@active = 'true']"/>
</xsl:function>
```

### Return Atomic Values

```xml
<xsl:variable name="values" as="xs:integer*">
  <xsl:sequence select="1 to 10"/>
</xsl:variable>
```

### Conditional Return

```xml
<xsl:function name="my:safe-divide" as="xs:decimal?">
  <xsl:param name="a" as="xs:decimal"/>
  <xsl:param name="b" as="xs:decimal"/>
  <xsl:sequence select="if ($b != 0) then $a div $b else ()"/>
</xsl:function>
```

## Error Codes

- **XTTE0505**: The value does not match the required type of the containing construct.

## See Also

- [xsl:copy-of](xsl-copy-of.md)
- [xsl:value-of](xsl-value-of.md)
- [xsl:variable](xsl-variable.md)
