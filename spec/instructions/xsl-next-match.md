---
name: xsl:next-match
category: instruction
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#next-match
---

# xsl:next-match

Invokes the next matching template rule for the current node.

## Attributes

None.

## Content Model

( `xsl:with-param`* , `xsl:fallback`* )

## Description

`xsl:next-match` searches for the template rule that would have matched the current node if the currently executing template had not existed. It walks the chain of matching templates in decreasing order of priority and import precedence, invoking the next one after the current template.

This is more flexible than `xsl:apply-imports`, which only considers templates with lower import precedence. `xsl:next-match` considers all matching templates regardless of how they were imported, making it suitable for building layered processing pipelines.

If no further matching template is found, the built-in template rule for the node kind is applied (unless `on-no-match="fail"` is set on the mode).

Parameters can be passed to the next template using `xsl:with-param` children. `xsl:fallback` children are only used for forward compatibility in XSLT 1.0 processors.

## Examples

### Adding Wrapper Around Default Processing

```xml
<xsl:template match="section" priority="2">
  <div class="section-wrapper">
    <xsl:next-match/>
  </div>
</xsl:template>

<xsl:template match="section" priority="1">
  <section>
    <xsl:apply-templates/>
  </section>
</xsl:template>
```

### Passing Parameters Down

```xml
<xsl:template match="item" priority="2">
  <xsl:next-match>
    <xsl:with-param name="depth" select="$depth + 1"/>
  </xsl:next-match>
</xsl:template>
```

## Error Codes

- **XTDE0560**: `xsl:next-match` is used outside a template that was invoked by `xsl:apply-templates`.

## See Also

- [xsl:apply-imports](xsl-apply-imports.md)
- [xsl:apply-templates](xsl-apply-templates.md)
- [xsl:template](xsl-template.md)
