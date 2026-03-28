---
name: xsl:on-empty
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#on-empty
---

# xsl:on-empty

Provides alternative content when the enclosing sequence constructor produces an empty result.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | No | `expression` | Expression producing the alternative content. |

## Content Model

Sequence constructor (used if `select` is absent).

## Description

`xsl:on-empty` is evaluated only if the other instructions in the same sequence constructor produce no output. It must appear as the last instruction in its containing sequence constructor.

This is useful for providing default content, fallback values, or placeholder markup when the primary content is absent. It avoids the need for explicit `xsl:choose`/`xsl:if` tests to check whether a sequence is empty before processing it.

The "emptiness" check considers the effective result: if the sequence constructor produces only empty text nodes or empty sequences, `xsl:on-empty` activates. Whitespace-only text nodes from the stylesheet are typically not counted.

## Examples

### Default Message

```xml
<ul>
  <xsl:for-each select="items/item">
    <li><xsl:value-of select="@name"/></li>
  </xsl:for-each>
  <xsl:on-empty>
    <li class="empty">No items found.</li>
  </xsl:on-empty>
</ul>
```

### Fallback Value in Attribute

```xml
<xsl:variable name="label">
  <xsl:value-of select="@display-name"/>
  <xsl:on-empty select="@id"/>
</xsl:variable>
```

## Error Codes

- **XTSE0010**: `xsl:on-empty` is not the last instruction in the sequence constructor.

## See Also

- [xsl:on-non-empty](xsl-on-non-empty.md)
- [xsl:where-populated](xsl-where-populated.md)
