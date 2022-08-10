using Pipelines.Services;

namespace Pipelines.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggingService _logger;

    public LoggingMiddleware(RequestDelegate next, ILoggingService logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // log the incoming request path
        _logger.Log(LogLevel.Information, context.Request.Path);
        
        // invoke the next middleware in the pipeline
        await _next(context);
        
        // log distinct response headers
        var responseHeaders = context.Response.Headers
            .Select(x => x.Key).Distinct();
        _logger.Log(LogLevel.Information, string.Join(", ",responseHeaders));
    }
}