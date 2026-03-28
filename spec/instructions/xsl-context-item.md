---
name: xsl:context-item
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#context-item-declaration
---

# xsl:context-item

Declares the expected type and required status of the context item for a template rule.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `as` | No | `item-type` | The required type of the context item. Default is `item()`. |
| `use` | No | `"required" \| "absent" \| "optional"` | Whether the context item is required, must be absent, or is optional. Default is `required`. |

## Content Model

Empty.

## Description

`xsl:context-item` appears as a child of `xsl:template` to declare expectations about the context item when the template is invoked. This provides both documentation and runtime checking.

When `use="required"` (the default for template rules), the template expects a context item to be present and will raise an error if invoked without one. When `use="absent"`, the template must not be invoked with a context item. When `use="optional"`, the template can work with or without a context item.

The `as` attribute constrains the type of the context item. If the actual context item does not match the declared type, a type error is raised. This is useful for catching programming errors early, especially in large stylesheets where templates may be called from many locations.

For named templates (as opposed to template rules), the default for `use` is `optional`, reflecting the common pattern where named templates are called without a specific context.

## Examples

### Requiring a Specific Node Type

```xml
<xsl:template match="employee">
  <xsl:context-item as="element(employee)" use="required"/>
  <row>
    <xsl:value-of select="@name"/>
  </row>
</xsl:template>
```

### Named Template Without Context

```xml
<xsl:template name="generate-header">
  <xsl:context-item use="absent"/>
  <header>
    <title><xsl:value-of select="$page-title"/></title>
  </header>
</xsl:template>
```

## Error Codes

- **XTTE3090**: The context item does not match the declared type.
- **XTTE3091**: The context item is absent when `use="required"`.

## See Also

- [xsl:template](xsl-template.md)
- [xsl:global-context-item](xsl-global-context-item.md)
