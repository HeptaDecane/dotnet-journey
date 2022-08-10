namespace Pipelines.Middlewares;

public class DelayMiddleware
{
    private readonly RequestDelegate _next;

    public DelayMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await Task.Delay(100);
        await _next(context);
        await Task.Delay(100);
    }

}