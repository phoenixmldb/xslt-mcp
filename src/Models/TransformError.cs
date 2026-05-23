namespace XsltMcpServer.Models;

public sealed record TransformError(
    string Code,
    string Message,
    int? Line,
    int? Column,
    string? SourceSnippet,
    string? SpecUrl);
