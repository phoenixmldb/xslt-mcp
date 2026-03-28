---
name: fn:key
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#function-key
---

# fn:key

Retrieves nodes using an XSLT key definition.

## Signature

```
fn:key($name as xs:string, $value as xs:anyAtomicType*) as node()*
fn:key($name as xs:string, $value as xs:anyAtomicType*, $top as node()) as node()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$name` | `xs:string` | The name of the key (as declared in `xsl:key`). |
| `$value` | `xs:anyAtomicType*` | The value(s) to look up. |
| `$top` | `node()?` | The document to search. Defaults to the document containing the context node. |

## Return Type

`node()*`

## Description

The `fn:key` function retrieves nodes from a document using a key defined by `xsl:key`. The `$name` parameter identifies the key definition, and `$value` specifies the lookup value(s). The function returns all nodes in the document whose key value matches any of the supplied values.

If `$top` is supplied, the search is restricted to the tree rooted at that node. Otherwise, the document containing the context node is searched. The key must be declared using `xsl:key` in the stylesheet. Multiple values can be provided in `$value`, and the function returns the union of matches for all values.

Keys provide indexed access to nodes, making lookups much more efficient than scanning with XPath predicates, especially for large documents with repeated lookups.

## Examples

```xpath
(: Given: <xsl:key name="emp-by-dept" match="employee" use="@department"/> :)

key('emp-by-dept', 'Sales')
(: Returns all employee elements with department="Sales" :)

key('emp-by-dept', ('Sales', 'Marketing'))
(: Returns employees in Sales or Marketing :)

key('emp-by-dept', 'IT', $other-doc)
(: Searches in a different document :)
```

## Error Codes

- `XTDE1260` — No key with the given name is declared.
- `XTDE1270` — The context node is not in a tree (when `$top` is not provided).

## See Also

- [fn:id](../node/fn-id.md)
- [fn:generate-id](../node/fn-generate-id.md)
