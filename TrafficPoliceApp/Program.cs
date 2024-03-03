using TrafficPoliceApp.Repositories;
using TrafficPoliceApp.Repositories.Base;
using TrafficPoliceApp.Services.Base;
using TrafficPoliceApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrafficPoliceApp.Data;
using TrafficPoliceApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();

// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie(o => 
//     {
//         o.LoginPath = "/Identity/Login";
//         o.ReturnUrlParameter = "returnUrl";
//     });


builder.Services.AddScoped<IFineRepository, FineRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ILoggerRepository, LoggingRepository>();

builder.Services.AddDbContext<MyDbContext>(dbContextOptionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("TrafficPoliceDb");
    dbContextOptionsBuilder.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IIdentityService, IdentityService>();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = true;
})
    .AddEntityFrameworkStores<MyDbContext>();

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