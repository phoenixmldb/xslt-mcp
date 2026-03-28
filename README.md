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

## License

Apache-2.0
