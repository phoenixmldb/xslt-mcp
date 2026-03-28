---
name: Streaming Rules
category: concept
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#streaming-concepts
---

# Streaming Rules

Streaming in XSLT 3.0 allows processors to handle documents of essentially unlimited size by reading the input as a sequential event stream rather than building a complete in-memory tree. The streaming rules are arguably the most technically complex part of the XSLT 3.0 specification. They define precisely which stylesheet constructs can operate in streaming mode and which cannot. Understanding these rules is essential for writing streamable stylesheets and for diagnosing the often cryptic errors that arise when streamability constraints are violated.

The fundamental constraint of streaming is that the input document is read once, sequentially, from start to end. At any point during processing, only the current node and its attributes and namespaces are fully accessible. Ancestors are accessible but only in a limited way (their names and attributes, not their other children). Preceding and following siblings are not accessible at all -- they have either already been read and discarded or have not yet been encountered. This means many common XSLT patterns that rely on random access to the tree (sorting by descendant values, cross-referencing between siblings, multi-pass processing) cannot be used in streaming mode.

## Posture Categories

The XSLT 3.0 specification defines a formal classification system for expressions based on how they navigate the streamed document. Every expression in a streamable construct is assigned a **posture** and a **sweep**, which together determine whether the expression is streamable and how it can be combined with other expressions.

The posture of an expression describes the set of nodes it may select relative to the streamed input:

- **Grounded**: The expression evaluates to a value that has no dependency on streamed nodes. Literal values, variables bound to grounded values, and calls to functions that return atomic values are grounded. A grounded expression can be used anywhere without restriction.

- **Rooted**: The expression is rooted at the root node of the streamed document. For example, the path `/root/header` starting from the document node is rooted. Rooted expressions navigate downward from the document root.

- **Striding**: The expression selects among the children of the context node. For example, `child::item` or `*` when the context is an element whose children are being streamed. Striding is the most common posture in streaming -- it represents the act of iterating over the children of the current node.

- **Crawling**: The expression navigates to descendants at any depth. For example, `.//item` or `descendant::para`. Crawling expressions consume the subtrees they visit.

- **Climbing**: The expression navigates upward (to ancestors) or accesses the current node. For example, `ancestor::section/@id` or `self::node()`. Climbing expressions are motionless with respect to the input stream because ancestor information is retained during streaming.

## Sweep Categories

The **sweep** of an expression describes how it consumes the input stream:

- **Motionless**: The expression does not advance the reading position. Accessing attributes of the context node, testing the name of the context node, and evaluating grounded expressions are motionless. Multiple motionless expressions can be evaluated at the same point.

- **Consuming**: The expression reads one or more events from the input stream. A `child::*` selection consumes the child events. The critical rule is that within any given construct, at most one consuming expression is permitted, because you cannot rewind the stream.

- **Absorbing**: The expression reads and discards events without producing a useful result. This occurs when a subtree is skipped over.

## Streamability Rules for Instructions

Each XSLT instruction has specific streamability rules:

**xsl:apply-templates**: Streamable when applied to a striding selection (the children of the context node). The select expression must be consuming, and the template bodies that match must individually be streamable. This is the primary mechanism for streaming -- mode-based dispatch over children.

**xsl:for-each**: Streamable when the select expression is striding or crawling. The body must be streamable with respect to each selected node. Unlike xsl:apply-templates, xsl:for-each processes nodes in document order without template dispatch.

**xsl:iterate**: The streaming workhorse. Designed specifically for streaming, xsl:iterate processes a sequence of nodes one at a time, maintaining explicit state through xsl:param/xsl:next-iteration. The select expression should be striding or crawling. Parameters carry state between iterations. xsl:iterate is the primary way to perform aggregation over streamed children.

**xsl:fork**: Allows multiple consuming operations to be performed in a single pass over the streamed children. Without xsl:fork, only one consuming expression is permitted. With xsl:fork, the processor conceptually "forks" the stream, allowing each branch to independently consume events. Each branch within xsl:fork must individually be streamable. This is essential when you need to produce multiple outputs from a single streamed input.

**xsl:value-of / xsl:copy-of**: Streamable when the select expression is motionless or consuming. Selecting the string value of the context node (`.`) is consuming because it requires reading all descendant text nodes.

**xsl:copy**: Streamable when applied to a streamed node. The processor copies the current node and its subtree by consuming the input events directly. This is the most efficient streaming pattern -- events are read and immediately written to the output.

**xsl:variable / xsl:param**: A variable bound to a consuming expression is only streamable if the variable is **grounded** -- that is, the value is fully materialized into a non-streaming form. You cannot bind a variable to a streamed subtree and then navigate it repeatedly. Use `copy-of()` or `snapshot()` to ground a streamed node.

**xsl:if / xsl:choose**: Streamable when the test/when conditions are motionless and the bodies are streamable. The conditions cannot consume the stream because the processor must evaluate the condition before deciding which branch to execute.

## The One-Consuming-Expression Rule

The most important streamability constraint is that within any sequence constructor, there may be at most **one** consuming expression. You cannot write:

```xml
<!-- NOT STREAMABLE: two consuming expressions -->
<xsl:template match="order">
  <total><xsl:value-of select="sum(item/price)"/></total>
  <count><xsl:value-of select="count(item)"/></count>
</xsl:template>
```

Both `sum(item/price)` and `count(item)` consume the child axis. The stream cannot be read twice. To solve this, use xsl:fork, xsl:iterate, or accumulators:

```xml
<!-- STREAMABLE: using xsl:fork -->
<xsl:template match="order">
  <xsl:fork>
    <xsl:sequence>
      <total><xsl:value-of select="sum(item/price)"/></total>
    </xsl:sequence>
    <xsl:sequence>
      <count><xsl:value-of select="count(item)"/></count>
    </xsl:sequence>
  </xsl:fork>
</xsl:template>
```

```xml
<!-- STREAMABLE: using xsl:iterate -->
<xsl:template match="order">
  <xsl:iterate select="item">
    <xsl:param name="total" select="0"/>
    <xsl:param name="cnt" select="0"/>
    <xsl:on-completion>
      <total><xsl:value-of select="$total"/></total>
      <count><xsl:value-of select="$cnt"/></count>
    </xsl:on-completion>
    <xsl:next-iteration>
      <xsl:with-param name="total" select="$total + price"/>
      <xsl:with-param name="cnt" select="$cnt + 1"/>
    </xsl:next-iteration>
  </xsl:iterate>
</xsl:template>
```

## Streamable Modes

Declaring `<xsl:mode streamable="yes"/>` tells the processor that all template rules in that mode must satisfy streamability constraints. The processor will analyze every template rule at compile time and report any that violate the rules. This is the primary way to enable streaming for template-rule-based processing.

When a mode is declared streamable, the following constraints apply to every template rule in that mode: the match pattern must be a valid streaming pattern, the template body must satisfy the streamability rules, and any called templates or functions must also be streamable when they receive streamed input. The streamability analysis is deep -- it follows function calls and named template invocations.

Streamable modes also affect the built-in template rules. In a streamable mode, the built-in template rule for element and document nodes applies templates to children in a streaming fashion. The built-in rule for text nodes outputs the text (a motionless operation on the context node). This means that a streamable mode with no user-defined templates will simply stream the text content of the document.

## The Role of Accumulators in Streaming

Accumulators provide a declarative mechanism for gathering information during streaming that would otherwise require multiple passes. An accumulator defines a set of rules that are evaluated as the processor traverses the document, maintaining a running value that is updated at each matching node.

Accumulators are inherently streaming-compatible because they are designed to work in a single-pass model. Each accumulator rule specifies a pattern and an expression that computes the new accumulator value. The accumulated value can be accessed at any point using `accumulator-before()` and `accumulator-after()`. This allows information to be gathered from nodes that have already been passed without violating the single-pass constraint.

A common use case is computing a running total or tracking the most recently seen value of a particular element:

```xml
<xsl:accumulator name="running-total" as="xs:decimal" initial-value="0" streamable="yes">
  <xsl:accumulator-rule match="transaction" select="$value + xs:decimal(@amount)"/>
</xsl:accumulator>

<xsl:template match="transaction">
  <entry amount="{@amount}" running-total="{accumulator-after('running-total')}"/>
</xsl:template>
```

## Guaranteed Streamable Patterns

The specification identifies certain patterns that are guaranteed to be streamable:

1. **Identity-like streaming**: Matching each element and copying it through with xsl:copy. This is the simplest streaming pattern -- events are read and immediately written.

2. **Filtering**: Matching specific children and copying or transforming them individually. Each template rule handles one child at a time, with only motionless operations on the child's attributes and consuming operations on the child's content.

3. **Aggregation via xsl:iterate**: Processing children sequentially and maintaining running totals or state through iteration parameters. The xsl:on-completion block can output the final result.

4. **Accumulator-based gathering**: Using accumulators to collect information during traversal without consuming the stream a second time.

5. **Burst-mode processing**: Using `xsl:source-document` with `streamable="yes"` to stream a document within an otherwise non-streaming stylesheet. This allows streaming to be applied selectively to large documents.

## Common Streaming Anti-Patterns

The following patterns are NOT streamable and represent common mistakes:

**Sorting streamed children**: `<xsl:for-each select="item"><xsl:sort select="@name"/>` is not streamable because sorting requires reading all items before any output, which defeats streaming.

**Grouping streamed children**: `xsl:for-each-group` is not streamable in most configurations because grouping typically requires seeing all items. However, `group-adjacent` and `group-starting-with` are streamable because they work sequentially.

**Accessing siblings**: Any expression using `preceding-sibling::` or `following-sibling::` is not streamable. Use accumulators to track values from preceding nodes.

**Multiple passes over children**: Any template that reads the children more than once (e.g., once to count them and once to process them) is not streamable. Use xsl:fork or xsl:iterate to combine multiple operations into a single pass.

**Conditional logic based on descendants**: `<xsl:if test="item[@type='special']">` at the parent level is not streamable because evaluating the condition requires consuming the children, leaving nothing for the body. Use accumulators to track such conditions.

**Binding variables to streamed subtrees**: `<xsl:variable name="items" select="item"/>` captures a reference to the children, but in streaming mode those children are transient. The variable must ground the value: `<xsl:variable name="items" select="copy-of(item)"/>`, but this defeats the purpose of streaming for large documents.

## Relationship to xsl:stream, xsl:source-document, and xsl:fork

The `xsl:stream` instruction (deprecated in favor of `xsl:source-document`) and `xsl:source-document` with `streamable="yes"` are the entry points for streaming. They specify the document to be streamed and provide a sequence constructor that must satisfy the streamability rules. Inside the construct, the context item is the document node of the streamed document.

The `xsl:fork` instruction is unique to streaming. It allows the body to contain multiple sequence constructors that each independently consume the streamed children. The processor handles this by buffering or parallel processing as needed. Without xsl:fork, you are limited to a single consuming traversal of the children.

Together with xsl:iterate and accumulators, these instructions form the complete toolkit for streaming in XSLT 3.0. The key insight is that streaming requires restructuring your stylesheet to process the document in a single sequential pass, gathering all needed information along the way rather than navigating back and forth through the tree.

## Streamability Analysis

Processors perform streamability analysis at compile time. This is a static analysis -- the processor examines the structure of the stylesheet without executing it and determines whether each construct satisfies the streamability rules. The analysis considers the posture and sweep of every expression and verifies that the combination rules are satisfied.

The analysis is compositional: the posture and sweep of a complex expression are determined by the postures and sweeps of its sub-expressions according to specific combination rules defined in the specification. For example, a path expression `A/B` where A is striding and B is motionless is striding overall. A path expression where A is striding and B is consuming is consuming overall.

If the analysis determines that a construct is not streamable, the processor must report an error at compile time. This is a significant advantage -- you do not need to run the stylesheet against a large document to discover streaming problems. The error messages typically identify the specific expression that violates the rules, though they can be difficult to interpret without understanding the posture/sweep framework.

## Examples

### Streaming a Large Log File

```xml
<xsl:mode streamable="yes"/>

<xsl:template match="/">
  <errors>
    <xsl:apply-templates select="log/entry[@level='ERROR']"/>
  </errors>
</xsl:template>

<xsl:template match="entry">
  <error time="{@timestamp}">
    <xsl:value-of select="message"/>
  </error>
</xsl:template>
```

### Streaming with xsl:iterate for Aggregation

```xml
<xsl:mode streamable="yes"/>

<xsl:template match="/">
  <xsl:source-document streamable="yes" href="sales.xml">
    <xsl:iterate select="sales/record">
      <xsl:param name="total" as="xs:decimal" select="0"/>
      <xsl:param name="count" as="xs:integer" select="0"/>
      <xsl:on-completion>
        <summary total="{$total}" count="{$count}" average="{$total div $count}"/>
      </xsl:on-completion>
      <xsl:next-iteration>
        <xsl:with-param name="total" select="$total + xs:decimal(@amount)"/>
        <xsl:with-param name="count" select="$count + 1"/>
      </xsl:next-iteration>
    </xsl:iterate>
  </xsl:source-document>
</xsl:template>
```

### Burst-Mode Streaming

```xml
<!-- Non-streaming stylesheet that streams one large document -->
<xsl:template match="/">
  <report>
    <xsl:source-document streamable="yes" href="huge-data.xml">
      <xsl:apply-templates select="data/record" mode="stream"/>
    </xsl:source-document>
  </report>
</xsl:template>

<xsl:mode name="stream" streamable="yes"/>

<xsl:template match="record" mode="stream">
  <row id="{@id}"><xsl:value-of select="name"/></row>
</xsl:template>
```

## See Also

- [Streaming](streaming.md)
- [Accumulators](accumulators.md)
- [xsl:iterate](../instructions/xsl-iterate.md)
- [xsl:fork](../instructions/xsl-fork.md)
- [xsl:source-document](../instructions/xsl-source-document.md)
- [Modes](modes.md)
