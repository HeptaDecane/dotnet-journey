using System.Security.Claims;
using Authy;
using Authy.Models;
using Authy.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// db configs
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("AuthyDatabase"));

//services
builder.Services.AddSingleton<UsersServices>();
builder.Services.AddSingleton<WeatherApiService>();

// TODO: auth handlers
builder.Services.AddAuthentication(User.AuthName).AddCookie(User.AuthName, options => {
    options.Cookie.Name = User.AuthName;
    options.LoginPath = "/users/login";
    options.AccessDeniedPath = "/users/AccessDenied";
});

// TODO: policy based authorization
builder.Services.AddAuthorization(options=>{
    options.AddPolicy("Admin", policy=>{
        policy.RequireClaim(ClaimTypes.Role, "ADMIN");
    });
    
    // more policies can be added here... using options.AddPolicy()
});

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

app.UseRouting();

// TODO: use authentication middleware
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();