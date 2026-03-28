---
name: fn:accumulator-after
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#function-accumulator-after
---

# fn:accumulator-after

Returns the accumulator value after processing the current node.

## Signature

```
fn:accumulator-after($name as xs:string) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$name` | `xs:string` | The name of the accumulator. |

## Return Type

`item()*`

## Description

The `fn:accumulator-after` function returns the value of the named accumulator after the current node has been processed. If the current node matches an accumulator rule, the "after" value reflects the updated state. If the current node does not match, the "after" value equals the "before" value.

For the document node, calling `accumulator-after` returns the final value of the accumulator after the entire document has been traversed. This is particularly useful for getting totals, final counts, or summary values.

Accumulators provide a clean, declarative way to compute running aggregates during streaming or non-streaming transformations.

## Examples

```xpath
(: Given: <xsl:accumulator name="total" initial-value="0">
     <xsl:accumulator-rule match="price" select="$value + number(.)"/>
   </xsl:accumulator> :)

accumulator-after('total')
(: Returns the running total including the current price :)

(: On the document node, gets the final total: :)
accumulator-after('total')
```

## Error Codes

- `XTDE3340` — No accumulator with the given name is declared.
- `XTDE3350` — The accumulator is not applicable to the current document.

## See Also

- [fn:accumulator-before](fn-accumulator-before.md)
