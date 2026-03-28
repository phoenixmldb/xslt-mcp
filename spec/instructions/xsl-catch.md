---
name: xsl:catch
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#catch
---

# xsl:catch

Error handler within `xsl:try` that processes caught dynamic errors.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `errors` | No | `name-tests` | Space-separated list of error QName patterns to catch. `*` matches any error. Default is `*`. |

## Content Model

Sequence constructor.

## Description

`xsl:catch` appears as a child of `xsl:try` and defines the fallback behavior when a dynamic error occurs during evaluation of the try body. Multiple `xsl:catch` elements can appear within a single `xsl:try`, each handling different error codes.

The `errors` attribute uses name tests (similar to match patterns for names) to specify which errors are caught. For example, `errors="err:FODC0002"` catches only the document-not-found error, while `errors="*"` catches all errors. Error codes in the `err` namespace refer to the standard XPath/XSLT error namespace `http://www.w3.org/2005/xqt-errors`. Multiple name tests can be separated by whitespace: `errors="err:FODC0002 err:FODC0005"`.

Within the body of `xsl:catch`, the following variables are automatically available in the `http://www.w3.org/2005/xqt-errors` namespace (conventionally bound to the `err` prefix):

- `$err:code` -- the error code as an `xs:QName`
- `$err:description` -- the error description as `xs:string?`
- `$err:value` -- an optional error value as `item()*`
- `$err:module` -- the URI of the stylesheet module where the error occurred, as `xs:string?`
- `$err:line-number` -- the line number as `xs:integer?`
- `$err:column-number` -- the column number as `xs:integer?`

The first `xsl:catch` whose `errors` pattern matches the error code is selected. If no `xsl:catch` matches, the error propagates.

## Examples

### Catching Specific Errors

```xml
<xsl:try>
  <xsl:copy-of select="doc($uri)"/>
  <xsl:catch errors="err:FODC0002">
    <error type="not-found">Document not found: <xsl:value-of select="$uri"/></error>
  </xsl:catch>
  <xsl:catch errors="*">
    <error type="unknown">
      Error <xsl:value-of select="$err:code"/>:
      <xsl:value-of select="$err:description"/>
    </error>
  </xsl:catch>
</xsl:try>
```

### Logging Error Details

```xml
<xsl:catch>
  <xsl:message select="'Error in ' || $err:module || ' line ' || $err:line-number"/>
  <fallback-result/>
</xsl:catch>
```

## Error Codes

- **XTSE0010**: `xsl:catch` appears outside of `xsl:try`.

## See Also

- [xsl:try](xsl-try.md)
- [xsl:fallback](xsl-fallback.md)
