using Microsoft.AspNetCore.Authentication.Cookies;
using TrafficPoliceApp.Repositories;
using TrafficPoliceApp.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Turbo.az.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o => 
    {
        o.LoginPath = "/Identity/Login";
        o.ReturnUrlParameter = "returnUrl";
    });


builder.Services.AddSingleton<IFineRepository, FineRepository>();

builder.Services.AddSingleton<IUserRepository>(provider => 
{
    string connectionStringName = "TrafficPoliceDb";

    string? connectionString = builder.Configuration.GetConnectionString(connectionStringName);

    if (string.IsNullOrEmpty(connectionString) || string.IsNullOrWhiteSpace(connectionString)) 
    {
        throw new Exception($"{connectionStringName} not found");
    }

    return new UserRepository(connectionString);
});

builder.Services.AddScoped<ILoggerRepository>(provider => 
{
    string connectionStringName = "TrafficPoliceDb";

    string? connectionString = builder.Configuration.GetConnectionString(connectionStringName);

    if (string.IsNullOrEmpty(connectionString) || string.IsNullOrWhiteSpace(connectionString)) 
    {
        throw new Exception($"{connectionStringName} not found");
    }

    bool isCustomLoggingEnabled = builder.Configuration.GetSection("isCustomLoggingEnabled").Get<bool>();

    return new LoggingRepository(connectionString, isCustomLoggingEnabled);
});

builder.Services.AddDbContext<MyDbContext>(dbContextOptionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("TrafficPoliceDb");
    dbContextOptionsBuilder.UseSqlServer(connectionString);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();