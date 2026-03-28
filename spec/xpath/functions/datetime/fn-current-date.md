---
name: fn:current-date
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-current-date
---

# fn:current-date

Returns the current date.

## Signature

```
fn:current-date() as xs:date
```

## Parameters

None.

## Return Type

`xs:date`

## Description

The `fn:current-date` function returns the current date from the dynamic context. The returned value includes timezone information. Within a single query or transformation execution, this function always returns the same value (it is stable).

The timezone component of the result is the implicit timezone from the dynamic context. The time of day is not included in the result; for that, use `fn:current-time` or `fn:current-dateTime`.

This function is commonly used for date-stamping output, filtering date-based data, and computing date differences.

## Examples

```xpath
current-date()
(: Returns e.g., 2024-03-15-05:00 :)

year-from-date(current-date())
(: Returns the current year, e.g., 2024 :)

current-date() > xs:date('2024-01-01')
(: Returns true if the current date is after January 1, 2024 :)
```

## Error Codes

None.

## See Also

- [fn:current-time](fn-current-time.md)
- [fn:current-dateTime](fn-current-dateTime.md)
