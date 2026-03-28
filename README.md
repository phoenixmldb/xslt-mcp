# xslt-mcp

An MCP server for XSLT — spec reference lookup, stylesheet validation, and XSLT/XPath execution powered by the [PhoenixmlDb](https://github.com/phoenixmldb) engine.

## Install

### Option 1: Self-contained binary (no .NET required)

Download the latest release for your platform from [GitHub Releases](https://github.com/phoenixmldb/xslt-mcp/releases):

| Platform | Download |
|----------|----------|
| Linux x64 | `xslt-mcp-linux-x64` |
| Linux ARM64 | `xslt-mcp-linux-arm64` |
| macOS x64 | `xslt-mcp-osx-x64` |
| macOS ARM64 | `xslt-mcp-osx-arm64` |
| Windows x64 | `xslt-mcp-win-x64.exe` |

```bash
# Linux/macOS
chmod +x xslt-mcp-linux-x64
./xslt-mcp-linux-x64
```

### Option 2: .NET tool (requires .NET 10 SDK)

```bash
dotnet tool install -g xslt-mcp
```

## Configure

Add to your MCP client configuration:

**Claude Code** (`.mcp.json`):
```json
{
  "mcpServers": {
    "xslt": {
      "command": "xslt-mcp"
    }
  }
}
```

**Claude Desktop** (`claude_desktop_config.json`):
```json
{
  "mcpServers": {
    "xslt": {
      "command": "xslt-mcp",
      "args": []
    }
  }
}
```

## Tools

### Spec Reference
| Tool | Description |
|------|-------------|
| `xslt_lookup_instruction` | Look up any XSLT instruction — attributes, content model, examples |
| `xslt_lookup_function` | Look up XPath/XSLT functions — signature, parameters, return type |
| `xslt_lookup_output_method` | Look up output methods (xml, html, json, text, etc.) |
| `xslt_lookup_error_code` | Look up error codes with descriptions and fix suggestions |
| `xslt_search` | Full-text search across all spec entries |
| `xslt_list_instructions` | List all XSLT instructions |
| `xslt_list_functions` | List all functions by category |

### Execution
| Tool | Description |
|------|-------------|
| `xslt_transform` | Run an XSLT transformation and get the output |
| `xslt_validate` | Compile a stylesheet without executing — check for errors |
| `xpath_evaluate` | Evaluate an XPath expression against XML |
| `xslt_explain_error` | Explain an error code with causes and fixes |

### Utilities
| Tool | Description |
|------|-------------|
| `xml_validate_schema` | Validate XML against an XSD schema |
| `xml_format` | Pretty-print XML |

## How It Works

The MCP server is built from two things: **spec reference files** and the **PhoenixmlDb XSLT engine**.

### Spec Files

The `spec/` directory contains Markdown files with YAML frontmatter covering XSLT 3.0/4.0, XPath 3.1, and Functions & Operators. Each file documents one instruction, function, error code, or concept:

```
spec/
  instructions/     # 74 files — xsl:template, xsl:for-each-group, xsl:stream, ...
  xpath/
    functions/      # 160+ files — fn:tokenize, map:merge, math:sqrt, ...
    axes/           # 13 files — child, descendant, following-sibling, ...
  concepts/         # 15 files — streaming-rules, patterns, serialization, ...
  error-codes/      # 23 files — XTSE0010, XTDE0640, FODC0002, ...
  output-methods/   # 7 files — xml, html, json, text, xhtml, csv, adaptive
```

Every file has this structure:

```markdown
---
name: xsl:for-each-group
category: instruction
since: "2.0"
spec_url: https://www.w3.org/TR/xslt-30/#for-each-group
---

# xsl:for-each-group

Groups items in a sequence and processes each group.

## Attributes
...
```

At startup, the server loads all Markdown files, parses the YAML frontmatter, and builds an in-memory index. The lookup tools search this index by name, category, or full-text keywords. The content is returned directly to the AI agent as Markdown.

### Execution Engine

The execution tools (`xslt_transform`, `xslt_validate`, `xpath_evaluate`) delegate to the [PhoenixmlDb.Xslt](https://www.nuget.org/packages/PhoenixmlDb.Xslt) engine — a .NET implementation of XSLT 3.0/4.0. This means the MCP server can not only look up how a feature *should* work, but also run it and show you the actual output.

### Embedded Resources

When installed as a dotnet tool or self-contained binary, the spec files are embedded as assembly resources — no external files needed. For local development, you can override the spec path:

```bash
XSLT_SPEC_PATH=/path/to/spec xslt-mcp
# or
xslt-mcp --spec-path /path/to/spec
```

## Spec Coverage and Contributing

The spec reference files were AI-generated from the W3C XSLT 3.0, XPath 3.1, and Functions & Operators specifications. They are interpretive summaries derived from the published standards — useful as reference for AI agents, but not a verbatim reproduction of the spec text. Every entry includes a `spec_url` linking to the authoritative W3C source.

**We're actively filling gaps and improving accuracy.** The initial release was generated from an LLM's training data without systematically checking against the spec's table of contents, which means coverage skewed toward frequently-discussed features and missed advanced/niche areas. We've since audited against the full spec and filled the major gaps (streaming, merge, accumulators, 100+ functions), but there's always room to improve.

### Areas where we'd welcome help

- **Streaming rules** — The streamability categories (grounded, striding, climbing, etc.) are complex. Our `streaming-rules.md` covers the fundamentals but the full spec has nuances we may have simplified.
- **Output serialization parameters** — The interaction between `xsl:output`, `xsl:result-document`, and character maps across all output methods has many edge cases.
- **Schema-aware processing** — Type annotations, `xsl:import-schema`, and validation interactions are lightly covered.
- **Error code completeness** — The XSLT 3.0 spec defines 100+ error codes. We have 23. Each one matters when you're debugging.
- **XSLT 4.0 additions** — xsl:map, xsl:map-entry, and other 4.0 features need coverage as the spec stabilizes.

### How to contribute

1. Fork this repo
2. Add or edit files in `spec/` — follow the existing YAML frontmatter + Markdown format
3. Submit a PR

The only requirements are: the `name`, `category`, and `since` frontmatter fields must be present, and the content should be accurate to the W3C spec. If you're unsure about a detail, include a note and link to the relevant spec section — an incomplete-but-honest entry is better than a confident-but-wrong one.

## License

Apache-2.0
