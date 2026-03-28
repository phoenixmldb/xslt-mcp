---
name: xml
category: output-method
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#xml-output-method
---

# XML Output Method

The default output method. Produces well-formed XML output.

## Serialization Parameters

| Parameter | Default | Description |
|-----------|---------|-------------|
| `version` | `1.0` | XML version (`1.0` or `1.1`). |
| `encoding` | `UTF-8` | Character encoding. |
| `indent` | `no` | Whether to add whitespace indentation. |
| `omit-xml-declaration` | `no` | Whether to suppress the XML declaration. |
| `standalone` | `omit` | Value of `standalone` in the declaration. |
| `doctype-public` | (none) | Public identifier for the DOCTYPE. |
| `doctype-system` | (none) | System identifier for the DOCTYPE. |
| `cdata-section-elements` | (none) | Elements whose text content uses CDATA sections. |
| `media-type` | `application/xml` | MIME media type. |
| `byte-order-mark` | `no` | Whether to emit a BOM. |
| `normalization-form` | `none` | Unicode normalization form. |
| `suppress-indentation` | (none) | Elements that should not be indented. (Since 3.0) |

## Behavior

- Produces a well-formed XML document or external general parsed entity.
- Special characters in text and attribute values are escaped (`<` as `&lt;`, `&` as `&amp;`, etc.).
- Empty elements may use self-closing tags (`<br/>`).
- Namespace declarations are output as needed.

## Examples

```xml
<xsl:output method="xml" version="1.0" encoding="UTF-8"
    indent="yes" omit-xml-declaration="no"/>
```

Output:
```xml
<?xml version="1.0" encoding="UTF-8"?>
<root>
  <item>Hello</item>
</root>
```

## See Also

- [xsl:output](../instructions/xsl-output.md)
- [HTML Output Method](html.md)
- [Text Output Method](text.md)
