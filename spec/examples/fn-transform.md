---
name: fn:transform
category: example
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-30/#func-transform
---

# Invoking another stylesheet from XPath

## Stylesheet

```xml
<xsl:stylesheet version="3.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:map="http://www.w3.org/2005/xpath-functions/map">

  <xsl:template name="xsl:initial-template">
    <xsl:variable name="source-doc" select="doc('input.xml')"/>

    <!-- Call a separate stylesheet from XPath -->
    <xsl:variable name="result" select="
      fn:transform(map{
        'stylesheet-location' : 'html-renderer.xsl',
        'source-node'         : $source-doc,
        'delivery-format'     : 'document'
      })"/>

    <!-- The result map keys are result-document URIs; '' = principal output -->
    <xsl:sequence select="map:get($result, '')"/>
  </xsl:template>

</xsl:stylesheet>
```

## Passing parameters to the inner stylesheet

```xml
<xsl:variable name="result" select="
  fn:transform(map{
    'stylesheet-location' : 'report.xsl',
    'source-node'         : $source-doc,
    'stylesheet-params'   : map{
      QName('', 'title')   : 'Q3 Report',
      QName('', 'year')    : xs:integer(2025)
    },
    'delivery-format'     : 'document'
  })"/>
```

## What it does

`fn:transform` invokes an XSLT transformation from within an XPath
expression. The first argument is an options map. Common keys:
- `stylesheet-location` — URI of the stylesheet to apply
- `stylesheet-node` — an already-parsed stylesheet document node (avoids re-parsing)
- `source-node` — the principal source document
- `stylesheet-params` — map of `xs:QName → item()*` for top-level parameters
- `delivery-format` — `'document'` returns a map of result documents, `'raw'` returns the raw sequence

The return value is a map from result-document URI strings to document nodes.
The principal output is always at key `""` (empty string).

## Common pitfalls

- The return value is a **map**, not a document. Access the principal output
  via `map:get($result, '')` or equivalently `$result?''`.
- With `delivery-format: 'raw'`, the result is the raw XDM sequence — nodes
  are anchored in an inner document store that may not survive the call in all
  implementations. Prefer `'document'` for multi-hop pipelines.
- Stylesheet compilation is repeated each call unless you cache the compiled
  form via `xsl:param` or a global variable (implementation-specific).
- `stylesheet-params` keys must be `xs:QName` values (use `QName(namespace, local)`),
  not plain strings.
