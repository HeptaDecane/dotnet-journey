using System.Diagnostics;
using Pipelines.Services;

namespace Pipelines.Middlewares;

public class TimerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggingService _logger;

    public TimerMiddleware(RequestDelegate next, ILoggingService logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        await _next(context);
        
        stopwatch.Stop();
        _logger.Log(LogLevel.Information, $"execution time - {stopwatch.ElapsedMilliseconds}ms");

    }
}