---
name: fn:local-name-from-QName
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-local-name-from-QName
---

# fn:local-name-from-QName

Returns the local name component of a QName.

## Signature

```
fn:local-name-from-QName($arg as xs:QName?) as xs:NCName?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `xs:QName?` | The QName. |

## Return Type

`xs:NCName?`

## Description

The `fn:local-name-from-QName` function returns the local name part of the supplied QName. If `$arg` is the empty sequence, the function returns the empty sequence.

This function is similar to `fn:local-name` but operates on QName values rather than nodes.

## Examples

```xpath
local-name-from-QName(QName('http://example.com', 'ex:item'))
(: Returns "item" :)

local-name-from-QName(QName('', 'local'))
(: Returns "local" :)
```

## Error Codes

None.

## See Also

- [fn:prefix-from-QName](fn-prefix-from-QName.md)
- [fn:namespace-uri-from-QName](fn-namespace-uri-from-QName.md)
- [fn:local-name](../node/fn-local-name.md)
