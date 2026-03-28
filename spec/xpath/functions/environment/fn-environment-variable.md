---
name: fn:environment-variable
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-environment-variable
---

# fn:environment-variable

Returns the value of an operating system environment variable.

## Signature

```
fn:environment-variable($name as xs:string) as xs:string?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$name` | `xs:string` | The name of the environment variable. |

## Return Type

`xs:string?`

## Description

The `fn:environment-variable` function returns the value of the environment variable with the specified name. If the environment variable does not exist, or if the implementation does not provide access to environment variables, the function returns the empty sequence.

Environment variable names are case-sensitive on most platforms (Linux, macOS) but case-insensitive on Windows. The availability and behavior of this function may be restricted by the processor for security reasons.

This function is useful for configuring transformations based on deployment environment, reading API keys or paths, and parameterizing behavior without modifying the stylesheet.

## Examples

```xpath
environment-variable('HOME')
(: Returns e.g., "/home/user" on Linux :)

environment-variable('PATH')
(: Returns the system PATH :)

(environment-variable('MY_CONFIG'), 'default')[1]
(: Returns the env var value, or 'default' if not set :)
```

## Error Codes

None.

## See Also

- [fn:available-environment-variables](fn-available-environment-variables.md)
- [fn:system-property](../xslt/fn-system-property.md)
