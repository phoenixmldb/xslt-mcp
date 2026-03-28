---
name: fn:document
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#function-document
---

# fn:document

Retrieves one or more XML documents by URI, with optional base URI resolution (XSLT-specific).

## Signature

```
fn:document($uri-sequence as item()*) as node()*
fn:document($uri-sequence as item()*, $base-node as node()) as node()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$uri-sequence` | `item()*` | A string or sequence of strings identifying the documents to retrieve. |
| `$base-node` | `node()?` | A node whose base URI is used for resolving relative URIs. |

## Return Type

`node()*`

## Description

The `fn:document` function is an XSLT-specific function that retrieves external XML documents. Unlike `fn:doc`, it can accept a sequence of URIs and return multiple document nodes. When a single string argument is provided, it behaves similarly to `fn:doc`.

If the first argument is a node-set (in XSLT 1.0 terms) or a sequence, each item is atomized to produce a URI, and the corresponding documents are returned. If a second argument is provided, the base URI of that node is used to resolve relative URIs in the first argument.

When the first argument is an empty string `""`, the function returns the root node of the stylesheet document containing the call. This is commonly used to access lookup data embedded in the stylesheet itself.

## Examples

```xpath
document('data.xml')
(: Loads data.xml relative to the stylesheet :)

document('data.xml', /)
(: Loads data.xml relative to the source document :)

document('')
(: Returns the root node of the current stylesheet :)

document(('file1.xml', 'file2.xml'))
(: Returns document nodes from both files :)
```

## Error Codes

- `XTDE1162` — A URI in the sequence identifies a resource that cannot be retrieved.

## See Also

- [fn:doc](fn-doc.md)
- [fn:collection](fn-collection.md)
