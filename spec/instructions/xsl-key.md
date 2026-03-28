---
name: xsl:key
category: instruction
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#key
---

# xsl:key

Declares a named key for use with the `key()` function, enabling efficient lookup of nodes by value.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `name` | Yes | `eqname` | The key name, used in calls to `key()`. |
| `match` | Yes | `pattern` | Pattern identifying which nodes are indexed. |
| `use` | No | `expression` | Expression computing the key value(s) for each matched node. |
| `composite` | No | `"yes" \| "no"` | Whether the key is composite (multi-part). Default is `no`. (Since 3.0) |
| `collation` | No | `uri` | Collation used for comparing key values. |

## Content Model

When `use` is absent, the content is a sequence constructor providing the key value. When `use` is present, the element must be empty.

## Examples

### Simple Key

```xml
<xsl:key name="emp-by-dept" match="employee" use="@department"/>

<!-- Usage: key('emp-by-dept', 'Sales') -->
```

### Composite Key (3.0)

```xml
<xsl:key name="cell" match="cell" use="@row, @col" composite="yes"/>

<!-- Usage: key('cell', ($row, $col)) -->
```

### Key with Content

```xml
<xsl:key name="item-by-code" match="item">
  <xsl:value-of select="upper-case(@code)"/>
</xsl:key>
```

## Error Codes

- **XTSE1220**: The `match` pattern is invalid.
- **XTSE1205**: Neither `use` nor a sequence constructor is provided.
- **XTDE1260**: The key value is not an atomic value.

## See Also

- [xsl:for-each](xsl-for-each.md)
