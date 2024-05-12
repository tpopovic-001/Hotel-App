using HotelWebApp.Data;
using HotelWebApp.Interfaces;
using Microsoft.Extensions.Options;
using HotelWebApp.Models;
using HotelWebApp.Interfaces;

var builder = WebApplication.CreateBuilder(args);


// MongoDB
builder.Services.Configure<DBSettings>(
    options =>
    {
        options.ConnectionString = builder.Configuration.GetSection("MongoDB:ConnectionString").Value;
        options.DatabaseName = builder.Configuration.GetSection("MongoDB:DatabaseName").Value;
    });
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IApartment, HotelAppDBContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
