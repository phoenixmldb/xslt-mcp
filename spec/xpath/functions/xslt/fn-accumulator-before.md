---
name: fn:accumulator-before
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#function-accumulator-before
---

# fn:accumulator-before

Returns the accumulator value before processing the current node.

## Signature

```
fn:accumulator-before($name as xs:string) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$name` | `xs:string` | The name of the accumulator (as declared in `xsl:accumulator`). |

## Return Type

`item()*`

## Description

The `fn:accumulator-before` function returns the value of the named accumulator as it was before the current node was processed. Accumulators are declared using `xsl:accumulator` and maintain running state as the document is traversed in document order. The "before" value represents the state prior to any accumulator rules firing for the current node.

For the document node, the "before" value is the initial value of the accumulator. For other nodes, it is the value after processing all preceding nodes but before the current node's accumulator rule (if any) fires.

Accumulators provide a streaming-compatible alternative to variables for maintaining state during document traversal. They can be used in streaming mode because they process nodes in document order.

## Examples

```xpath
(: Given: <xsl:accumulator name="count" initial-value="0">
     <xsl:accumulator-rule match="item" select="$value + 1"/>
   </xsl:accumulator> :)

accumulator-before('count')
(: Returns the count of item elements processed before the current node :)
```

## Error Codes

- `XTDE3340` — No accumulator with the given name is declared.
- `XTDE3350` — The accumulator is not applicable to the current document.

## See Also

- [fn:accumulator-after](fn-accumulator-after.md)
