---
name: fn:current-time
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-current-time
---

# fn:current-time

Returns the current time.

## Signature

```
fn:current-time() as xs:time
```

## Parameters

None.

## Return Type

`xs:time`

## Description

The `fn:current-time` function returns the current time from the dynamic context. The returned value includes timezone information. Within a single query or transformation execution, this function always returns the same value (it is stable).

The value is consistent with `fn:current-dateTime` such that the time component of `fn:current-dateTime()` equals the value returned by `fn:current-time()`.

This function is commonly used for timestamping and time-based conditional logic.

## Examples

```xpath
current-time()
(: Returns e.g., 14:30:00.000-05:00 :)

hours-from-time(current-time())
(: Returns the current hour, e.g., 14 :)
```

## Error Codes

None.

## See Also

- [fn:current-date](fn-current-date.md)
- [fn:current-dateTime](fn-current-dateTime.md)
