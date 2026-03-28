---
name: fn:type-available
category: function
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#function-type-available
---

# fn:type-available

Tests whether a schema type is available.

## Signature

```
fn:type-available($type-name as xs:string) as xs:boolean
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$type-name` | `xs:string` | The name of the type as a lexical QName. |

## Return Type

`xs:boolean`

## Description

The `fn:type-available` function returns `true` if the schema type identified by `$type-name` is available in the static context. The type name is resolved as a QName using the namespace declarations in scope.

Built-in schema types (like `xs:integer`, `xs:string`, `xs:date`) are always available. User-defined types from imported schemas are also tested. This function is useful for testing schema-awareness capabilities and writing portable stylesheets.

This function is primarily relevant for schema-aware processors. Non-schema-aware processors will return `true` only for built-in types.

## Examples

```xpath
type-available('xs:integer')
(: Returns true — built-in type :)

type-available('xs:dateTimeStamp')
(: Returns true for XSD 1.1 types :)

type-available('my:CustomType')
(: Returns true if the schema type is imported :)
```

## Error Codes

- `XTDE1428` — The type name is not a valid QName.

## See Also

- [fn:function-available](fn-function-available.md)
- [fn:element-available](fn-element-available.md)
