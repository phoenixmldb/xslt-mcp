---
name: xsl:try
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#try
---

# xsl:try

Provides error handling. Evaluates the body, and if a dynamic error occurs, catches it with `xsl:catch` and evaluates the fallback.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | No | `expression` | Expression to try. |
| `rollback-output` | No | `"yes" \| "no"` | Whether to discard output from the failed try body. Default is `yes`. |

## Content Model

When `select` is absent: ( _sequence-constructor_, `xsl:catch`+ )
When `select` is present: ( `xsl:catch`+ )

## Examples

### Basic Error Handling

```xml
<xsl:try>
  <xsl:value-of select="xs:integer($input)"/>
  <xsl:catch>
    <xsl:text>Invalid number</xsl:text>
  </xsl:catch>
</xsl:try>
```

### Catching Specific Errors

```xml
<xsl:try>
  <xsl:copy-of select="doc($uri)"/>
  <xsl:catch errors="err:FODC0002">
    <error>Document not found: <xsl:value-of select="$uri"/></error>
  </xsl:catch>
  <xsl:catch errors="*">
    <error>Unexpected error: <xsl:value-of select="$err:description"/></error>
  </xsl:catch>
</xsl:try>
```

### Using xsl:catch Variables

Within `xsl:catch`, these variables are available:
- `$err:code` — the error code as a QName
- `$err:description` — the error description string
- `$err:value` — the error value (if any)
- `$err:module` — the URI of the stylesheet module
- `$err:line-number` — the line number where the error occurred
- `$err:column-number` — the column number

```xml
<xsl:try>
  <xsl:sequence select="my:risky-function($input)"/>
  <xsl:catch>
    <xsl:message select="'Error ' || $err:code || ': ' || $err:description"/>
    <fallback/>
  </xsl:catch>
</xsl:try>
```

## Error Codes

- **XTSE0010**: No `xsl:catch` child is present.

## See Also

- [xsl:message](xsl-message.md)
- [xsl:fallback](xsl-fallback.md)
