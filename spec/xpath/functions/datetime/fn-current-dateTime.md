---
name: fn:current-dateTime
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-current-dateTime
---

# fn:current-dateTime

Returns the current date and time.

## Signature

```
fn:current-dateTime() as xs:dateTime
```

## Parameters

None.

## Return Type

`xs:dateTime`

## Description

The `fn:current-dateTime` function returns the current date and time from the dynamic context, including timezone information. Within a single query or transformation execution, this function always returns the same value (it is stable). This ensures consistent timestamps throughout a single processing run.

The returned value is consistent with `fn:current-date` and `fn:current-time`: extracting the date component gives the same value as `fn:current-date()`, and extracting the time component gives the same value as `fn:current-time()`.

This function is the most complete of the three current date/time functions and is commonly used for generating ISO timestamps and computing durations.

## Examples

```xpath
current-dateTime()
(: Returns e.g., 2024-03-15T14:30:00.000-05:00 :)

format-dateTime(current-dateTime(), '[Y0001]-[M01]-[D01]T[H01]:[m01]:[s01]Z')
(: Returns formatted ISO timestamp :)

current-dateTime() - xs:dateTime('2024-01-01T00:00:00')
(: Returns a dayTimeDuration :)
```

## Error Codes

None.

## See Also

- [fn:current-date](fn-current-date.md)
- [fn:current-time](fn-current-time.md)
