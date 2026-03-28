---
name: xsl:import-schema
category: instruction
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#import-schema
---

# xsl:import-schema

Imports an XML Schema for schema-aware XSLT processing.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `namespace` | No | `uri` | Target namespace of the schema to import. |
| `schema-location` | No | `uri` | Location hint for the schema document. |

## Content Model

Optionally, an inline `xs:schema` element.

## Description

`xsl:import-schema` is a top-level declaration that makes XML Schema type definitions and element declarations available for use in the stylesheet. This enables schema-aware processing, including type annotations, validation, and type-based pattern matching.

At least one of `namespace` or `schema-location` must be specified, or an inline schema must be provided. The processor uses these to locate and load the schema definition.

Once a schema is imported, its types can be used in:
- `as` attributes for variables, parameters, and functions (e.g., `as="schema-element(invoice)"`)
- `match` patterns in templates (e.g., `match="element(*, orderType)"`)
- The `validation` and `type` attributes on construction instructions

Schema-aware processing is an optional feature of XSLT processors. Not all processors support it. When using `xsl:import-schema`, the stylesheet should use `version="2.0"` or higher and the processor must be schema-aware.

## Examples

### Importing by Namespace

```xml
<xsl:import-schema namespace="http://example.com/orders"
                   schema-location="orders.xsd"/>
```

### Importing with No Target Namespace

```xml
<xsl:import-schema schema-location="local-types.xsd"/>
```

### Using Imported Types

```xml
<xsl:import-schema namespace="http://example.com/types"/>

<xsl:template match="element(*, tns:addressType)">
  <div class="address">
    <xsl:apply-templates/>
  </div>
</xsl:template>
```

## Error Codes

- **XTSE0420**: Neither `namespace`, `schema-location`, nor inline schema is provided.
- **XTSE0005**: The processor does not support schema-aware processing.

## See Also

- [xsl:document](xsl-document.md)
- [xsl:element](xsl-element.md)
