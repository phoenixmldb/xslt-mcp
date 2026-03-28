---
name: fn:data
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-data
---

# fn:data

Extracts the typed value (atomization) of a sequence of items.

## Signature

```
fn:data() as xs:anyAtomicType*
fn:data($arg as item()*) as xs:anyAtomicType*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `item()*` | The items to atomize. Defaults to the context item. |

## Return Type

`xs:anyAtomicType*`

## Description

The `fn:data` function extracts the typed values from a sequence of items through atomization. For atomic values, the value is returned unchanged. For nodes, the typed value is extracted: for untyped elements and attributes, this is an `xs:untypedAtomic` value; for schema-typed nodes, it is the typed value according to the schema.

If `$arg` is the empty sequence, the function returns the empty sequence. If no argument is provided, the context item is atomized.

This function makes atomization explicit. While XPath performs implicit atomization in many contexts (comparisons, function arguments), calling `fn:data` explicitly can improve clarity and is required in some contexts.

## Examples

```xpath
data(<price>9.99</price>)
(: Returns "9.99" as xs:untypedAtomic :)

data((1, 'hello', 3.14))
(: Returns (1, 'hello', 3.14) — atomic values pass through unchanged :)

data(//item/@price)
(: Returns the typed values of all price attributes :)
```

## Error Codes

- `FOTY0012` — If an item in the sequence is a function (functions have no typed value).
- `XPTY0004` — If the context item is not defined (when called without arguments).

## See Also

- [fn:string](../string/fn-string.md)
