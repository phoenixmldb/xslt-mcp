---
name: Collations
category: concept
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#collations
---

# Collations

Collations define the rules for comparing and ordering strings. In XSLT, collations affect sorting (`xsl:sort`), grouping (`xsl:for-each-group`), duplicate elimination (`fn:distinct-values`), and string comparison functions (`fn:compare`, `fn:contains`, `fn:starts-with`, `fn:ends-with`). Choosing the correct collation is essential for producing linguistically correct results, particularly in multilingual applications.

A collation is identified by a URI. The XSLT specification defines one mandatory collation (Unicode codepoint collation) and allows processors to support additional collations, including the Unicode Collation Algorithm (UCA). The default collation for a stylesheet can be set with the `default-collation` attribute on `xsl:stylesheet` or on individual instructions.

## Unicode Codepoint Collation

The **Unicode codepoint collation** (`http://www.w3.org/2005/xpath-functions/collation/codepoint`) is the only collation that all XSLT processors must support. It compares strings by their Unicode code point values, character by character. This produces a well-defined, locale-independent ordering, but it is not linguistically correct for most natural languages:

- Uppercase letters sort before lowercase (A=65 < a=97).
- Accented characters sort after unaccented (z=122 < a-grave=224).
- Non-Latin scripts sort after Latin.

The codepoint collation is suitable for machine-oriented comparisons (identifiers, codes, URIs) but generally not for human-readable text.

## Default Collation

The `default-collation` attribute specifies the collation used when no explicit collation is given:

```xml
<xsl:stylesheet version="3.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    default-collation="http://www.w3.org/2005/xpath-functions/collation/codepoint">
```

The default collation can also be set on individual elements, providing a scoped override:

```xml
<xsl:for-each-group select="//name"
    group-by="."
    default-collation="http://www.w3.org/2013/collation/UCA?lang=en">
  ...
</xsl:for-each-group>
```

If no `default-collation` is specified, the processor's implementation-defined default applies (typically the codepoint collation).

## The HTML ASCII Case-Insensitive Collation

XPath 3.1 defines the **html-ascii-case-insensitive** collation (`http://www.w3.org/2005/xpath-functions/collation/html-ascii-case-insensitive`). This collation treats ASCII letters A-Z and a-z as equal but is otherwise identical to codepoint comparison. It does NOT perform full Unicode case folding -- only the 26 ASCII letter pairs are affected.

This collation is specifically designed for HTML processing where attribute values and element names are case-insensitive in the ASCII range. It is more predictable than a full case-insensitive Unicode collation.

## Unicode Collation Algorithm (UCA)

The **Unicode Collation Algorithm** (`http://www.w3.org/2013/collation/UCA`) provides linguistically correct string comparison for all languages. Processors that support UCA accept a collation URI with parameters:

```
http://www.w3.org/2013/collation/UCA?lang=de;strength=secondary
```

Common UCA parameters:

- **lang** -- BCP 47 language tag (e.g., `en`, `de`, `zh`, `ja`). Determines language-specific sorting rules.
- **strength** -- Comparison level:
  - `primary` -- Base characters only (a = A = a-grave).
  - `secondary` -- Base characters + accents (a = A, a != a-grave).
  - `tertiary` -- Base + accents + case (a != A, a != a-grave). This is the default.
  - `identical` -- Full comparison including code point.
- **alternate** -- How to handle variable characters (spaces, punctuation). `shifted` makes them ignorable at primary strength.
- **caseFirst** -- Whether uppercase sorts before lowercase (`upper`) or after (`lower`).
- **numeric** -- If `yes`, numbers embedded in strings are compared numerically ("file2" < "file10").
- **caseLevel** -- Adds a separate level for case distinctions.

UCA support is implementation-defined. Not all processors support it, and the supported parameters may vary.

## How Collations Affect Functions and Instructions

### fn:compare()

```xml
<!-- Codepoint comparison: 'a' < 'b' -->
<xsl:value-of select="compare('apple', 'banana')"/>  <!-- -1 -->

<!-- Case-insensitive comparison -->
<xsl:value-of select="compare('Apple', 'apple',
    'http://www.w3.org/2005/xpath-functions/collation/html-ascii-case-insensitive')"/>  <!-- 0 -->
```

### fn:contains(), fn:starts-with(), fn:ends-with()

These substring-matching functions accept an optional collation argument. With the codepoint collation, matching is exact. With a case-insensitive collation, matching ignores case:

```xml
<!-- Case-insensitive contains -->
<xsl:if test="contains($text, 'ERROR',
    'http://www.w3.org/2005/xpath-functions/collation/html-ascii-case-insensitive')">
```

### fn:distinct-values()

Duplicate elimination uses the collation to determine equality:

```xml
<!-- Without collation: 'Apple' and 'apple' are distinct -->
<xsl:value-of select="count(distinct-values(('Apple', 'apple')))"/>  <!-- 2 -->

<!-- With case-insensitive collation: they are the same -->
<xsl:value-of select="count(distinct-values(('Apple', 'apple'),
    'http://www.w3.org/2005/xpath-functions/collation/html-ascii-case-insensitive'))"/>  <!-- 1 -->
```

### xsl:sort

The `collation` attribute on `xsl:sort` controls sort order:

```xml
<xsl:for-each select="//person">
  <xsl:sort select="last-name" collation="http://www.w3.org/2013/collation/UCA?lang=de"/>
  <xsl:sort select="first-name"/>
  <name><xsl:value-of select="concat(last-name, ', ', first-name)"/></name>
</xsl:for-each>
```

German collation ensures that umlauted characters sort correctly (a < a-umlaut < b rather than a-umlaut sorting after z as it would with codepoint collation).

### xsl:for-each-group

The `collation` attribute affects `group-by` grouping:

```xml
<xsl:for-each-group select="//word" group-by="."
    collation="http://www.w3.org/2005/xpath-functions/collation/html-ascii-case-insensitive">
  <word value="{current-grouping-key()}" count="{count(current-group())}"/>
</xsl:for-each-group>
```

## Implementation-Defined Collation Support

Beyond the mandatory codepoint collation, processor support varies:

- Saxon supports UCA and Java-based locale collations.
- .NET-based processors may support .NET CultureInfo-based collations.
- Browser-based processors typically support only the codepoint collation.

When writing portable stylesheets, either use only the codepoint collation or test collation support and provide fallbacks.

## Examples

```xml
<!-- Set default collation for the stylesheet -->
<xsl:stylesheet version="3.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    default-collation="http://www.w3.org/2013/collation/UCA?lang=en">

  <!-- Sort names linguistically -->
  <xsl:template match="directory">
    <xsl:for-each select="person">
      <xsl:sort select="name"/>  <!-- uses default UCA collation -->
      <entry><xsl:value-of select="name"/></entry>
    </xsl:for-each>
  </xsl:template>

  <!-- Explicit codepoint comparison for identifiers -->
  <xsl:template match="codes">
    <xsl:for-each select="code">
      <xsl:sort select="."
          collation="http://www.w3.org/2005/xpath-functions/collation/codepoint"/>
      <item><xsl:value-of select="."/></item>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>
```

## See Also

- [xsl:sort](../instructions/xsl-sort.md)
- [xsl:for-each-group](../instructions/xsl-for-each-group.md)
