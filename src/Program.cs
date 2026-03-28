using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using XsltMcpServer;

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
    .WithToolsFromAssembly();

var host = builder.Build();
await host.RunAsync();
