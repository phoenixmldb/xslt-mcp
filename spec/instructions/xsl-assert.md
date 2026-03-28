---
name: xsl:assert
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#assert
---

# xsl:assert

Evaluates an assertion and raises an error if the test condition is false.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `test` | Yes | `expression` | Boolean expression that must evaluate to true. |
| `select` | No | `expression` | Expression producing the error message if the assertion fails. |
| `error-code` | No | `EQName` | QName of the error to raise. Default is `Q{http://www.w3.org/2005/xqt-errors}XTMM9001`. |

## Content Model

Sequence constructor (used to produce the error message if `select` is absent).

## Description

`xsl:assert` tests a condition and raises a dynamic error if the condition is not satisfied. It is analogous to assertions in programming languages and is intended to catch unexpected conditions during stylesheet execution.

When the `test` expression evaluates to `false` (using the effective boolean value), the processor raises an error with the code specified in `error-code`. The error message is produced either by the `select` attribute or by evaluating the sequence constructor body.

Assertions can be disabled by the processor as an optimization in production mode, similar to how assertions work in Java or C#. This means they should not be relied upon for validation of external input but are appropriate for documenting and checking invariants.

## Examples

### Validating Input

```xml
<xsl:assert test="exists($config)" error-code="my:ERR001"
            select="'Configuration document is required'"/>
```

### Checking Invariants

```xml
<xsl:template match="order">
  <xsl:assert test="@total = sum(item/@price)">
    Order total <xsl:value-of select="@id"/> does not match sum of items.
  </xsl:assert>
  <xsl:apply-templates/>
</xsl:template>
```

## Error Codes

- **XTMM9001**: Default error code when assertion fails and no `error-code` is specified.

## See Also

- [xsl:message](xsl-message.md)
- [xsl:try](xsl-try.md)
