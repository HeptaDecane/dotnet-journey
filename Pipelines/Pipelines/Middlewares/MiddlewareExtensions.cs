namespace Pipelines.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseResponseMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ResponseMiddleware>();
    }

    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<LoggingMiddleware>();
    }

    public static IApplicationBuilder UseDelayMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<DelayMiddleware>();
    }

    public static IApplicationBuilder UseTimerMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<TimerMiddleware>();
    }
}

