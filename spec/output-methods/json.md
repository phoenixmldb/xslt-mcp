---
name: json
category: output-method
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#json-output-method
---

# JSON Output Method

Produces JSON output. The result tree must conform to the structure expected by the JSON output method (maps and arrays).

## Serialization Parameters

| Parameter | Default | Description |
|-----------|---------|-------------|
| `encoding` | `UTF-8` | Character encoding. |
| `indent` | `no` | Whether to indent with whitespace. |
| `media-type` | `application/json` | MIME media type. |

## Behavior

- The result must be a single map, array, string, number, boolean, or null.
- XDM maps become JSON objects.
- XDM arrays become JSON arrays.
- `xs:boolean` values become JSON `true`/`false`.
- The empty sequence becomes JSON `null`.
- Numbers become JSON numbers.
- Strings become JSON strings (with appropriate escaping).

## Examples

```xml
<xsl:output method="json" indent="yes"/>

<xsl:template match="/">
  <xsl:map>
    <xsl:map-entry key="'name'" select="person/name/string()"/>
    <xsl:map-entry key="'age'" select="person/age/xs:integer(.)"/>
    <xsl:map-entry key="'active'" select="true()"/>
    <xsl:map-entry key="'tags'">
      <xsl:array>
        <xsl:for-each select="person/tag">
          <xsl:array-member select="string(.)"/>
        </xsl:for-each>
      </xsl:array>
    </xsl:map-entry>
  </xsl:map>
</xsl:template>
```

Output:
```json
{
  "name": "John Smith",
  "age": 42,
  "active": true,
  "tags": ["developer", "lead"]
}
```

## See Also

- [xsl:output](../instructions/xsl-output.md)
- [Adaptive Output Method](adaptive.md)
- [XML Output Method](xml.md)
