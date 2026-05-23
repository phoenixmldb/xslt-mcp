using System.ComponentModel;
using ModelContextProtocol.Server;

namespace XsltMcpServer;

[McpServerPromptType]
public static class PromptDefinitions
{
    [McpServerPrompt(Name = "xslt-write-streaming-transform"), Description(
        "Walk through writing a streaming XSLT 3.0 transform with verification at each step.")]
    public static string WriteStreamingTransform(
        [Description("Shape of the input XML, e.g. '<items><item id=...>...</item></items>'")]
        string inputShape,
        [Description("Desired output shape, e.g. '<report><stat name=... value=.../></report>'")]
        string outputShape) =>
        $$"""
        Help me write a streaming XSLT 3.0 transform.

        Input shape:
        {{inputShape}}

        Desired output shape:
        {{outputShape}}

        Constraints:
        - Use <xsl:mode streamable="yes"/>
        - After writing each template, call xslt_explain_streamability to verify it's still streamable
        - For each non-streamable suggestion, propose a streaming alternative (xsl:iterate, xsl:accumulator, xsl:for-each-group)
        - Prefer xsl:iterate over xsl:for-each for stateful traversal
        - Use xslt_find_examples for any pattern you're unsure about
        - End with xslt_transform using a small synthetic input to verify the output

        Start by sketching the mode declaration and template skeleton.
        """;

    [McpServerPrompt(Name = "xslt-migrate-2-to-3"), Description(
        "Migrate an XSLT 2.0 stylesheet to XSLT 3.0, adopting maps/arrays, packages, accumulators where useful.")]
    public static string MigrateXslt2to3(
        [Description("The XSLT 2.0 stylesheet to migrate (complete XML)")] string stylesheet) =>
        $$"""
        I want to migrate this XSLT 2.0 stylesheet to XSLT 3.0. Suggest concrete improvements where 3.0 idioms simplify the code:

        ```xml
        {{stylesheet}}
        ```

        For each suggested change:
        - Cite the XSLT 3.0 feature it uses (xsl:iterate, xsl:accumulator, xsl:try, maps, arrays, etc.)
        - Show the before/after
        - Verify the change with xslt_validate

        Be conservative — only suggest changes that clearly improve readability or unlock streaming. Don't churn for the sake of using new features.

        Use xslt_compare_versions to confirm any feature is in 3.0 (not 4.0 draft).
        """;

    [McpServerPrompt(Name = "xslt-debug-transform"), Description(
        "Debug an XSLT transform that's producing wrong output or erroring.")]
    public static string DebugTransform(
        [Description("The stylesheet (complete XML)")] string stylesheet,
        [Description("The source XML input")] string sourceXml,
        [Description("The error code or wrong-output description")] string problem) =>
        $$"""
        Help me debug this XSLT transform.

        Stylesheet:
        ```xml
        {{stylesheet}}
        ```

        Source XML:
        ```xml
        {{sourceXml}}
        ```

        Problem: {{problem}}

        Steps:
        1. Run xslt_transform to capture the actual error or output
        2. If there's an error code, run xslt_suggest_fix with it
        3. If the output is wrong, use xpath_evaluate to inspect parts of the source
        4. Propose a fix and verify with xslt_transform using a minimal example

        Don't guess — verify each hypothesis with a tool call.
        """;

    [McpServerPrompt(Name = "xslt-write-test"), Description(
        "Author an xslt_transform invocation given a stylesheet and an expected output description.")]
    public static string WriteTest(
        [Description("The stylesheet to test")] string stylesheet,
        [Description("Description of the expected behavior")] string expectedBehavior) =>
        $$"""
        Author a test for this stylesheet:

        ```xml
        {{stylesheet}}
        ```

        Expected behavior: {{expectedBehavior}}

        Generate:
        1. A minimal source XML that exercises the behavior
        2. The exact expected output XML (canonical, no whitespace assumptions)
        3. An xslt_transform call ready to run to verify the output

        After the test passes, suggest 2 edge cases worth additional tests.
        """;
}
