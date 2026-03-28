---
name: xsl:if
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#xsl-if
---

# xsl:if

Provides simple conditional processing. If the test expression evaluates to `true`, the content is instantiated; otherwise, nothing is produced.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `test` | Yes | `expression` | XPath expression evaluated as a boolean. Uses the effective boolean value. |

## Content Model

_sequence-constructor_

## Examples

### Basic Conditional

```xml
<xsl:if test="@status = 'active'">
  <span class="active">Active</span>
</xsl:if>
```

### Testing Existence

```xml
<xsl:if test="comment">
  <div class="comments">
    <xsl:apply-templates select="comment"/>
  </div>
</xsl:if>
```

### Numeric Test

```xml
<xsl:if test="count(item) > 0">
  <ul>
    <xsl:apply-templates select="item"/>
  </ul>
</xsl:if>
```

## Error Codes

- **XTSE0010**: The `test` attribute is required.
- **FORG0006**: The effective boolean value cannot be computed for the expression result.

## See Also

- [xsl:choose](xsl-choose.md)
- [xsl:when](xsl-when.md)
