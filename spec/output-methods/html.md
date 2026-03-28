---
name: html
category: output-method
since: "1.0"
spec_url: https://www.w3.org/TR/xslt-30/#html-output-method
---

# HTML Output Method

Produces output suitable for HTML user agents. Follows HTML serialization rules rather than XML rules.

## Serialization Parameters

| Parameter | Default | Description |
|-----------|---------|-------------|
| `html-version` | `5` (Since 3.0) | HTML version. |
| `version` | (implementation-defined) | Alternative way to specify the version. |
| `encoding` | `UTF-8` | Character encoding. |
| `indent` | `yes` | Whether to add whitespace indentation. |
| `doctype-public` | (none) | Public identifier for the DOCTYPE. |
| `doctype-system` | (none) | System identifier for the DOCTYPE. |
| `media-type` | `text/html` | MIME media type. |
| `omit-xml-declaration` | `yes` | Not applicable but accepted. |
| `suppress-indentation` | (none) | Elements not to indent. |
| `include-content-type` | `yes` | Whether to add a `<meta>` content-type element. |

## Behavior

- Void elements (`br`, `hr`, `img`, `input`, `meta`, `link`, etc.) are output without closing tags.
- Boolean attributes (`checked`, `disabled`, `selected`, etc.) are output minimized.
- `<script>` and `<style>` content is not escaped.
- Character references use `&#NNN;` form rather than `&#xNNN;` where possible.
- The `<` character in attribute values need not be escaped.
- Recognized URI attributes are escaped according to RFC 3986.

## Examples

```xml
<xsl:output method="html" html-version="5" encoding="UTF-8" indent="yes"/>
```

Output:
```html
<!DOCTYPE html>
<html>
<head>
  <meta charset="UTF-8">
  <title>Page Title</title>
</head>
<body>
  <p>Hello World</p>
  <br>
  <input type="text" disabled>
</body>
</html>
```

## See Also

- [xsl:output](../instructions/xsl-output.md)
- [XHTML Output Method](xhtml.md)
- [XML Output Method](xml.md)
