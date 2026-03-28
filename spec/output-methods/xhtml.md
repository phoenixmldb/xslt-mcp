---
name: xhtml
category: output-method
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#xhtml-output-method
---

# XHTML Output Method

Produces output conforming to XHTML rules — well-formed XML that is also compatible with HTML user agents.

## Serialization Parameters

Same as the XML output method, plus:

| Parameter | Default | Description |
|-----------|---------|-------------|
| `html-version` | `5` | XHTML version. |
| `encoding` | `UTF-8` | Character encoding. |
| `indent` | `no` | Whether to indent. |
| `include-content-type` | `yes` | Whether to add a `<meta>` content-type element. |
| `media-type` | `application/xhtml+xml` | MIME media type. |
| `doctype-public` | (none) | Public identifier. |
| `doctype-system` | (none) | System identifier. |

## Behavior

- Follows XML serialization rules (well-formed output).
- Void elements use self-closing syntax with a space before the slash (`<br />`).
- `<script>` and `<style>` elements are output with content, not as empty elements.
- Namespace `http://www.w3.org/1999/xhtml` is the default namespace.

## Examples

```xml
<xsl:output method="xhtml" html-version="5" encoding="UTF-8" indent="yes"/>
```

Output:
```xml
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>Page Title</title>
</head>
<body>
  <p>Hello World</p>
  <br />
</body>
</html>
```

## See Also

- [HTML Output Method](html.md)
- [XML Output Method](xml.md)
- [xsl:output](../instructions/xsl-output.md)
