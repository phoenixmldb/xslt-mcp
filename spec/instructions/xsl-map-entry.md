---
name: xsl:map-entry
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#creating-maps
---

# xsl:map-entry

Creates a single key-value entry within an `xsl:map`.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `key` | Yes | `expression` | Expression producing the key (must be an atomic value). |
| `select` | No | `expression` | Expression producing the value for this entry. |

## Content Model

Sequence constructor (used to produce the value if `select` is absent).

## Description

`xsl:map-entry` creates a single entry in an XDM map. The `key` attribute provides the map key, which must be an atomic value (string, integer, etc.). The value is provided by either the `select` attribute or the body sequence constructor.

The key can be any atomic type, but string keys are most common. When using maps for JSON output, keys must be strings.

If both `select` and a body are present, it is a static error. If neither is present, the value is the empty sequence.

## Examples

### String Key with Computed Value

```xml
<xsl:map-entry key="'total'" select="sum(//amount)"/>
```

### Complex Value from Body

```xml
<xsl:map-entry key="'items'">
  <xsl:sequence select="array { //item/string(@name) }"/>
</xsl:map-entry>
```

### Integer Key

```xml
<xsl:map-entry key="position()" select="string(.)"/>
```

## Error Codes

- **XTTE3365**: The key is not an atomic value.
- **XTSE0010**: Both `select` and a sequence constructor body are present.

## See Also

- [xsl:map](xsl-map.md)
