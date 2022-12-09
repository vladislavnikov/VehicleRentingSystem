using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<VehicleDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/Login";
});

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
