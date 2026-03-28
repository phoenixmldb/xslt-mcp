---
name: xsl:document
category: instruction
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#creating-document-nodes
---

# xsl:document

Creates a document node in the result tree.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `validation` | No | `"strict" \| "lax" \| "preserve" \| "strip"` | Validation mode for the document content. |
| `type` | No | `EQName` | Schema type for validation. |

## Content Model

Sequence constructor.

## Description

`xsl:document` creates a new document node wrapping the result of its body. The sequence constructor is evaluated and the resulting nodes become children of the new document node, following the rules for document node construction.

This instruction is useful when the result of a transformation needs to be a well-formed document node rather than a fragment. For example, when constructing a document to be stored in a variable or passed to a function that expects a document node.

The `validation` and `type` attributes control schema-aware validation of the constructed document. These are only relevant in schema-aware processors.

Note that `xsl:document` creates a document node in memory; it does not write to a file. To write output to a file, use `xsl:result-document`.

## Examples

### Creating a Document in a Variable

```xml
<xsl:variable name="temp-doc" as="document-node()">
  <xsl:document>
    <root>
      <xsl:apply-templates select="$items"/>
    </root>
  </xsl:document>
</xsl:variable>
```

### Building a Document for Further Processing

```xml
<xsl:variable name="enriched" as="document-node()">
  <xsl:document>
    <data>
      <xsl:for-each select="$records">
        <record id="{@id}" processed="{current-dateTime()}">
          <xsl:copy-of select="node()"/>
        </record>
      </xsl:for-each>
    </data>
  </xsl:document>
</xsl:variable>

<xsl:apply-templates select="$enriched/data/record"/>
```

## Error Codes

- **XTTE3160**: Validation fails for the constructed document.

## See Also

- [xsl:result-document](xsl-result-document.md)
- [xsl:copy-of](xsl-copy-of.md)
