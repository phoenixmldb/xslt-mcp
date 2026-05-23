namespace XsltMcpServer.Models;

public sealed record TransformResult(
    bool Ok,
    string? Value,
    string? OutputMethod,
    long? ElapsedMs,
    IReadOnlyList<TransformError>? Errors)
{
    public static TransformResult Success(string? value, string? outputMethod, long? elapsedMs) =>
        new(true, value, outputMethod, elapsedMs, null);

    public static TransformResult Failure(IReadOnlyList<TransformError> errors) =>
        new(false, null, null, null, errors);
}
