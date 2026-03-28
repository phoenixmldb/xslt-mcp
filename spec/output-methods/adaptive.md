---
name: adaptive
category: output-method
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#adaptive-output-method
---

# Adaptive Output Method

Selects the serialization format based on the content of the result. This method can handle mixed content, serializing each item appropriately.

## Serialization Parameters

| Parameter | Default | Description |
|-----------|---------|-------------|
| `encoding` | `UTF-8` | Character encoding. |
| `indent` | `no` | Whether to indent. |
| `item-separator` | `\n` | Separator between top-level items. |
| `media-type` | (none) | MIME media type. |

## Behavior

- Document and element nodes are serialized using the XML output method.
- Maps are serialized as JSON objects.
- Arrays are serialized as JSON arrays.
- Atomic values are serialized as their string representation.
- Function items cause a serialization error.
- Items are separated by `item-separator` (default: newline).

## Examples

```xml
<xsl:output method="adaptive" indent="yes"/>

<xsl:template match="/">
  <xsl:sequence select="42"/>
  <xsl:sequence select="map{'key': 'value'}"/>
  <root><child>text</child></root>
</xsl:template>
```

Output:
```
42
{"key":"value"}
<root><child>text</child></root>
```

## See Also

- [JSON Output Method](json.md)
- [XML Output Method](xml.md)
- [Text Output Method](text.md)
