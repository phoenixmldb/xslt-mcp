---
name: xsl:map
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#creating-maps
---

# xsl:map

Creates an XDM map from a sequence of key-value entries.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `select` | No | `expression` | Expression producing additional map entries to merge. Added in XSLT 4.0. |

## Content Model

( `xsl:map-entry` | other-instructions )*

The body can contain `xsl:map-entry` elements and other instructions that produce maps (which are merged into the result).

## Description

`xsl:map` constructs an XDM map, which is a collection of key-value pairs. Each `xsl:map-entry` child contributes one entry. Maps produced by other instructions in the body are merged into the result map.

Maps are a fundamental data structure in XSLT 3.0+ and XPath 3.1, enabling structured data manipulation beyond the XML node tree. They are used for lookup tables, configuration, JSON generation, and passing named parameters.

If duplicate keys appear, the last entry wins (entries from later children override earlier ones). This follows the semantics of `map:merge()` with `duplicates='use-last'`.

In XSLT 4.0, the `select` attribute allows providing additional map content from an expression, which is merged with entries produced by the body.

## Examples

### Simple Lookup Table

```xml
<xsl:variable name="colors" as="map(xs:string, xs:string)">
  <xsl:map>
    <xsl:map-entry key="'error'" select="'red'"/>
    <xsl:map-entry key="'warning'" select="'orange'"/>
    <xsl:map-entry key="'info'" select="'blue'"/>
  </xsl:map>
</xsl:variable>

<span style="color: {$colors($level)}">
  <xsl:value-of select="$message"/>
</span>
```

### Dynamic Map Construction

```xml
<xsl:variable name="index" as="map(xs:string, element())">
  <xsl:map>
    <xsl:for-each select="//item[@id]">
      <xsl:map-entry key="string(@id)" select="."/>
    </xsl:for-each>
  </xsl:map>
</xsl:variable>
```

### Nested Maps for JSON Generation

```xml
<xsl:variable name="json-data" as="map(*)">
  <xsl:map>
    <xsl:map-entry key="'name'" select="$person/@name/string()"/>
    <xsl:map-entry key="'age'" select="xs:integer($person/@age)"/>
    <xsl:map-entry key="'address'">
      <xsl:map>
        <xsl:map-entry key="'city'" select="$person/address/@city/string()"/>
        <xsl:map-entry key="'zip'" select="$person/address/@zip/string()"/>
      </xsl:map>
    </xsl:map-entry>
  </xsl:map>
</xsl:variable>

<xsl:output method="json"/>
<xsl:template match="/">
  <xsl:sequence select="$json-data"/>
</xsl:template>
```

## Error Codes

- **XTTE3375**: A child instruction produces a value that is not a map entry or map.

## See Also

- [xsl:map-entry](xsl-map-entry.md)
- [xsl:sequence](xsl-sequence.md)
