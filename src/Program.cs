using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using XsltMcpServer;

// Report the server version AND the engine it bundles. A version identifies the package, not
// what it carries: this server sat on PhoenixmlDb.Xslt 1.3.21 for three months while the engine
// shipped up to 1.6.12, and nothing anywhere said so. The same gap once had Martin Honnen
// inferring a stale engine from a repro that kept failing against a version that supposedly
// contained the fix. Printing both is what makes that visible without forcing the two packages
// into lockstep, which would mean releasing this server on every engine patch.
static string VersionOf(System.Reflection.Assembly asm)
{
    var v = asm.GetCustomAttributes(typeof(System.Reflection.AssemblyInformationalVersionAttribute), false)
               .OfType<System.Reflection.AssemblyInformationalVersionAttribute>()
               .FirstOrDefault()?.InformationalVersion
            ?? asm.GetName().Version?.ToString(3)
            ?? "unknown";
    var plus = v.IndexOf('+', StringComparison.Ordinal);
    return plus >= 0 ? v[..plus] : v;
}

var serverVersion = VersionOf(System.Reflection.Assembly.GetEntryAssembly()!);
var engineVersion = VersionOf(typeof(PhoenixmlDb.Xslt.XsltTransformer).Assembly);

if (args.Contains("--version", StringComparer.Ordinal))
{
    Console.WriteLine($"xslt-mcp {serverVersion}");
    Console.WriteLine($"  PhoenixmlDb.Xslt {engineVersion}");
    return 0;
}

// Resolve spec data: env var > CLI arg > filesystem fallback > embedded resources
var specPath = Environment.GetEnvironmentVariable("XSLT_SPEC_PATH");

for (var i = 0; i < args.Length; i++)
{
    if (args[i] is "--spec-path" && i + 1 < args.Length)
        specPath = args[++i];
}

SpecIndex index;
if (specPath != null)
{
    index = SpecIndex.Load(specPath);
    await Console.Error.WriteLineAsync(
        $"[xslt-mcp] Loaded {index.GetAll().Count} spec entries from {specPath}");
}
else
{
    // Try filesystem first (local dev), fall back to embedded resources (dotnet tool)
    var fsPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", ".."));
    if (Directory.Exists(fsPath) &&
        Directory.EnumerateFiles(fsPath, "*.md", SearchOption.AllDirectories).Any())
    {
        index = SpecIndex.Load(fsPath);
        await Console.Error.WriteLineAsync(
            $"[xslt-mcp] Loaded {index.GetAll().Count} spec entries from {fsPath}");
    }
    else
    {
        index = SpecIndex.LoadFromAssembly(typeof(SpecIndex).Assembly);
        await Console.Error.WriteLineAsync(
            $"[xslt-mcp] Loaded {index.GetAll().Count} spec entries from embedded resources");
    }
}

// Build and run MCP server
var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole(options =>
{
    // MCP uses stdio — log to stderr only
    options.LogToStandardErrorThreshold = LogLevel.Trace;
});

builder.Services.AddSingleton(index);

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly()
    .WithPromptsFromAssembly()
    .WithResourcesFromAssembly();

await Console.Error.WriteLineAsync(
    $"[xslt-mcp] {serverVersion}, bundling PhoenixmlDb.Xslt {engineVersion}").ConfigureAwait(false);

var host = builder.Build();
await host.RunAsync().ConfigureAwait(false);
return 0;
