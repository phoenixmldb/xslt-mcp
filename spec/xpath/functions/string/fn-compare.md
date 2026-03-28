---
name: fn:compare
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-compare
---

# fn:compare

Compares two strings using a specified collation.

## Signature

```
fn:compare($comparand1 as xs:string?, $comparand2 as xs:string?) as xs:integer?
fn:compare($comparand1 as xs:string?, $comparand2 as xs:string?, $collation as xs:string) as xs:integer?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$comparand1` | `xs:string?` | The first string. |
| `$comparand2` | `xs:string?` | The second string. |
| `$collation` | `xs:string?` | The collation URI for comparison. |

## Return Type

`xs:integer?`

## Description

The `fn:compare` function compares two strings and returns -1 if the first string is less than the second, 0 if they are equal, and 1 if the first is greater. The comparison uses the specified collation, or the default collation if none is specified.

If either argument is the empty sequence, the function returns the empty sequence. This three-valued return (-1, 0, 1) is useful for sorting and ordering operations.

The collation determines how strings are compared — whether case-sensitive, accent-sensitive, and the ordering of characters. The default Unicode codepoint collation compares strings by their Unicode code values.

## Examples

```xpath
compare('abc', 'def')
(: Returns -1 :)

compare('abc', 'abc')
(: Returns 0 :)

compare('def', 'abc')
(: Returns 1 :)

compare((), 'abc')
(: Returns () :)
```

## Error Codes

- `FOCH0002` — The collation URI is not supported.

## See Also

- [fn:deep-equal](../sequence/fn-deep-equal.md)
