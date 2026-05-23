---
name: xsl:package
category: example
since: "3.0"
spec_url: https://www.w3.org/TR/xslt-30/#packages
---

# Modular packaging with xsl:override

## Base package (base-templates.xsl)

```xml
<xsl:package name="http://example.com/base"
             version="3.0"
             package-version="1.0"
             xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <!-- Public template — callers can override this -->
  <xsl:template name="page-header" visibility="public">
    <header>
      <h1>Default Header</h1>
    </header>
  </xsl:template>

  <!-- Public function — callable from using packages -->
  <xsl:function name="ex:format-date" visibility="public"
                xmlns:ex="http://example.com/base">
    <xsl:param name="d" as="xs:date"
               xmlns:xs="http://www.w3.org/2001/XMLSchema"/>
    <xsl:value-of select="format-date($d, '[D] [MNn] [Y]')"/>
  </xsl:function>

  <!-- Abstract template — must be overridden by using package -->
  <xsl:template name="page-footer" visibility="abstract"/>

</xsl:package>
```

## Using package (main.xsl)

```xml
<xsl:stylesheet version="3.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:ex="http://example.com/base">

  <!-- Import the base package -->
  <xsl:use-package name="http://example.com/base" package-version="1.0">
    <!-- Override the public template -->
    <xsl:override>
      <xsl:template name="page-header">
        <header class="branded">
          <img src="logo.svg" alt="Logo"/>
        </header>
      </xsl:template>
    </xsl:override>
  </xsl:use-package>

  <!-- Provide the required abstract template -->
  <xsl:template name="page-footer">
    <footer>Copyright 2025</footer>
  </xsl:template>

  <xsl:template name="xsl:initial-template">
    <html>
      <body>
        <xsl:call-template name="page-header"/>
        <main><p>Content here</p></main>
        <xsl:call-template name="page-footer"/>
      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>
```

## What it does

`xsl:package` is the XSLT 3.0 unit of modular deployment. A package
declares named templates, functions, and variables with explicit
`visibility` (`public`, `final`, `private`, or `abstract`). A
`xsl:use-package` consumer can override non-`final` public components via
`xsl:override`. `abstract` visibility means the base package provides no
implementation — the using package **must** supply one.

## Common pitfalls

- Attempting to override a `final` component raises `XTSE3060`.
- The `package-version` attribute uses a structured version number — omit it
  from `xsl:use-package` to accept any version, or pin it for reproducible builds.
- Functions with `visibility="private"` are not callable from outside the
  package at all — compile-time error if referenced externally.
- Packages are separate compilation units; the processor must locate them
  using a package catalog or an implementation-defined resolution mechanism.
