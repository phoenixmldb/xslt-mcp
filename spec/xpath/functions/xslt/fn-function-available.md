---
name: fn:function-available
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#function-function-available
---

# fn:function-available

Tests whether a named function is available for use.

## Signature

```
fn:function-available($function-name as xs:string) as xs:boolean
fn:function-available($function-name as xs:string, $arity as xs:integer) as xs:boolean
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$function-name` | `xs:string` | The name of the function as a lexical QName. |
| `$arity` | `xs:integer?` | The number of arguments. If omitted, tests for any arity. |

## Return Type

`xs:boolean`

## Description

The `fn:function-available` function returns `true` if a function with the given name (and optionally arity) is available in the static context. The function name is resolved as a QName using the namespace declarations in scope.

This function tests for built-in functions, stylesheet functions (`xsl:function`), and extension functions. If `$arity` is provided, it tests for a function with exactly that number of parameters.

This function is useful for writing portable stylesheets that can adapt to different processor capabilities, testing for extension functions before calling them, and implementing graceful degradation.

## Examples

```xpath
function-available('fn:serialize')
(: Returns true for XSLT 3.0 processors :)

function-available('fn:json-doc', 1)
(: Tests if single-argument json-doc is available :)

if (function-available('ext:my-function'))
then ext:my-function($data)
else $fallback
```

## Error Codes

- `XTDE1400` — The function name is not a valid QName.

## See Also

- [fn:element-available](fn-element-available.md)
- [fn:type-available](fn-type-available.md)
- [fn:system-property](fn-system-property.md)
