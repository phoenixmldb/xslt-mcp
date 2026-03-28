---
name: Keys and Indexes
category: concept
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#keys
---

# Keys and Indexes

Keys provide an efficient mechanism for looking up nodes by value, analogous to database indexes. The `xsl:key` declaration defines a named index over a set of nodes, and the `fn:key()` function retrieves nodes from that index. Keys are one of the most important performance tools in XSLT -- they transform O(n) linear scans into O(1) hash lookups.

Keys are especially valuable when performing cross-referencing between elements, joining data from different parts of a document, or looking up nodes by identifier. Any pattern that involves iterating over a node set and testing a value against another node set is a candidate for optimization with keys.

## Declaring Keys

A key is declared at the top level of the stylesheet using `xsl:key`:

```xml
<xsl:key name="person-by-id" match="person" use="@id"/>
```

This declares a key named `person-by-id` that indexes `person` elements by their `@id` attribute. The components are:

- **name** -- The name of the key (a QName), used to reference it in `fn:key()` calls.
- **match** -- A pattern identifying which nodes are indexed. Any node matching this pattern is included in the key.
- **use** -- An XPath expression evaluated for each matched node to determine the key value(s). If the expression returns multiple values, the node is indexed under each value.

The `use` expression can be any XPath expression. Common patterns:

```xml
<!-- Index by attribute -->
<xsl:key name="emp-by-dept" match="employee" use="@department"/>

<!-- Index by child element content -->
<xsl:key name="book-by-author" match="book" use="author"/>

<!-- Index by computed value -->
<xsl:key name="item-by-code" match="item" use="concat(@category, '-', @id)"/>

<!-- Index by multiple values (node indexed under each) -->
<xsl:key name="person-by-name" match="person" use="(first-name, last-name)"/>
```

## Using fn:key()

The `key()` function retrieves nodes from a declared key:

```xml
<!-- Get the person with id 'P001' -->
<xsl:value-of select="key('person-by-id', 'P001')/name"/>

<!-- Get all employees in the Sales department -->
<xsl:for-each select="key('emp-by-dept', 'Sales')">
  <xsl:value-of select="name"/>
</xsl:for-each>
```

The function signature is `key(key-name, value, top?)`:

- **key-name** -- String: the name of the key.
- **value** -- The lookup value. Can be a single value or a sequence. When a sequence is provided, nodes matching any value in the sequence are returned.
- **top** -- Optional: the document node to search. Defaults to the document containing the context node. This parameter is essential when working with multiple documents.

## Composite Keys

XSLT 3.0 supports **composite keys** where the `use` attribute specifies a `map` or the `composite` attribute is set to `yes`:

```xml
<xsl:key name="cell" match="cell" use="@row, @col" composite="yes"/>

<!-- Look up with composite value -->
<xsl:value-of select="key('cell', ('R1', 'C3'))"/>
```

With `composite="yes"`, the key value is a tuple rather than individual values. Without it, `use="@row, @col"` would index the node under both `@row` and `@col` independently (which is usually not what you want for a composite lookup).

## Keys Across Documents

By default, `key()` searches the document containing the context node. To search a different document, pass it as the third argument:

```xml
<xsl:variable name="lookup-doc" select="doc('lookup-table.xml')"/>

<!-- Look up in the external document -->
<xsl:value-of select="key('code-desc', @code, $lookup-doc)"/>
```

This is the standard pattern for joining data between documents. The key index is built lazily for each document -- the first call to `key()` with a given document triggers index construction for that document.

## Performance Implications

Keys provide dramatic performance improvements for lookup-heavy transformations:

**Without keys** (O(n*m) complexity):
```xml
<!-- For each order, find the customer -- scans all customers for each order -->
<xsl:template match="order">
  <xsl:variable name="cust" select="//customer[@id = current()/@customer-id]"/>
  <order customer-name="{$cust/name}"/>
</xsl:template>
```

**With keys** (O(n) complexity):
```xml
<xsl:key name="cust-by-id" match="customer" use="@id"/>

<xsl:template match="order">
  <order customer-name="{key('cust-by-id', @customer-id)/name}"/>
</xsl:template>
```

The key-based version builds the index once (O(m) to build) and then performs O(1) lookups for each of n orders, giving O(n+m) total instead of O(n*m).

Keys are built lazily by most processors -- the index is constructed the first time `key()` is called for a given key name and document. Subsequent calls use the cached index. This means the first call may be slower, but all subsequent calls are fast.

## Keys vs. Predicates

A common question is when to use keys versus predicate-based lookups. Guidelines:

- **Use keys** when the same lookup is performed multiple times with different values. The index is built once and reused.
- **Use predicates** for one-off lookups where building an index would be wasted effort.
- **Use keys** when joining two large node sets (e.g., orders to customers).
- **Use predicates** when the lookup set is small or when the predicate involves complex conditions beyond simple value equality.

Most processors optimize simple predicates like `[@id = $value]` into implicit key lookups, but this optimization is not guaranteed. Explicit keys are portable and reliable.

## Examples

```xml
<!-- Cross-reference: departments and employees -->
<xsl:key name="employees-in-dept" match="employee" use="@dept-id"/>

<xsl:template match="department">
  <div class="department">
    <h2><xsl:value-of select="name"/></h2>
    <ul>
      <xsl:for-each select="key('employees-in-dept', @id)">
        <li><xsl:value-of select="name"/></li>
      </xsl:for-each>
    </ul>
  </div>
</xsl:template>

<!-- Multi-document join -->
<xsl:key name="price-by-sku" match="product" use="@sku"/>

<xsl:variable name="price-list" select="doc('prices.xml')"/>

<xsl:template match="line-item">
  <xsl:variable name="price" select="key('price-by-sku', @sku, $price-list)/@price"/>
  <item sku="{@sku}" qty="{@qty}" unit-price="{$price}" total="{@qty * $price}"/>
</xsl:template>

<!-- Composite key for grid lookup -->
<xsl:key name="grid-cell" match="cell" use="@row, @col" composite="yes"/>

<xsl:template match="reference">
  <xsl:value-of select="key('grid-cell', (@row, @col))/value"/>
</xsl:template>
```

## See Also

- [xsl:key](../instructions/xsl-key.md)
- [Template Rules](template-rules.md)
