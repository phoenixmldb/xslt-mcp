---
name: xsl:message
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#message
---

# xsl:message

Sends a message to the XSLT processor. Typically used for debugging, progress reporting, or signaling errors. When `terminate="yes"`, execution stops.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | No | `expression` | Expression providing the message content. |
| `terminate` | No | `{ "yes" \| "no" }` | Whether to terminate execution. Default is `no`. |
| `error-code` | No | `{ eqname }` | Error code associated with the termination. Default is `XTMM9000`. (Since 3.0) |

## Content Model

When `select` is absent, the content is a sequence constructor. When `select` is present, the element must be empty.

## Examples

### Debug Message

```xml
<xsl:message select="'Processing: ' || @id"/>
```

### Terminate on Error

```xml
<xsl:if test="not(@required-attr)">
  <xsl:message terminate="yes"
      error-code="my:MISSING_ATTR">
    Required attribute 'required-attr' is missing on element
    <xsl:value-of select="name()"/>
  </xsl:message>
</xsl:if>
```

## Error Codes

- **XTMM9000**: Default error code when `terminate="yes"` and no `error-code` is specified.

## See Also

- [xsl:try](xsl-try.md)
- [xsl:assert](xsl-assert.md)
