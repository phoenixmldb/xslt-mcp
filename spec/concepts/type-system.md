---
name: Type System
category: concept
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#types
---

# Type System

XSLT's type system is built on the XML Schema Definition (XSD) type hierarchy and the XPath/XQuery Data Model (XDM). Starting with XSLT 2.0, the language has a rich type system that supports type declarations on variables, parameters, function signatures, and template outputs. XSLT 3.0 further refines this with support for maps, arrays, and function items as first-class types.

The type system serves two purposes: enabling static type checking (where a processor can catch type errors at compile time) and controlling runtime behavior (implicit conversions, function dispatch, and serialization). Even when a processor does not perform full static type checking, sequence type declarations serve as runtime assertions and documentation.

## Built-in Atomic Types

XSLT uses the XSD built-in types as its atomic type inventory. The most commonly used types are:

- **xs:string** -- A sequence of Unicode characters.
- **xs:integer** -- An arbitrary-precision integer. Subtypes include xs:int, xs:long, xs:short, xs:positiveInteger, xs:nonNegativeInteger, etc.
- **xs:decimal** -- An arbitrary-precision decimal number. xs:integer is a subtype of xs:decimal.
- **xs:double** and **xs:float** -- IEEE 754 floating-point numbers. Used for scientific computation and when NaN/Infinity are needed.
- **xs:boolean** -- true or false.
- **xs:date**, **xs:dateTime**, **xs:time** -- Date and time values with optional timezone.
- **xs:duration**, **xs:yearMonthDuration**, **xs:dayTimeDuration** -- Duration values.
- **xs:anyURI** -- A URI reference.
- **xs:QName** -- A namespace-qualified name.
- **xs:base64Binary**, **xs:hexBinary** -- Binary data.
- **xs:untypedAtomic** -- The type of atomic values extracted from untyped (non-schema-validated) nodes.

## Sequence Types

Sequence types describe the type and cardinality of XDM values. They are used in the `as` attribute on xsl:variable, xsl:param, xsl:function, xsl:template, and other declarations. The syntax combines an item type with an occurrence indicator:

- `xs:integer` -- Exactly one integer.
- `xs:integer?` -- Zero or one integer.
- `xs:integer*` -- Zero or more integers.
- `xs:integer+` -- One or more integers.
- `item()` -- Any single item (node or atomic value).
- `item()*` -- Any sequence of items.
- `node()` -- Any single node.
- `element()` -- Any element node.
- `element(name)` -- An element with a specific name.
- `element(*, xs:integer)` -- An element with a specific type annotation.
- `attribute(name, xs:string)` -- A typed attribute.
- `document-node(element(root))` -- A document containing a specific document element.
- `map(xs:string, item()*)` -- A map with string keys and arbitrary values (XSLT 3.0).
- `array(xs:integer)` -- An array of integers (XSLT 3.0).
- `function(xs:string) as xs:boolean` -- A function item type (XSLT 3.0).

## Type Annotations on Nodes

When an XML document is validated against a schema, element and attribute nodes carry **type annotations**. An element validated as `xs:date` will have its typed value as an xs:date instance rather than a plain string. This affects how comparisons, arithmetic, and sorting work.

In **basic XSLT processing** (without schema awareness), all element nodes have the type annotation `xs:untyped` and all attribute nodes have `xs:untypedAtomic`. Atomization of an untyped node produces an `xs:untypedAtomic` value, which is then implicitly cast as needed by operators and functions.

In **schema-aware processing**, the processor validates input documents and stylesheet-created elements against imported schemas. Type annotations are preserved and enforced. Schema-aware processing requires importing schemas with `xsl:import-schema` and is an optional conformance level -- not all processors support it.

## Implicit and Explicit Type Conversions

XSLT defines several implicit conversion rules:

- **Atomization**: When a node is used where an atomic value is expected, the typed value of the node is extracted. For untyped nodes, this produces xs:untypedAtomic.
- **Untypedatomic promotion**: xs:untypedAtomic values are implicitly cast to the required type in many contexts. In arithmetic, untypedAtomic is promoted to xs:double. In comparisons, the promotion depends on the type of the other operand.
- **Numeric promotion**: xs:integer can be promoted to xs:decimal, xs:decimal to xs:float, and xs:float to xs:double, as needed by operators.
- **URI promotion**: xs:anyURI can be promoted to xs:string.

Explicit conversions use constructor functions (`xs:integer('42')`) or the `cast as` / `castable as` expressions (`$x cast as xs:date`).

## Function Signatures and Type Checking

Function declarations in XSLT 3.0 include full type signatures:

```xml
<xsl:function name="my:discount" as="xs:decimal">
  <xsl:param name="price" as="xs:decimal"/>
  <xsl:param name="rate" as="xs:decimal"/>
  <xsl:sequence select="$price * $rate"/>
</xsl:function>
```

The `as` attribute on xsl:param declares the expected argument type, and the `as` attribute on xsl:function declares the return type. When the function is called, the processor checks that the arguments match the declared types (after applying implicit conversions) and that the return value matches the declared return type.

Type checking may be static (at compile time) or dynamic (at runtime), depending on the processor. A processor that performs static type checking can report type errors before execution. However, static type checking is optional in XSLT 3.0 and most processors rely on dynamic checking.

## Maps, Arrays, and Function Types (XSLT 3.0)

XSLT 3.0 introduces three new item types that extend the XDM:

**Maps** (`map(K, V)`) are collections of key-value pairs. Keys must be atomic values. Maps are immutable -- operations like `map:put()` return new maps. Maps are used extensively for JSON processing and for passing structured data.

**Arrays** (`array(T)`) are ordered sequences of values. Unlike XDM sequences, arrays can be nested and can contain empty entries. Arrays are used for JSON array representation.

**Function items** (`function(A) as R`) are first-class values representing functions. Higher-order functions like `fn:for-each()`, `fn:filter()`, and `fn:fold-left()` accept function items as arguments.

## Examples

```xml
<!-- Variable with sequence type -->
<xsl:variable name="count" as="xs:integer" select="count(//item)"/>

<!-- Parameter with optional type -->
<xsl:param name="date" as="xs:date?" select="()"/>

<!-- Template with typed output -->
<xsl:template match="price" as="xs:decimal">
  <xsl:sequence select="xs:decimal(.) * 1.1"/>
</xsl:template>

<!-- Map type -->
<xsl:variable name="config" as="map(xs:string, xs:string)"
              select="map { 'mode': 'production', 'debug': 'false' }"/>

<!-- Function item type -->
<xsl:variable name="sorter" as="function(item()*) as item()*"
              select="function($seq) { sort($seq) }"/>
```

## See Also

- [xsl:function](../instructions/xsl-function.md)
- [xsl:variable](../instructions/xsl-variable.md)
- [xsl:import-schema](../instructions/xsl-import-schema.md)
