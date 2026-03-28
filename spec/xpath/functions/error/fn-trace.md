---
name: fn:trace
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-trace
---

# fn:trace

Returns its input value while sending a trace message to the diagnostic output.

## Signature

```
fn:trace($value as item()*) as item()*
fn:trace($value as item()*, $label as xs:string) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$value` | `item()*` | The value to trace and return. |
| `$label` | `xs:string?` | A label to include in the diagnostic output. |

## Return Type

`item()*`

## Description

The `fn:trace` function returns `$value` unchanged while outputting a diagnostic message. The message typically includes the label (if provided) and a representation of the value. The destination and format of the trace output are implementation-defined (commonly stderr or a debug log).

This function is designed for debugging purposes. Because it returns its input unchanged, it can be inserted into any expression without altering the computation. This makes it ideal for inspecting intermediate values in complex expressions.

The trace output is a side effect and does not affect the result of the transformation. Implementations may choose to suppress trace output in production mode.

## Examples

```xpath
trace(//employee, 'employees')
(: Returns all employees and outputs a trace message :)

$price * trace($quantity, 'qty')
(: Traces the quantity value during multiplication :)

//item[trace(price, 'checking price') > 100]
(: Traces each price as it is evaluated in the predicate :)
```

## Error Codes

None.

## See Also

- [fn:error](fn-error.md)
