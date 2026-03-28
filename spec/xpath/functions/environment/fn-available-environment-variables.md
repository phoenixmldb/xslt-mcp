---
name: fn:available-environment-variables
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-available-environment-variables
---

# fn:available-environment-variables

Returns the names of all available environment variables.

## Signature

```
fn:available-environment-variables() as xs:string*
```

## Parameters

None.

## Return Type

`xs:string*`

## Description

The `fn:available-environment-variables` function returns a sequence of strings containing the names of all environment variables that are available to the processor. The order of the names is implementation-dependent.

If the implementation does not provide access to environment variables, or if access is restricted for security reasons, the function returns the empty sequence. The returned names can be used as arguments to `fn:environment-variable` to retrieve their values.

This function is useful for discovering what configuration is available in the current environment and for diagnostic purposes.

## Examples

```xpath
available-environment-variables()
(: Returns e.g., ('HOME', 'PATH', 'USER', ...) :)

available-environment-variables()[starts-with(., 'MY_')]
(: Returns only environment variables starting with "MY_" :)

count(available-environment-variables())
(: Returns the number of available environment variables :)
```

## Error Codes

None.

## See Also

- [fn:environment-variable](fn-environment-variable.md)
