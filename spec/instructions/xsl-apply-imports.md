---
name: xsl:apply-imports
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#apply-imports
---

# xsl:apply-imports

Invokes the overridden template rule from an imported stylesheet module.

## Attributes

None.

## Content Model

( `xsl:with-param`* )

## Description

`xsl:apply-imports` is used within a template rule to invoke the template that would have matched the current node if the importing stylesheet module had not been included. It provides a mechanism similar to calling a superclass method in object-oriented programming.

The instruction searches for template rules with lower import precedence than the rule currently being evaluated. The context node, context position, and context size remain unchanged. Parameters can be passed to the overridden template using `xsl:with-param` children.

`xsl:apply-imports` must appear within a template rule that was activated by `xsl:apply-templates`. It cannot be used in templates invoked by `xsl:call-template` or in named templates that were not matched by a pattern.

In XSLT 2.0+, `xsl:next-match` provides a more flexible alternative that considers all matching templates regardless of import precedence.

## Examples

### Extending an Imported Template

```xml
<!-- imported.xsl -->
<xsl:template match="para">
  <p><xsl:apply-templates/></p>
</xsl:template>

<!-- main.xsl -->
<xsl:import href="imported.xsl"/>

<xsl:template match="para">
  <div class="wrapper">
    <xsl:apply-imports/>
  </div>
</xsl:template>
```

### Passing Parameters

```xml
<xsl:template match="item">
  <xsl:apply-imports>
    <xsl:with-param name="mode" select="'enhanced'"/>
  </xsl:apply-imports>
</xsl:template>
```

## Error Codes

- **XTDE0560**: `xsl:apply-imports` is used in a template not invoked by `xsl:apply-templates`.

## See Also

- [xsl:next-match](xsl-next-match.md)
- [xsl:import](xsl-import.md)
- [xsl:apply-templates](xsl-apply-templates.md)
