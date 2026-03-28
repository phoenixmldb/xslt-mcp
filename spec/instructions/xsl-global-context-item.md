---
name: xsl:global-context-item
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#global-context-item
---

# xsl:global-context-item

Declares the type and required status of the initial context item for the stylesheet as a whole.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `as` | No | `item-type` | The required type of the global context item. Default is `item()`. |
| `use` | No | `"required" \| "absent" \| "optional"` | Whether a global context item must be supplied. Default is `optional`. |

## Content Model

Empty.

## Description

`xsl:global-context-item` is a top-level declaration that specifies expectations about the initial context item supplied when the stylesheet is invoked. This controls whether the transformation requires a source document and what type it must be.

When `use="required"`, the invoker must supply a context item (typically a source document). When `use="absent"`, the stylesheet does not accept a context item and will raise an error if one is supplied. When `use="optional"`, the stylesheet works with or without a context item.

The `as` attribute constrains the type. For example, `as="document-node(element(root))"` requires the source to be a document with a `root` element. Type checking occurs before the transformation begins.

This declaration is particularly useful for stylesheet packages that define clear contracts about their inputs. It also helps processors optimize startup by knowing whether a source document is needed.

Only one `xsl:global-context-item` declaration is allowed per stylesheet module. If multiple imported modules declare it, they must be consistent.

## Examples

### Requiring a Source Document

```xml
<xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:global-context-item as="document-node()" use="required"/>

  <xsl:template match="/">
    <xsl:apply-templates/>
  </xsl:template>
</xsl:stylesheet>
```

### Stylesheet That Takes No Input

```xml
<xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:global-context-item use="absent"/>

  <xsl:template name="xsl:initial-template">
    <report generated="{current-dateTime()}">
      <xsl:for-each select="uri-collection('data/')">
        <xsl:apply-templates select="doc(.)"/>
      </xsl:for-each>
    </report>
  </xsl:template>
</xsl:stylesheet>
```

## Error Codes

- **XTDE0040**: A context item was required but not supplied.
- **XTDE0045**: A context item was supplied but `use="absent"`.
- **XTTE0050**: The context item does not match the declared type.

## See Also

- [xsl:context-item](xsl-context-item.md)
- [xsl:param](xsl-param.md)
