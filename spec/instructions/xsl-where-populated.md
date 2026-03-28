---
name: xsl:where-populated
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#where-populated
---

# xsl:where-populated

Outputs its content only if the content is non-empty, preventing empty wrapper elements.

## Attributes

None.

## Content Model

Sequence constructor.

## Description

`xsl:where-populated` evaluates its body and produces the result only if the result contains at least one non-empty item. If the body produces an empty sequence or only whitespace-only text nodes, the entire result is discarded.

This solves a common problem in XSLT: generating wrapper elements that may end up empty. Without `xsl:where-populated`, you would need to test the content with an `xsl:if` or `xsl:choose`, which often requires evaluating the content twice.

The typical use case is wrapping an `xsl:apply-templates` or `xsl:for-each` in an element that should only appear if there is actual content. For example, a `<ul>` that should only be emitted if there are `<li>` children.

`xsl:where-populated` is related to but distinct from `xsl:on-empty` and `xsl:on-non-empty`. While those provide alternative or additional content based on emptiness, `xsl:where-populated` conditionally suppresses the entire output.

## Examples

### Conditional Wrapper Element

```xml
<xsl:where-populated>
  <ul>
    <xsl:for-each select="items/item[@visible='true']">
      <li><xsl:value-of select="@name"/></li>
    </xsl:for-each>
  </ul>
</xsl:where-populated>
<!-- Produces nothing if no visible items exist -->
```

### Optional Section

```xml
<xsl:where-populated>
  <section class="notes">
    <h2>Notes</h2>
    <xsl:apply-templates select="note"/>
  </section>
</xsl:where-populated>
```

### Avoiding Empty Attributes

```xml
<xsl:variable name="classes" as="xs:string?">
  <xsl:where-populated>
    <xsl:value-of select="string-join((@type, @status, @priority), ' ')"/>
  </xsl:where-populated>
</xsl:variable>
```

## Error Codes

None specific to this instruction.

## See Also

- [xsl:on-empty](xsl-on-empty.md)
- [xsl:on-non-empty](xsl-on-non-empty.md)
