---
name: fn:error
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-error
---

# fn:error

Raises a dynamic error.

## Signature

```
fn:error() as none
fn:error($code as xs:QName?) as none
fn:error($code as xs:QName?, $description as xs:string) as none
fn:error($code as xs:QName?, $description as xs:string, $error-object as item()*) as none
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$code` | `xs:QName?` | The error code as a QName. Defaults to `err:FOER0000`. |
| `$description` | `xs:string?` | A human-readable description of the error. |
| `$error-object` | `item()*` | Additional error information (implementation-dependent usage). |

## Return Type

`none` (this function never returns; it always raises an error)

## Description

The `fn:error` function raises a dynamic error, immediately terminating the current evaluation. The error code, description, and error object are made available to the error handling mechanism. If no arguments are provided, the default error code `err:FOER0000` is used.

The function has a return type of `none`, meaning it can be used in any context syntactically — it can appear where any type is expected because it never actually returns a value.

This function is the primary mechanism for raising application-specific errors in XPath and XSLT. In XSLT, errors raised by `fn:error` can be caught using `xsl:try`/`xsl:catch`.

## Examples

```xpath
error()
(: Raises FOER0000 :)

error(QName('http://example.com/err', 'app:INVALID'), 'Invalid input data')
(: Raises a custom error :)

if ($value < 0)
then error(QName('', 'NEGATIVE'), 'Value must be non-negative', $value)
else $value

(: Used in XSLT with try/catch: :)
(: <xsl:try select="error(QName('', 'TEST'), 'test error')">
     <xsl:catch>Caught: {$err:description}</xsl:catch>
   </xsl:try> :)
```

## Error Codes

- `FOER0000` — Default error code when none is specified.
- The function itself raises the error specified by its `$code` parameter.

## See Also

- [fn:trace](fn-trace.md)
