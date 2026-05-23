# xslt-mcp release notes

## 1.4.0 — 2026-05-23

Major release combining four shippable phases of work. Consumers upgrading from 1.0.1 will see all of these features at once.

### Structured execution returns (was Phase A)
- All execution tools now return JSON `TransformResult` envelopes instead of plain strings.
- Shape: `{ ok, value?, outputMethod?, elapsedMs?, errors? }` with errors carrying `{ code, message, line, column, sourceSnippet }`.
- Runtime exception handling narrowed to `XsltException`, `XQueryParseException`, `XmlException` — no more silently swallowed OOM/cancellation.
- Affected tools: `xslt_transform`, `xslt_validate`, `xpath_evaluate`.
- Fix: invalid XPath in `xpath_evaluate` previously leaked `XQueryParseException` because the engine uses the XQuery parser internally; now produces a structured error.

### Compile handles, parameters, multi-input (was Phase B)
- New `xslt_compile` returns a SHA256-keyed handle for a compiled stylesheet.
- New `xslt_apply` applies a handle to source XML, skipping re-compilation. Each Apply call is serialized per-handle via a `SemaphoreSlim` to prevent racing on cached transformer state.
- `xslt_transform` and `xslt_apply` accept a JSON `parameters` arg to bind `xsl:param` declarations: strings as `xs:string`, numbers as `xs:double`, booleans as `xs:boolean`.
- `xslt_transform` and `xslt_apply` accept a JSON `documents` arg mapping URIs → XML so `doc()`/`document()` calls resolve to in-memory XML without filesystem access. Both relative and absolute URIs work — the engine sets a synthetic base URI (`http://xslt-mcp.internal/`) at load time.
- Error codes: `XMCP0001` for unknown handle, `XMCP0002` for invalid parameters JSON, `XMCP0003` for invalid documents JSON.

### Spec-aware tooling (was Phase C)
- `xslt_explain_streamability` — analyzes each `xsl:mode` and `xsl:template` and reports streamability with reasons. Uses the engine's XTSE3430 detection (conservative — does not implement full XSLT 3.0 §19 posture/sweep analysis); false negatives are possible. Use as early warning, not final validation.
- `xslt_compare_versions` — for any instruction or function, returns when the spec introduced it.
- `xslt_find_examples` — returns curated working examples by topic. Ships with 8 hand-authored examples: streaming-mode, xsl:merge, xsl:fork, xsl:accumulator, xsl:iterate, xsl:package, fn:transform, xsl:evaluate.
- `xslt_suggest_fix` — given an error code, returns a spec-grounded actionable suggestion. Top-10 rules cover XTSE0010, XTSE0090, XTSE0265, XTSE0530, XTSE0610, XTSE3430, XTDE0030, XTDE0040, XTDE1280, XTTE0505.
- `xslt_test` — assertion runner. Apply a stylesheet to source and compare the output to an expected XML document via `XDocument.DeepEquals` (canonical, whitespace-insensitive); returns pass/fail with a diff.

### MCP discoverability (was Phase D)
- `server_capabilities` — reports engine type and version, spec coverage stats, feature flags, and the complete tool list. Call once at session start to know what you can rely on. Verified at build time to match the assembly's registered tools.
- 4 MCP prompts: `xslt-write-streaming-transform`, `xslt-migrate-2-to-3`, `xslt-debug-transform`, `xslt-write-test` — surfaced as slash commands in MCP clients.
- MCP resources: browse the spec corpus via `xslt://index` and `xslt://instructions/{name}` URIs without explicit tool calls.

### Notes / known limitations
- `xslt_explain_streamability` does N+1 stylesheet compiles per call (one full load + one per template). For a 50-template stylesheet expect a multi-second response.
- `xslt_compile`'s handle cache is process-lifetime with no eviction. Acceptable for the single-user MCP server model.

## 1.0.1 — earlier

Bumps PhoenixmlDb.Xslt 1.1.0.10 → 1.3.21. Picks up 20+ versions of engine fixes including streaming xsl:merge runtime, streaming xsl:fork, full source-location coverage for LSP, Phase 2.5 perf, Martin Honnen bug fixes (fn:transform raw delivery / cross-store nodes / source-location URI / streamable identity / load-xquery-module HTTPS), and the JSON serializer conformance work picked up via the XQuery dependency. No MCP API changes.
