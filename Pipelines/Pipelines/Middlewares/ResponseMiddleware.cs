namespace Pipelines.Middlewares;

public class ResponseMiddleware
{
    private readonly RequestDelegate _next;

    public ResponseMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context) {
        await context.Response.WriteAsync("Hello Readers!");
        // await next(context)
    }
}