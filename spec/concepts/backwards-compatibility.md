---
name: Backwards Compatibility
category: concept
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#compatibility
---

# Backwards Compatibility

XSLT has evolved through four major versions (1.0, 2.0, 3.0, and the emerging 4.0), each adding significant capabilities while attempting to maintain compatibility with earlier versions. The `version` attribute on `xsl:stylesheet` and on individual instructions controls the compatibility mode, which affects how the processor handles features, errors, and type conversions. Understanding version compatibility is essential for maintaining legacy stylesheets, migrating between versions, and writing stylesheets that work across multiple processors.

## The version Attribute

The `version` attribute on `xsl:stylesheet` declares the XSLT version the stylesheet is written for:

```xml
<xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
```

This attribute affects processor behavior in two ways:

1. It determines which features are available and how they behave.
2. It activates backwards-compatible or forwards-compatible processing mode when the declared version does not match the processor's supported version.

Individual elements can also carry a `version` attribute to override the stylesheet-level version for a specific subtree. This allows mixing version-specific behavior within a single stylesheet.

## Backwards Compatible Processing Mode

When an XSLT 2.0 or 3.0 processor encounters a stylesheet with `version="1.0"`, it enters **backwards compatible processing mode** for the constructs within that scope. This mode emulates XSLT 1.0 behavior for certain constructs that changed between versions:

- **String-to-number coercion**: In XSLT 1.0, applying arithmetic to a string silently converts it to a number (or NaN). In XSLT 2.0+, this is a type error. Backwards compatible mode restores the silent conversion.

- **First-item extraction**: In XSLT 1.0, `xsl:value-of` and comparisons implicitly used the first node in a node-set. In XSLT 2.0+, comparisons use existential semantics (any match). Backwards compatible mode restores 1.0 behavior.

- **Boolean comparisons**: The comparison `$x = 'value'` in XSLT 1.0 converts the node-set to a string (first node) and compares. In XSLT 2.0, it checks if ANY node equals the value. Backwards compatible mode uses 1.0 semantics.

- **Error handling**: Some errors that are fatal in XSLT 2.0+ are treated as recoverable or produce fallback behavior in backwards compatible mode.

Backwards compatible mode is available when `[xsl:use-when]` or the `version` attribute indicates 1.0. The processor will report warnings for constructs that behave differently.

## Forwards Compatible Processing Mode

When a processor encounters a stylesheet version higher than it supports (e.g., an XSLT 2.0 processor encountering `version="3.0"`), it enters **forwards compatible processing mode**:

- Unknown elements in the XSLT namespace are not errors. The processor ignores them at compile time and raises an error only if they are actually evaluated at runtime.
- Unknown attributes on XSLT elements are ignored.
- `xsl:fallback` children of unrecognized instructions are evaluated as alternatives.

This mechanism allows stylesheets to use features from a newer version while providing fallbacks for older processors:

```xml
<xsl:try version="3.0">
  <xsl:sequence select="parse-xml($input)"/>
  <xsl:catch>
    <error>Failed to parse XML</error>
  </xsl:catch>
  <xsl:fallback>
    <!-- XSLT 2.0 processor will use this instead -->
    <xsl:sequence select="doc($input-uri)"/>
  </xsl:fallback>
</xsl:try>
```

## xsl:fallback

The `xsl:fallback` instruction provides alternative processing when an instruction is not recognized by the processor:

```xml
<xsl:evaluate xpath="$dynamic-expr">
  <xsl:fallback>
    <xsl:message>xsl:evaluate not supported; using static path</xsl:message>
    <xsl:value-of select="*"/>
  </xsl:fallback>
</xsl:evaluate>
```

`xsl:fallback` is only evaluated when its parent instruction is not recognized (in forwards compatible mode). If the parent instruction is recognized, `xsl:fallback` is ignored entirely.

## Differences Between XSLT Versions

### XSLT 1.0 (1999)

The original version. Key characteristics:
- Result tree fragment type (distinct from node-set).
- No schema type awareness.
- Limited data types (string, number, boolean, node-set).
- No regular expressions.
- No grouping instruction.
- No user-defined functions.
- Single output document only.

### XSLT 2.0 (2007)

Major upgrade introducing:
- XPath 2.0 with XSD type system (xs:integer, xs:date, etc.).
- Sequence type declarations (`as` attribute).
- `xsl:for-each-group` for grouping.
- `xsl:function` for user-defined functions.
- `xsl:result-document` for multiple output documents.
- Regular expression support (fn:matches, fn:replace, xsl:analyze-string).
- `xsl:next-match` for template rule chaining.
- Schema-aware processing (optional).
- Temporary trees (replacing result tree fragments) can be navigated.

### XSLT 3.0 (2017)

Major upgrade introducing:
- Streaming (`xsl:mode streamable="yes"`, `xsl:source-document`, `xsl:iterate`, `xsl:fork`).
- Maps and arrays as first-class data types.
- Higher-order functions (function items, fn:for-each, fn:filter, fn:fold-left).
- JSON support (fn:json-doc, fn:parse-json, json serialization method).
- `xsl:try`/`xsl:catch` for error handling.
- `xsl:evaluate` for dynamic XPath evaluation.
- `xsl:accumulator` for streaming aggregation.
- Packages (`xsl:package`, `xsl:use-package`) for modularization.
- `xsl:assert` for stylesheet assertions.
- `xsl:where-populated` for conditional output.
- Shadow attributes (underscore-prefixed attributes evaluated as AVTs).
- Text value templates (`expand-text="yes"`).

### XSLT 4.0 (Draft)

Emerging specification building on 3.0:
- XPath 4.0 with enhanced expressions.
- `xsl:for-member` for iterating over array members.
- Record types for structured data.
- Enhanced map and array operations.
- `xsl:switch` as a streamlined alternative to xsl:choose.
- Improved CSV and text parsing support.
- `xsl:item-type` for declaring custom item types.
- Enhanced error handling.
- Various ergonomic improvements.

## Migration Considerations

### 1.0 to 2.0/3.0

The most impactful migration. Common issues:
- **Namespace declarations**: XSLT 2.0+ requires the XPath functions namespace for non-built-in functions.
- **Type errors**: Implicit string-to-number conversions that worked in 1.0 become type errors. Fix by adding explicit casts: `xs:decimal(@price)`.
- **Comparison semantics**: `$nodes = 'value'` changes from first-item to existential semantics. Review all comparisons.
- **Result tree fragments**: In 1.0, variables containing generated XML could not be navigated. In 2.0+, they can. This is generally a non-breaking improvement.
- **xsl:value-of separator**: In 2.0+, `xsl:value-of` with a sequence produces space-separated output by default (1.0 took first item only).

### 2.0 to 3.0

Generally non-breaking. Key changes:
- `xsl:source-document` replaces the deprecated `xsl:stream`.
- Packages change visibility and encapsulation semantics.
- New reserved function names (map:*, array:*) might conflict with user functions.
- Text value templates (`{$var}` in text) require `expand-text="yes"` -- not enabled by default.

### Strategy

The recommended migration strategy is:
1. Set `version` to the target version.
2. Compile and fix all errors reported.
3. Test thoroughly -- semantic differences (especially in comparisons and type handling) may not produce compile errors.
4. Use `version="1.0"` on specific elements as a temporary bridge for constructs that need 1.0 behavior.

## Examples

```xml
<!-- Mixed-version stylesheet -->
<xsl:stylesheet version="3.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <!-- Modern XSLT 3.0 template -->
  <xsl:template match="/">
    <xsl:try>
      <xsl:apply-templates/>
      <xsl:catch>
        <error><xsl:value-of select="$err:description"/></error>
      </xsl:catch>
    </xsl:try>
  </xsl:template>

  <!-- Legacy template with 1.0 behavior -->
  <xsl:template match="price" version="1.0">
    <!-- In 1.0 mode, string-to-number coercion is silent -->
    <xsl:value-of select=". * 1.1"/>
  </xsl:template>

  <!-- Feature detection with fallback -->
  <xsl:template match="data">
    <xsl:evaluate xpath="@expression">
      <xsl:fallback>
        <!-- Processor doesn't support xsl:evaluate -->
        <xsl:value-of select="."/>
      </xsl:fallback>
    </xsl:evaluate>
  </xsl:template>

</xsl:stylesheet>
```

## See Also

- [xsl:fallback](../instructions/xsl-fallback.md)
- [Packages](packages.md)
