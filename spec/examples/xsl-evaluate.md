---
name: xsl:evaluate
category: example
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#evaluate
---

# Dynamic XPath evaluation with xsl:evaluate

## Stylesheet

```xml
<xsl:stylesheet version="3.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema">

  <!-- xpath expressions are stored in the config document -->
  <xsl:variable name="config" select="doc('config.xml')"/>

  <xsl:template name="xsl:initial-template">
    <xsl:variable name="source" select="doc('data.xml')"/>
    <results>
      <xsl:for-each select="$config/queries/query">
        <xsl:variable name="expr" select="string(@xpath)"/>
        <result name="{@name}">
          <!-- Evaluate a user-supplied XPath against the source document -->
          <xsl:evaluate xpath="$expr"
                        context-item="$source"
                        namespace-context="."/>
        </result>
      </xsl:for-each>
    </results>
  </xsl:template>

</xsl:stylesheet>
```

## Passing variables into the dynamic expression

```xml
<xsl:evaluate xpath="$expr"
              context-item="$source">
  <xsl:with-param name="threshold" select="42" as="xs:integer"/>
</xsl:evaluate>
```

Inside `$expr` the variable `$threshold` is then in scope.

## What it does

`xsl:evaluate` evaluates an XPath expression supplied as a string at
runtime. The `xpath` attribute is an AVT-like expression that yields the
XPath string; `context-item` sets the context node for the evaluation;
`namespace-context` provides a node whose in-scope namespaces are used to
resolve prefixes in the expression.

`xsl:with-param` children bind external variables into the evaluated
expression.

This is XSLT's mechanism for dynamic dispatch over XPath and is gated
behind the optional feature
`"http://www.w3.org/TR/xslt-30/#dt-evaluation-feature"` — check whether
your processor supports it before relying on it.

## Common pitfalls

- If the processor does not implement the evaluation feature, calling
  `xsl:evaluate` raises `XTDE3175`.
- Namespace prefixes inside the dynamic expression are resolved against
  `namespace-context`, **not** against the stylesheet's own namespace
  bindings (unless `namespace-context` happens to point to a stylesheet node).
- The expression is re-compiled on every invocation unless the processor
  caches it — repeated calls with the same string may be expensive.
- Only evaluate expressions from trusted sources; arbitrary XPath can
  read any file accessible to the processor.
