---
name: csv
category: output-method
since: "4.0"
spec_url: https://www.w3.org/TR/xslt-40/#csv-output-method
---

# CSV Output Method

Produces comma-separated values output. Introduced in XSLT 4.0 (draft specification).

## Serialization Parameters

| Parameter | Default | Description |
|-----------|---------|-------------|
| `encoding` | `UTF-8` | Character encoding. |
| `media-type` | `text/csv` | MIME media type. |
| `field-delimiter` | `,` | Character separating fields. |
| `line-separator` | `\r\n` | Line ending sequence. |
| `quote-character` | `"` | Character used to quote fields. |

## Behavior

- The result is expected to be an array of arrays (rows of fields).
- Fields containing the delimiter, quote character, or line separators are quoted.
- Quote characters within fields are escaped by doubling.
- This is a draft specification; details may change.

## Examples

```xml
<xsl:output method="csv"/>

<xsl:template match="/">
  <xsl:array>
    <xsl:array-member>
      <xsl:array>
        <xsl:array-member select="'Name'"/>
        <xsl:array-member select="'Age'"/>
      </xsl:array>
    </xsl:array-member>
    <xsl:for-each select="people/person">
      <xsl:array-member>
        <xsl:array>
          <xsl:array-member select="string(name)"/>
          <xsl:array-member select="string(age)"/>
        </xsl:array>
      </xsl:array-member>
    </xsl:for-each>
  </xsl:array>
</xsl:template>
```

Output:
```
Name,Age
John Smith,42
Jane Doe,35
```

## See Also

- [Text Output Method](text.md)
- [JSON Output Method](json.md)
- [xsl:output](../instructions/xsl-output.md)
