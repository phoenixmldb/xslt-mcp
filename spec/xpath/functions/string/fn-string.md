---
name: fn:string
category: function
since: "1.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-string
---

# fn:string

Converts an item to its string value.

## Signature

```
fn:string() as xs:string
fn:string($arg as item()?) as xs:string
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$arg` | `item()?` | The item to convert. If omitted, uses the context item. |

## Returns

The string value of `$arg`. For nodes, this is the string value of the node. For atomic values, the canonical lexical representation. If `$arg` is the empty sequence, returns the zero-length string.

## Examples

```xml
<!-- String value of an element -->
<xsl:value-of select="string(price)"/>

<!-- String value of a number -->
string(42) → "42"

<!-- String of empty sequence -->
string(()) → ""

<!-- Context item -->
<xsl:for-each select="item">
  <xsl:value-of select="string()"/>
</xsl:for-each>
```

## Error Codes

- **XPDY0002**: The context item is absent when called with no argument.
- **FOTY0014**: `$arg` is a function item (functions have no string value).

## See Also

- [fn:string-join](fn-string-join.md)
- [fn:concat](fn-concat.md)
