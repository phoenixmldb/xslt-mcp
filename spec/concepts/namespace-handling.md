---
name: Namespace Handling
category: concept
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#namespace
---

# Namespace Handling

Namespace management is a pervasive concern in XSLT. Because XSLT stylesheets are themselves XML documents that produce XML (or HTML) output, there are multiple namespace contexts in play simultaneously: the namespaces of the XSLT instructions, the namespaces of the source document, the namespaces of literal result elements in the stylesheet, and the namespaces desired in the output. Controlling which namespaces appear in the output and how they are mapped is one of the trickier aspects of XSLT development.

XSLT provides several mechanisms for namespace management: `exclude-result-prefixes` to suppress unwanted namespaces, `xsl:namespace` to explicitly create namespace nodes, `xsl:namespace-alias` for code-generation scenarios, `copy-namespaces` to control namespace copying, and automatic namespace fixup to ensure well-formedness.

## Namespace Context in Stylesheets

Every element in the stylesheet inherits a namespace context -- the set of in-scope namespace bindings from the XML declaration and ancestor elements. When a literal result element is output, all in-scope namespaces from its stylesheet context are copied to the output by default, which often includes unwanted namespaces.

For example:

```xml
<xsl:stylesheet version="3.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:fn="http://www.w3.org/2005/xpath-functions"
    xmlns:my="http://example.com/my">

  <xsl:template match="/">
    <html>  <!-- This outputs <html xmlns:fn="..." xmlns:my="..."> -->
      <body><xsl:apply-templates/></body>
    </html>
  </xsl:template>
</xsl:stylesheet>
```

The `<html>` literal result element would carry the `fn` and `my` namespace declarations to the output, which is almost never desired.

## exclude-result-prefixes

The `exclude-result-prefixes` attribute prevents specified namespace prefixes from being copied to the output when they appear on literal result elements:

```xml
<xsl:stylesheet version="3.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:fn="http://www.w3.org/2005/xpath-functions"
    xmlns:my="http://example.com/my"
    exclude-result-prefixes="fn my">
```

With this declaration, the `fn` and `my` namespaces are excluded from literal result elements. The special value `#all` excludes all non-XSLT namespaces:

```xml
exclude-result-prefixes="#all"
```

The `exclude-result-prefixes` attribute can also appear on any literal result element, providing scoped control:

```xml
<xsl:template match="/">
  <html xmlns:h="http://www.w3.org/1999/xhtml" exclude-result-prefixes="h">
    ...
  </html>
</xsl:template>
```

Note: `exclude-result-prefixes` only affects literal result elements. Namespaces on elements created by `xsl:element` or `xsl:copy` are controlled differently.

## extension-element-prefixes

The `extension-element-prefixes` attribute identifies namespaces used for extension instructions. Elements in these namespaces are treated as XSLT instructions (not literal result elements) and are not copied to the output:

```xml
<xsl:stylesheet version="3.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:saxon="http://saxon.sf.net/"
    extension-element-prefixes="saxon">
```

This is primarily relevant for processor-specific extensions. The XSLT namespace itself is automatically excluded.

## xsl:namespace Instruction

The `xsl:namespace` instruction (XSLT 2.0+) explicitly creates a namespace node on the parent element in the result tree:

```xml
<xsl:template match="/">
  <root>
    <xsl:namespace name="xs" select="'http://www.w3.org/2001/XMLSchema'"/>
    <xsl:namespace name="" select="'http://example.com/default'"/>
    ...
  </root>
</xsl:template>
```

This produces:

```xml
<root xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns="http://example.com/default">
```

The `name` attribute specifies the prefix (empty string for the default namespace), and the `select` attribute or string value specifies the namespace URI. This is useful when the output namespace is computed dynamically or when you need to create namespace declarations that do not exist in the stylesheet's namespace context.

## xsl:namespace-alias

The `xsl:namespace-alias` declaration maps one namespace to another in the output. This is essential when the stylesheet generates another XSLT stylesheet (or any XML vocabulary that uses the XSLT namespace):

```xml
<xsl:stylesheet version="3.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:out="http://www.w3.org/1999/XSL/Transform-alias">

  <xsl:namespace-alias stylesheet-prefix="out" result-prefix="xsl"/>

  <xsl:template match="/">
    <out:stylesheet version="3.0">
      <out:template match="/">
        <html><body>Generated</body></html>
      </out:template>
    </out:stylesheet>
  </xsl:template>
</xsl:stylesheet>
```

In the stylesheet, `out:` elements are recognized as literal result elements (not XSLT instructions). In the output, they are mapped to the `xsl:` namespace, producing a valid XSLT stylesheet.

## Namespace Fixup

XSLT processors perform **namespace fixup** to ensure that the output is namespace-well-formed. This includes:

1. **Adding namespace declarations**: If an element or attribute uses a namespace that is not declared in its context, the processor adds the necessary `xmlns` declaration.

2. **Avoiding conflicts**: If two different namespaces are bound to the same prefix in different parts of the result tree, the processor renames prefixes as needed to avoid conflicts.

3. **Default namespace handling**: If an element is in no namespace but its parent declares a default namespace, the processor may add `xmlns=""` to undeclare the default namespace.

Namespace fixup is automatic and generally transparent, but it can produce unexpected output when namespace contexts are complex. Understanding fixup behavior helps diagnose cases where unexpected namespace declarations appear in the output.

## copy-namespaces Attribute

The `copy-namespaces` attribute on `xsl:copy` and `xsl:copy-of` controls whether namespace nodes are copied when copying elements:

```xml
<!-- Copy element without its namespace nodes -->
<xsl:copy-of select="$element" copy-namespaces="no"/>

<!-- Copy element with all namespace nodes (default) -->
<xsl:copy-of select="$element" copy-namespaces="yes"/>
```

Setting `copy-namespaces="no"` strips inherited namespace declarations from copied elements. This is useful when copying elements from a namespace-rich source into a simpler output context. Only namespace declarations actually used by the element and its attributes are preserved through fixup.

## Default Namespace Complications

The default namespace (declared with `xmlns="..."` without a prefix) is a frequent source of confusion:

1. **In the source**: Elements in a default namespace require namespace-aware matching. `<xsl:template match="item">` does NOT match `<item xmlns="http://example.com/">`. You must either declare the namespace and use a prefix in the match pattern, or use `*:item`.

2. **In the stylesheet**: A default namespace on the stylesheet element affects literal result elements. If you declare `xmlns="http://www.w3.org/1999/xhtml"` on `xsl:stylesheet`, all literal result elements in templates will be in the XHTML namespace.

3. **Undeclaring**: In XML 1.0, you cannot undeclare a non-default namespace prefix (though XML 1.1 allows this). You can undeclare the default namespace with `xmlns=""`. XSLT processors handle this when constructing elements that should be in no namespace within a context that has a default namespace.

## Examples

```xml
<xsl:stylesheet version="3.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:my="http://example.com/functions"
    xmlns:data="http://example.com/data"
    exclude-result-prefixes="#all">

  <!-- Source uses default namespace -->
  <xsl:template match="data:catalog">
    <html>
      <body>
        <xsl:apply-templates select="data:item"/>
      </body>
    </html>
  </xsl:template>

  <!-- Dynamically add namespace -->
  <xsl:template match="data:item">
    <div>
      <xsl:if test="@format = 'svg'">
        <xsl:namespace name="svg" select="'http://www.w3.org/2000/svg'"/>
      </xsl:if>
      <xsl:apply-templates/>
    </div>
  </xsl:template>

  <!-- Copy without extra namespaces -->
  <xsl:template match="data:content">
    <xsl:copy copy-namespaces="no">
      <xsl:apply-templates select="@* | node()"/>
    </xsl:copy>
  </xsl:template>

</xsl:stylesheet>
```

## See Also

- [xsl:namespace](../instructions/xsl-namespace.md)
- [xsl:namespace-alias](../instructions/xsl-namespace-alias.md)
- [xsl:copy](../instructions/xsl-copy.md)
- [xsl:copy-of](../instructions/xsl-copy-of.md)
