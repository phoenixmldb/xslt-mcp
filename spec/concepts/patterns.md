---
name: Patterns
category: concept
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#patterns
---

# Patterns

Patterns are expressions used in XSLT to match nodes. They appear primarily in the `match` attribute of `xsl:template`, `xsl:key`, `xsl:accumulator-rule`, `xsl:number`, and other instructions. Although patterns resemble XPath expressions syntactically, they serve a fundamentally different purpose: while an XPath expression selects nodes by navigating from a context, a pattern tests whether a given node matches certain criteria. The evaluation model is inverted -- rather than "starting here, find nodes that match," patterns answer "does this node match?"

Patterns are a restricted subset of XPath expressions. Not all valid XPath expressions are valid patterns. The restrictions ensure that pattern matching can be implemented efficiently -- a processor should be able to test whether a node matches a pattern without needing to evaluate arbitrary XPath navigations.

## Pattern Syntax

The basic forms of patterns are:

**Name patterns**: Match elements or attributes by name. `item` matches any `item` element. `@type` matches any `type` attribute. `ns:item` matches `item` in a specific namespace.

**Wildcard patterns**: `*` matches any element. `@*` matches any attribute. `ns:*` matches any element in a namespace. `*:local` matches any element with a given local name regardless of namespace.

**Kind test patterns**: `node()` matches any node. `text()` matches text nodes. `comment()` matches comments. `processing-instruction()` or `processing-instruction('target')` matches processing instructions. `document-node()` matches document nodes. `element()` and `attribute()` with optional type annotations match typed nodes.

**Path patterns**: Patterns can include path steps using `/` and `//`. `section/para` matches a `para` element whose parent is `section`. `chapter//note` matches a `note` element that is a descendant of `chapter`. `/` matches the document node. `/catalog` matches a `catalog` element that is the document element.

**Predicate patterns**: Patterns can include predicates. `item[@type='book']` matches `item` elements with a `type` attribute equal to `'book'`. `item[position() = 1]` matches the first `item` in context (though predicate semantics in patterns are subtle).

**Union patterns**: Multiple patterns can be combined with `|`. `h1 | h2 | h3` matches any of those elements.

## Root Patterns

A pattern beginning with `/` is a **root pattern** that anchors the match at the document node. `/` alone matches the document node. `/html` matches an `html` element that is the child of the document node (the document element). Root patterns provide absolute positioning within the document hierarchy.

## Key Patterns

XSLT 3.0 allows patterns of the form `key('keyname', 'value')` which match nodes that are returned by the `key()` function for the given key name and value. This provides an efficient way to match nodes based on indexed lookups rather than structural position.

## Priority Rules

When multiple template rules match a node, the processor uses priority to select the best match. Each pattern has either an explicit priority (from the `priority` attribute on `xsl:template`) or a computed **default priority**.

Default priority is calculated as follows:

| Pattern Form | Default Priority |
|---|---|
| `node()`, `*`, `@*`, `text()`, `comment()`, `processing-instruction()`, `document-node()` | -0.5 |
| `ns:*`, `@ns:*`, `*:local` | -0.25 |
| `name`, `@name`, `processing-instruction('target')` | 0 |
| Patterns with predicates, path steps, or `key()` | 0.5 |

When a pattern is a union (`A | B`), each alternative is treated as a separate template rule with its own default priority. The pattern `* | @*` is equivalent to two template rules, each with priority -0.5.

XSLT 3.0 refines the priority calculation further. A pattern like `element(*, xs:integer)` has priority 0 (a kind test with a type), while `schema-element(invoice)` has priority 0.25.

## How Patterns Differ from XPath Expressions

The key differences between patterns and XPath expressions:

1. **Direction**: XPath expressions navigate forward from a context. Patterns are matched against a node -- the processor tests whether the node satisfies the pattern, which conceptually involves navigating backward (up to ancestors).

2. **Restricted axes**: Patterns primarily use the child and attribute axes (forward) and implicitly the parent axis (backward via `/`). Axes like `following-sibling`, `preceding`, and `descendant` are not available as top-level pattern steps (though `//` is allowed as shorthand for descendant-or-self).

3. **Predicate context**: In an XPath expression, predicates have a well-defined context position and size. In patterns, predicates are evaluated with the matched node as context, but position and size depend on the sibling context, which can be surprising.

4. **No variables at top level**: In XSLT 2.0 and earlier, pattern predicates could not reference variables. XSLT 3.0 relaxes this -- variables may be referenced in pattern predicates.

## Named Patterns (XSLT 3.0)

XSLT 3.0 does not have "named patterns" as a distinct feature, but it significantly extends pattern syntax. The `match` attribute now supports additional forms including ancestor patterns using the `ancestor::` and `ancestor-or-self::` axes within predicates, and type-based patterns like `element(*, xs:date)` that match based on schema type annotations.

## Examples

```xml
<!-- Simple element match -->
<xsl:template match="para">
  <p><xsl:apply-templates/></p>
</xsl:template>

<!-- Path pattern: para inside section -->
<xsl:template match="section/para">
  <p class="section-para"><xsl:apply-templates/></p>
</xsl:template>

<!-- Predicate pattern -->
<xsl:template match="item[@status='active']">
  <div class="active"><xsl:apply-templates/></div>
</xsl:template>

<!-- Document node match -->
<xsl:template match="/">
  <html><body><xsl:apply-templates/></body></html>
</xsl:template>

<!-- Union pattern -->
<xsl:template match="h1 | h2 | h3">
  <heading level="{local-name()}"><xsl:apply-templates/></heading>
</xsl:template>

<!-- Root-anchored pattern -->
<xsl:template match="/catalog/item">
  <entry><xsl:apply-templates/></entry>
</xsl:template>

<!-- Descendant pattern -->
<xsl:template match="chapter//footnote">
  <fn><xsl:apply-templates/></fn>
</xsl:template>

<!-- Attribute match -->
<xsl:template match="@href">
  <xsl:attribute name="href" select="resolve-uri(., base-uri(..))"/>
</xsl:template>
```

## See Also

- [Template Rules](template-rules.md)
- [xsl:template](../instructions/xsl-template.md)
- [xsl:key](../instructions/xsl-key.md)
- [Modes](modes.md)
