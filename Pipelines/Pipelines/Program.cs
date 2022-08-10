using Pipelines.Middlewares;
using Pipelines.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ILoggingService, LoggingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// TODO: app.Use()
app.Use(async (context, next) => {
    //Do work that does not write to the Response
    await next.Invoke();
    //Do logging or other work that does not write to the Response.
});

/*
    // TODO: app.Run()
    app.Run(async (context) => {
        await context.Response.WriteAsync("Hello Readers!");
    });
    // nothing below this point will be executed, Run() is always terminal
*/

/*
    // TODO: app.UseMiddleware()
    app.UseMiddleware<ResponseMiddleware>();
    // terminal middleware by definition
*/

// TODO: using ApplicationBuilder extensions
app.UseTimerMiddleware();
app.UseLoggingMiddleware();
app.UseDelayMiddleware();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
