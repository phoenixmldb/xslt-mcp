---
name: fn:apply
category: function
since: "3.0"
spec_url: https://www.w3.org/TR/xpath-functions-31/#func-apply
---

# fn:apply

Applies a function to arguments supplied as an array.

## Signature

```
fn:apply($function as function(*), $array as array(*)) as item()*
```

## Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `$function` | `function(*)` | The function to apply. |
| `$array` | `array(*)` | An array whose members are the arguments to the function. |

## Return Type

`item()*`

## Description

The `fn:apply` function invokes a function item with arguments provided as an array. The number of members in the array must match the arity of the function. This enables dynamic function invocation where the number and types of arguments are not known statically.

The function `$function` is called with the members of `$array` as its arguments: `$array?1` as the first argument, `$array?2` as the second, and so on. This is analogous to the spread operator or `apply` function in other programming languages.

This function is essential for implementing generic higher-order patterns where functions of varying arities need to be called dynamically.

## Examples

```xpath
apply(concat#3, ['a', 'b', 'c'])
(: Returns "abc" :)

apply(function($x, $y) { $x + $y }, [3, 4])
(: Returns 7 :)

let $f := if ($mode = 'add') then function($a, $b) { $a + $b }
          else function($a, $b) { $a * $b }
return apply($f, [5, 3])
(: Returns 8 or 15 depending on $mode :)
```

## Error Codes

- `FOAP0001` — The arity of the function does not match the size of the array.
- `XPTY0004` — Type error in the function call.

## See Also

- [fn:fold-left](fn-fold-left.md)
- [fn:for-each](../sequence/fn-for-each.md)
