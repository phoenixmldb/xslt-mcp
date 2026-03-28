---
name: xsl:apply-templates
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#apply-templates
---

# xsl:apply-templates

Selects a sequence of nodes and applies template rules to each. This is the primary mechanism for recursive processing in XSLT.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | No | `expression` | XPath expression selecting nodes to process. Default is `child::node()`. |
| `mode` | No | `eqname | "#current" | "#default" | "#unnamed"` | The mode for matching template rules. Default is `#default`. |

## Content Model

( `xsl:sort`*, `xsl:with-param`* )

## Examples

### Basic Usage

```xml
<xsl:template match="book">
  <div>
    <xsl:apply-templates select="chapter"/>
  </div>
</xsl:template>
```

### With Sort and Parameters

```xml
<xsl:template match="library">
  <xsl:apply-templates select="book" mode="summary">
    <xsl:sort select="title"/>
    <xsl:with-param name="show-isbn" select="true()"/>
  </xsl:apply-templates>
</xsl:template>
```

### Processing All Children (Default)

```xml
<xsl:template match="section">
  <div>
    <!-- Processes all child nodes -->
    <xsl:apply-templates/>
  </div>
</xsl:template>
```

## Error Codes

- **XTDE0410**: No template rule matches the selected node (when the mode has `on-no-match="fail"`).
- **XTTE0505**: The result does not match the declared type of the mode.
- **XTDE0430**: The selected item is not a node.

## See Also

- [xsl:template](xsl-template.md)
- [xsl:sort](xsl-sort.md)
- [xsl:with-param](xsl-with-param.md)
- [Concepts: Template Rules](../concepts/template-rules.md)
- [Concepts: Modes](../concepts/modes.md)
