---
name: xsl:fallback
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#fallback
---

# xsl:fallback

Provides fallback behavior when an instruction is not recognized by the processor.

## Attributes

None.

## Content Model

Sequence constructor.

## Description

`xsl:fallback` is used as a child of extension instructions or instructions from a higher XSLT version that the current processor does not support. When the processor encounters an unknown instruction, it looks for `xsl:fallback` children and evaluates them instead. If no `xsl:fallback` is present, the processor raises an error.

This mechanism enables forward-compatible processing. A stylesheet can use instructions from a newer XSLT version while providing fallback behavior for older processors. It can also be used with vendor-specific extension instructions to provide portable alternatives.

When `xsl:fallback` appears as a child of a recognized instruction, it is ignored during normal processing. It only takes effect when the parent instruction is not understood by the processor.

Multiple `xsl:fallback` elements can appear as children of the same unknown instruction. All of them are evaluated in order.

## Examples

### Forward Compatibility

```xml
<xsl:fancy-new-instruction xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:fallback>
    <xsl:message>xsl:fancy-new-instruction not supported, using fallback</xsl:message>
    <xsl:apply-templates/>
  </xsl:fallback>
</xsl:fancy-new-instruction>
```

### Extension Instruction Fallback

```xml
<ext:chart type="bar" data="$values" xmlns:ext="http://example.com/xslt-ext">
  <xsl:fallback>
    <table>
      <xsl:for-each select="$values">
        <tr><td><xsl:value-of select="."/></td></tr>
      </xsl:for-each>
    </table>
  </xsl:fallback>
</ext:chart>
```

## Error Codes

- **XTDE1450**: Unknown instruction encountered and no `xsl:fallback` is provided.

## See Also

- [xsl:message](xsl-message.md)
