---
name: Template Rules
category: concept
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#rules
---

# Template Rules

Template rules are the core mechanism of XSLT processing. A template rule consists of a pattern (the `match` attribute) and a template body (the sequence constructor). When `xsl:apply-templates` is invoked, the processor selects the best-matching template rule for each node.

## How Template Matching Works

1. The processor evaluates `xsl:apply-templates` for a set of nodes.
2. For each node, the processor finds all template rules whose `match` pattern matches the node.
3. Among matching rules, the one with the highest **import precedence** is selected.
4. Among rules with the same import precedence, the one with the highest **priority** is selected.
5. If there is still a tie, the processor may report a warning and select the rule that appears last in document order.

## Default Priority

When no explicit `priority` is specified, the default priority is determined by the specificity of the match pattern:

| Pattern Type | Default Priority |
|-------------|-----------------|
| `node()`, `*`, `@*`, `text()` etc. | -0.5 |
| `ns:*`, `@ns:*` | -0.25 |
| `element-name`, `@attr-name` | 0 |
| Patterns with predicates or path steps | 0.5 |

## Built-in Template Rules

XSLT defines built-in template rules that apply when no user-defined template matches:

- **Document and element nodes**: Apply templates to children (`<xsl:apply-templates/>`)
- **Text and attribute nodes**: Copy the string value (`<xsl:value-of select="."/>`)
- **Comment and processing instruction nodes**: Do nothing (produce no output)

## Conflict Resolution

When multiple template rules match with the same priority and import precedence, this is an **ambiguity**. XSLT 3.0 processors must either report a warning or raise an error (depending on the `on-multiple-match` property of the mode).

## Examples

```xml
<!-- Higher specificity = higher default priority -->
<xsl:template match="*">...</xsl:template>              <!-- priority -0.5 -->
<xsl:template match="item">...</xsl:template>            <!-- priority 0 -->
<xsl:template match="item[@type='special']">...</xsl:template>  <!-- priority 0.5 -->

<!-- Explicit priority override -->
<xsl:template match="item" priority="10">...</xsl:template>
```

## See Also

- [xsl:template](../instructions/xsl-template.md)
- [xsl:apply-templates](../instructions/xsl-apply-templates.md)
- [Modes](modes.md)
