---
name: fn:local-name
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-local-name
---

# fn:local-name

Returns the local name of a node.

## Signature

```
fn:local-name() as xs:string
fn:local-name($arg as node()?) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `node()?` | The node whose local name is returned. Defaults to the context item. |

## Return Type

`xs:string`

## Description

The `fn:local-name` function returns the local part of the name of the specified node. For elements and attributes, this is the name without any namespace prefix. For processing instructions, this is the target name. For text, comment, and document nodes, it returns the empty string.

If `$arg` is the empty sequence, the function returns the zero-length string. If no argument is provided, the context item is used.

This function is commonly used in generic template rules that process elements by their local name regardless of namespace.

## Examples

```xpath
local-name(<html:div xmlns:html="http://www.w3.org/1999/xhtml"/>)
(: Returns "div" :)

local-name(<item/>)
(: Returns "item" :)

//*[local-name() = 'title']
(: Selects all elements with local name "title" in any namespace :)
```

## Error Codes

- `XPTY0004` — If the context item is not a node (when called without arguments).

## See Also

- [fn:name](fn-name.md)
- [fn:namespace-uri](fn-namespace-uri.md)
- [fn:local-name-from-QName](../qname/fn-local-name-from-QName.md)
