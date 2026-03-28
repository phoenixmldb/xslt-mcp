---
name: Regular Expressions
category: concept
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#regex
---

# Regular Expressions

XSLT (via XPath) supports regular expressions for pattern matching, string replacement, and tokenization. The regex flavor used is defined by the XPath/XQuery specification and is based on XML Schema regular expressions, NOT on Perl-compatible regular expressions (PCRE). This distinction is critical -- developers familiar with regex in other languages will encounter significant differences in syntax and capabilities.

Regular expressions are used by the XPath functions `fn:matches()`, `fn:replace()`, `fn:tokenize()`, and by the XSLT instruction `xsl:analyze-string`. Understanding the specific regex flavor is essential for correct usage.

## XPath Regex Flavor

The XPath regex syntax is defined in the XPath Functions and Operators specification, which references the XML Schema regex syntax with extensions. Key characteristics:

- The regex is always applied against the entire string for `fn:matches()` unless anchored. There are no implicit anchors -- `fn:matches('abc', 'b')` returns `true` because the pattern is found within the string.
- For `fn:replace()` and `fn:tokenize()`, the regex must not match a zero-length string (to avoid infinite loops).
- Replacement strings in `fn:replace()` use `$1`, `$2`, etc. for back-references to captured groups, and `$0` for the entire match.

## Character Classes

Standard character class syntax is supported:

- `[abc]` -- Matches a, b, or c.
- `[a-z]` -- Matches any lowercase letter.
- `[^abc]` -- Matches any character except a, b, or c.
- `.` -- Matches any character except newline (unless dot-all flag is set).

### Character Class Escapes

XPath defines several shorthand character class escapes. Some are shared with other regex flavors; others are unique to XML Schema regex:

- `\d` -- Any digit (Unicode category Nd, not just `[0-9]`).
- `\D` -- Any non-digit.
- `\w` -- Any "word" character (letters, digits, combining marks). Broader than Perl's `\w`.
- `\W` -- Any non-word character.
- `\s` -- Whitespace: space, tab, newline, carriage return (`[ \t\n\r]`).
- `\S` -- Any non-whitespace.
- `\i` -- Any XML name-start character (letters, underscore, colon).
- `\I` -- Any character that is not an XML name-start character.
- `\c` -- Any XML name character (name-start characters plus digits, hyphen, period, combining characters).
- `\C` -- Any character that is not an XML name character.

### Unicode Category Escapes

The `\p{Category}` syntax matches characters in a Unicode category:

- `\p{L}` -- Any letter.
- `\p{Lu}` -- Uppercase letters.
- `\p{Ll}` -- Lowercase letters.
- `\p{N}` -- Any number.
- `\p{Nd}` -- Decimal digits.
- `\p{P}` -- Punctuation.
- `\p{S}` -- Symbols.
- `\p{IsBasicLatin}` -- Characters in the Basic Latin block.
- `\p{IsGreek}` -- Characters in the Greek block.
- `\P{Category}` -- Negation: any character NOT in the category.

## Quantifiers

Standard quantifiers are supported:

- `*` -- Zero or more.
- `+` -- One or more.
- `?` -- Zero or one.
- `{n}` -- Exactly n.
- `{n,}` -- At least n.
- `{n,m}` -- Between n and m.

Quantifiers are greedy by default. The reluctant (lazy) modifier `?` is supported: `*?`, `+?`, `??`, `{n,m}?`.

## Flags

The functions `fn:matches()`, `fn:replace()`, `fn:tokenize()`, and the `xsl:analyze-string` instruction accept a `flags` parameter:

- **`s`** (dot-all) -- Makes `.` match any character including newline. Without this flag, `.` does not match `\n`.
- **`m`** (multi-line) -- Makes `^` and `$` match the start and end of each line (not just the start and end of the string).
- **`i`** (case-insensitive) -- Performs case-insensitive matching. Unicode case folding is applied.
- **`x`** (extended) -- Whitespace in the regex is ignored (for readability). Use `\s` or `[ ]` to match actual whitespace. Comments from `#` to end of line are also ignored.
- **`q`** (literal, XPath 3.1) -- Treats the entire pattern as a literal string; no characters have special regex meaning. Useful for matching strings that contain regex metacharacters.

Flags can be combined: `flags="si"` enables both dot-all and case-insensitive matching.

## Differences from Perl/PCRE

Developers accustomed to PCRE or other regex flavors must be aware of these differences:

- **No lookahead or lookbehind**: `(?=...)`, `(?!...)`, `(?<=...)`, `(?<!...)` are not supported.
- **No backreferences in patterns**: `\1` inside the pattern itself is not supported. Backreferences are only available in `fn:replace()` replacement strings.
- **No possessive quantifiers**: `*+`, `++`, `?+` are not supported.
- **No atomic groups**: `(?>...)` is not supported.
- **No named groups**: `(?P<name>...)` or `(?<name>...)` are not supported.
- **No conditional patterns**: `(?(condition)yes|no)` is not supported.
- **`\b` is not word boundary**: `\b` is the backspace character (U+0008) in XML Schema regex, NOT a word boundary assertion. There is no word boundary assertion.
- **`\i` and `\c` are unique**: These XML name character classes do not exist in other flavors.
- **Character class subtraction**: `[a-z-[aeiou]]` (consonants) is supported. This syntax is not available in most other flavors.
- **Unicode-first**: `\d` matches all Unicode digits, not just ASCII 0-9. Similarly for `\w`.

## Functions That Use Regex

### fn:matches()

Tests whether a string matches a regex:

```xml
<xsl:if test="matches($input, '^\d{4}-\d{2}-\d{2}$')">
  <!-- Input looks like a date -->
</xsl:if>
```

### fn:replace()

Replaces matches of a regex in a string:

```xml
<!-- Replace multiple whitespace with single space -->
<xsl:value-of select="replace($text, '\s+', ' ')"/>

<!-- Swap first and last name -->
<xsl:value-of select="replace('John Smith', '(\w+)\s+(\w+)', '$2, $1')"/>
```

### fn:tokenize()

Splits a string using a regex as the delimiter:

```xml
<!-- Split on comma with optional whitespace -->
<xsl:for-each select="tokenize($csv, ',\s*')">
  <item><xsl:value-of select="."/></item>
</xsl:for-each>
```

### xsl:analyze-string

The most powerful regex instruction, allowing different processing for matched and non-matched portions:

```xml
<xsl:analyze-string select="$text" regex="\{\{(\w+)\}\}">
  <xsl:matching-substring>
    <placeholder name="{regex-group(1)}"/>
  </xsl:matching-substring>
  <xsl:non-matching-substring>
    <text><xsl:value-of select="."/></text>
  </xsl:non-matching-substring>
</xsl:analyze-string>
```

## Examples

```xml
<!-- Validate email-like pattern -->
<xsl:if test="matches(@email, '^[\w.+-]+@[\w.-]+\.\w{2,}$', 'i')">
  <valid-email/>
</xsl:if>

<!-- Extract digits from a string -->
<xsl:value-of select="replace('Order #12345-AB', '[^\d]', '')"/>
<!-- Result: 12345 -->

<!-- Character class subtraction: match consonants -->
<xsl:value-of select="replace('hello world', '[a-z-[aeiou]]', '*')"/>
<!-- Result: *e**o *o*** -->

<!-- Multi-line matching -->
<xsl:variable name="lines" select="matches($text, '^ERROR:', 'm')"/>

<!-- Tokenize with extended flag for readability -->
<xsl:variable name="parts" select="tokenize($record, '
    \s* [|] \s*   (: pipe-separated with optional whitespace :)
', 'x')"/>
```

## See Also

- [xsl:analyze-string](../instructions/xsl-analyze-string.md)
- [fn:matches](../functions/fn-matches.md)
- [fn:replace](../functions/fn-replace.md)
- [fn:tokenize](../functions/fn-tokenize.md)
