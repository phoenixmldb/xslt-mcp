---
name: Whitespace Handling
category: concept
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#strip
---

# Whitespace Handling

Whitespace handling in XSLT is one of the most frequently misunderstood aspects of the language. There are multiple interacting mechanisms that control how whitespace is preserved or stripped in both the input document and the output, and their interactions can produce surprising results. Understanding these mechanisms is essential for producing correctly formatted output, especially when dealing with mixed content, HTML generation, or text-oriented transformations.

Whitespace in XSLT involves three distinct phases: whitespace in the source document (stripping/preservation), whitespace in the stylesheet itself (which text nodes are significant), and whitespace in the output (indentation, serialization).

## Whitespace Stripping in Source Documents

By default, all text nodes in the source document are preserved. The `xsl:strip-space` and `xsl:preserve-space` declarations control which whitespace-only text nodes are stripped from the source tree before processing begins.

```xml
<!-- Strip whitespace-only text nodes from all elements -->
<xsl:strip-space elements="*"/>

<!-- But preserve whitespace in specific elements -->
<xsl:preserve-space elements="pre code listing"/>
```

Important: only **whitespace-only** text nodes are affected. A text node containing "Hello World" is never stripped, even with `xsl:strip-space elements="*"`. Only text nodes consisting entirely of spaces, tabs, newlines, and carriage returns are candidates for stripping.

The `elements` attribute takes a whitespace-separated list of element names or wildcards:

- `*` -- All elements.
- `ns:*` -- All elements in a namespace.
- `element-name` -- A specific element.

When both `xsl:strip-space` and `xsl:preserve-space` match an element, the more specific match wins. If they have equal specificity, `xsl:preserve-space` takes precedence (at equal import precedence).

## The xml:space Attribute

The `xml:space` attribute in the source document provides element-level control:

- `xml:space="preserve"` -- Indicates that whitespace within this element is significant and should be preserved.
- `xml:space="default"` -- Indicates that the application's default whitespace handling should apply.

However, `xml:space` in the source document does NOT override `xsl:strip-space`. The XSLT processor's stripping rules take precedence. The `xml:space` attribute is primarily a signal to applications, and XSLT has its own mechanism (`xsl:strip-space`/`xsl:preserve-space`).

## Whitespace in the Stylesheet

Whitespace text nodes within the stylesheet itself follow specific rules:

1. **Text nodes in sequence constructors**: Whitespace-only text nodes are stripped from the stylesheet unless they are within `xsl:text`. A template body like:

```xml
<xsl:template match="item">
  <div>
    <xsl:value-of select="name"/>
  </div>
</xsl:template>
```

The newlines and indentation around `<div>` and `<xsl:value-of>` are whitespace-only text nodes. The XSLT processor strips these because they appear in the stylesheet between XSLT instructions and literal result elements.

2. **xsl:text preserves all content**: Text within `xsl:text` is always preserved, including whitespace:

```xml
<xsl:text>  Hello  World  </xsl:text>
```

This outputs exactly "  Hello  World  " including leading/trailing spaces.

3. **Boundary whitespace**: A whitespace-only text node in a sequence constructor is stripped unless it has a non-whitespace sibling text node. This is called **boundary whitespace** stripping.

## xsl:text and Whitespace Control

`xsl:text` is the primary mechanism for controlling whitespace in output:

```xml
<!-- Output a newline -->
<xsl:text>&#10;</xsl:text>

<!-- Output a space between values -->
<xsl:value-of select="first"/>
<xsl:text> </xsl:text>
<xsl:value-of select="last"/>

<!-- Output a tab-separated line -->
<xsl:text>Name&#9;Age&#9;City&#10;</xsl:text>
```

Without `xsl:text`, controlling the exact whitespace in output is difficult because the processor strips whitespace-only text nodes from the stylesheet. With `xsl:text`, every character between the opening and closing tags is preserved verbatim.

## Boundary Whitespace Rules

The rules for stripping whitespace text nodes from the stylesheet are:

1. A text node in a sequence constructor that consists entirely of whitespace is stripped if it is **boundary whitespace** -- meaning all text nodes in the sequence constructor are whitespace-only.

2. If a sequence constructor contains at least one non-whitespace text node, whitespace-only text nodes are preserved.

3. Text nodes within `xsl:text` are never stripped.

4. The `xml:space="preserve"` attribute on a stylesheet element preserves all whitespace text nodes within that element.

This means:

```xml
<!-- All whitespace is stripped (only whitespace text nodes present) -->
<xsl:template match="x">
  <a/>
  <b/>
</xsl:template>
<!-- Output: <a/><b/> -->

<!-- Whitespace preserved (non-whitespace text node "hello" is present) -->
<xsl:template match="x">
  hello
  <b/>
</xsl:template>
<!-- Output: hello\n  <b/> (approximately) -->
```

## Output Indentation

The `indent` attribute on `xsl:output` controls whether the serializer adds whitespace for readability:

```xml
<xsl:output method="xml" indent="yes"/>
```

When `indent="yes"`, the serializer inserts newlines and spaces between elements to create a hierarchical visual structure. The exact indentation is implementation-defined.

**Caution**: Indentation adds whitespace to the output that is not in the result tree. This can cause problems with mixed content elements and whitespace-sensitive contexts. For example:

```xml
<!-- Result tree produces: <p>Hello<b>World</b></p> -->
<!-- With indent="yes", output might be:
<p>
  Hello
  <b>World</b>
</p>
-->
```

The extra whitespace around "Hello" and `<b>` may cause rendering differences. XSLT 3.0 addresses this with `suppress-indentation`:

```xml
<xsl:output method="html" indent="yes" suppress-indentation="p span a em strong"/>
```

## Common Pitfalls

**Unexpected blank lines in output**: Usually caused by whitespace text nodes in the stylesheet that are not stripped. Common when mixing literal text with instructions:

```xml
<!-- Problem: newlines around xsl:value-of produce blank lines -->
<xsl:template match="item">
<xsl:value-of select="name"/>
</xsl:template>
```

**Missing spaces between values**: When two `xsl:value-of` or `xsl:apply-templates` instructions are adjacent, no space appears between their output:

```xml
<!-- Problem: outputs "JohnSmith" not "John Smith" -->
<xsl:value-of select="first"/><xsl:value-of select="last"/>

<!-- Fix: add explicit space -->
<xsl:value-of select="first"/>
<xsl:text> </xsl:text>
<xsl:value-of select="last"/>

<!-- Or use concat/string-join -->
<xsl:value-of select="string-join((first, last), ' ')"/>
```

**Whitespace in mixed content**: When transforming mixed content (elements containing both text and child elements), whitespace text nodes from the source often need to be preserved. Using `xsl:strip-space elements="*"` can strip significant whitespace from mixed content elements:

```xml
<!-- Source: <p>This is <b>bold</b> text.</p> -->
<!-- With strip-space="*", the spaces around <b> may be stripped -->
<!-- Fix: preserve-space for mixed content elements -->
<xsl:preserve-space elements="p li td th dd dt"/>
```

**Indentation corrupting text output**: Using `indent="yes"` with the text method has no effect (text method never indents), but using it with XML output that is later parsed can introduce unwanted whitespace nodes.

## Examples

```xml
<xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <!-- Strip whitespace from all source elements except pre and code -->
  <xsl:strip-space elements="*"/>
  <xsl:preserve-space elements="pre code"/>

  <!-- Output with indentation but protect inline elements -->
  <xsl:output method="html" indent="yes" suppress-indentation="p span a code em strong"/>

  <xsl:template match="section">
    <div class="section">
      <xsl:apply-templates/>
    </div>
  </xsl:template>

  <!-- Careful whitespace control in text output -->
  <xsl:template match="record" mode="csv">
    <xsl:value-of select="name"/>
    <xsl:text>,</xsl:text>
    <xsl:value-of select="age"/>
    <xsl:text>,</xsl:text>
    <xsl:value-of select="city"/>
    <xsl:text>&#10;</xsl:text>
  </xsl:template>

</xsl:stylesheet>
```

## See Also

- [xsl:text](../instructions/xsl-text.md)
- [xsl:strip-space](../instructions/xsl-strip-space.md)
- [xsl:preserve-space](../instructions/xsl-preserve-space.md)
- [xsl:output](../instructions/xsl-output.md)
- [Serialization](serialization.md)
