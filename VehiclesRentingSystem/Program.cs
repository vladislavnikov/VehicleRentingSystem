using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using VehicleRentingSystem.Contracts;
using VehicleRentingSystem.Data.Models;
using VehicleRentingSystem.Services;
using VehiclesRentingSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<VehicleDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<User>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = false;
//    options.Password.RequireDigit = false;
//    options.Password.RequireLowercase = false;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//})
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<VehicleDbContext>();

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<VehicleDbContext>();


//


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/Login";
});

//builder.Services.Configure<RazorViewEngineOptions>(options =>
//{
//    options.AreaViewLocationFormats.Clear();
//    options.AreaViewLocationFormats.Add("/Views/Car/All.cshtml");
//    //options.AreaViewLocationFormats.Add("/Admin/{2}/Views/Shared/{0}.cshtml");
//    //options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
//});

//{ 0}
//-Action Name
//{ 1}
//-Controller Name
//{ 2}
//-Area Name

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICarService,CarService>();
builder.Services.AddScoped<IBusService,BusService>();
builder.Services.AddScoped<IBoatService,BoatService>();
builder.Services.AddScoped<ITruckService,TruckService>();
builder.Services.AddScoped<IBikeService,BikeService>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}/{id?}");

//app.UseEndpoints(
//                endpoints =>
//                {
//                    endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
//                    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
//                    endpoints.MapRazorPages();
//                });


app.MapRazorPages();

app.Run();
