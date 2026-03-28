---
name: xsl:evaluate
category: instruction
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#evaluate
---

# xsl:evaluate

Dynamically evaluates an XPath expression supplied as a string at runtime.

## Attributes

| Attribute | Required | Type | Description |
|-----------|----------|------|-------------|
| `xpath` | Yes | `expression` | Expression returning the XPath string to evaluate. |
| `as` | No | `sequence-type` | Required type of the evaluation result. Default is `item()*`. |
| `base-uri` | No | `uri` | Base URI for resolving relative URIs in the evaluated expression. |
| `context-item` | No | `expression` | The context item for the evaluation. Defaults to the current context item. |
| `namespace-context` | No | `expression` | Node whose in-scope namespaces are used for resolving prefixes in the XPath. |
| `schema-aware` | No | `"yes" \| "no"` | Whether schema-aware XPath evaluation is enabled. |
| `with-params` | No | `expression` | A map supplying variable bindings (XSLT 4.0). |

## Content Model

( `xsl:with-param`* )

Parameters declared as children become variable bindings available in the evaluated expression.

## Description

`xsl:evaluate` is one of the most powerful instructions in XSLT 3.0, enabling dynamic XPath evaluation. The `xpath` attribute provides the expression to evaluate as a string, which is compiled and executed at runtime. This enables data-driven transformations where XPath expressions are stored in configuration files, databases, or the input document itself.

Variable bindings can be passed to the evaluated expression using `xsl:with-param` children. Each parameter name becomes a variable name accessible with `$` in the expression. In XSLT 4.0, the `with-params` attribute accepts a map for passing bindings more concisely.

The `namespace-context` attribute is critical when the dynamic expression contains namespace prefixes. It should reference a node whose in-scope namespaces provide the prefix-to-URI bindings. Without this, prefixed names in the dynamic expression will not resolve correctly.

Security note: `xsl:evaluate` executes arbitrary XPath expressions. Processors may provide configuration options to disable it for security purposes. When processing untrusted input, the XPath string should be validated or restricted.

## Examples

### Data-Driven Column Extraction

```xml
<!-- Config: <column xpath="@name"/> <column xpath="address/city"/> -->
<xsl:template match="record">
  <row>
    <xsl:for-each select="$config/column">
      <cell>
        <xsl:evaluate xpath="@xpath" context-item="$current-record"/>
      </cell>
    </xsl:for-each>
  </row>
</xsl:template>
```

### Dynamic Sort Key

```xml
<xsl:for-each select="$items">
  <xsl:sort>
    <xsl:evaluate xpath="$sort-expression"/>
  </xsl:sort>
  <xsl:copy-of select="."/>
</xsl:for-each>
```

### With Parameter Bindings

```xml
<xsl:evaluate xpath="'$x + $y * 2'">
  <xsl:with-param name="x" select="10"/>
  <xsl:with-param name="y" select="20"/>
</xsl:evaluate>
<!-- Result: 50 -->
```

### Namespace-Aware Evaluation

```xml
<xsl:variable name="ns-context" as="element()">
  <dummy xmlns:prod="http://example.com/products"/>
</xsl:variable>

<xsl:evaluate xpath="$user-xpath" namespace-context="$ns-context"
              context-item="."/>
```

## Error Codes

- **XTDE3150**: The XPath expression is syntactically invalid.
- **XTDE3170**: The result does not match the `as` type.
- **XTTE3175**: A required variable binding is missing.

## See Also

- [xsl:with-param](xsl-with-param.md)
- [xsl:function](xsl-function.md)
