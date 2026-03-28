---
name: fn:name
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-name
---

# fn:name

Returns the qualified name of a node as a string.

## Signature

```
fn:name() as xs:string
fn:name($arg as node()?) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `node()?` | The node whose name is returned. Defaults to the context item. |

## Return Type

`xs:string`

## Description

The `fn:name` function returns the name of the specified node as it would appear in the source document, including any namespace prefix. For an element `<html:div>`, it returns `"html:div"`. For nodes without names (text, comment, document), it returns the empty string.

If `$arg` is the empty sequence, the function returns the zero-length string. If no argument is provided, the context item is used.

Note that the prefix used is implementation-dependent — it is the prefix as it appears in the source or as assigned by the processor. For namespace-safe comparisons, use `fn:local-name` and `fn:namespace-uri` instead.

## Examples

```xpath
name(<html:div xmlns:html="http://www.w3.org/1999/xhtml"/>)
(: Returns "html:div" :)

name(<item/>)
(: Returns "item" :)

name(/)
(: Returns "" — document nodes have no name :)
```

## Error Codes

- `XPTY0004` — If the context item is not a node (when called without arguments).

## See Also

- [fn:local-name](fn-local-name.md)
- [fn:namespace-uri](fn-namespace-uri.md)
