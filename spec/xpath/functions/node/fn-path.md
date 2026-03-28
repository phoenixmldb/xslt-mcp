---
name: fn:path
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-path
---

# fn:path

Returns an XPath expression that uniquely identifies a node within its document.

## Signature

```
fn:path() as xs:string?
fn:path($arg as node()?) as xs:string?
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `node()?` | The node for which to generate a path. Defaults to the context item. |

## Return Type

`xs:string?`

## Description

The `fn:path` function returns a canonical XPath expression that can be used to locate the given node within its containing tree. The returned path uses abbreviated syntax with positional predicates to uniquely identify the node. For document nodes, it returns `/`. For elements, it produces paths like `/Q{ns}root[1]/Q{ns}child[2]`.

If `$arg` is the empty sequence, the function returns the empty sequence. The path uses Clark notation `Q{uri}local` for element and attribute names to avoid namespace prefix dependencies.

This function is particularly useful for error reporting, debugging, and generating references to specific nodes in diagnostic output.

## Examples

```xpath
path(/)
(: Returns "/" :)

path(/html/body/div[3])
(: Returns something like "/Q{}html[1]/Q{}body[1]/Q{}div[3]" :)

path(/root/@id)
(: Returns "/Q{}root[1]/@Q{}id" :)
```

## Error Codes

- `XPTY0004` — If the context item is not a node (when called without arguments).

## See Also

- [fn:root](fn-root.md)
- [fn:generate-id](fn-generate-id.md)
