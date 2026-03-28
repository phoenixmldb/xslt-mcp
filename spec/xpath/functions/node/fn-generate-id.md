---
name: fn:generate-id
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-generate-id
---

# fn:generate-id

Returns a unique string identifier for a node.

## Signature

```
fn:generate-id() as xs:string
fn:generate-id($arg as node()?) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `node()?` | The node for which to generate an ID. Defaults to the context item. |

## Return Type

`xs:string`

## Description

The `fn:generate-id` function returns a string that uniquely identifies a node within the current transformation. The same node always produces the same ID within a single execution, and different nodes always produce different IDs. The returned string is a valid XML name (NCName).

If `$arg` is the empty sequence, the function returns the zero-length string. The format of the generated ID is implementation-dependent and should not be relied upon to follow any particular pattern.

This function is commonly used to generate HTML `id` attributes, create cross-references between output elements, and implement Muenchian grouping in XSLT 1.0 stylesheets.

## Examples

```xpath
generate-id(.)
(: Returns a unique ID like "d1e42" :)

generate-id(/doc/item[1]) = generate-id(/doc/item[1])
(: Always true — same node, same ID :)

<a href="#{generate-id(.)}">Link</a>
(: Creates a link to an element's generated ID :)
```

## Error Codes

- `XPTY0004` — If the context item is not a node (when called without arguments).

## See Also

- [fn:id](fn-id.md)
- [fn:path](fn-path.md)
