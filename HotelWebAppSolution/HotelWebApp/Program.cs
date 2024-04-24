using HotelApp.Models;
using HotelWebApp.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


// MongoDB
builder.Services.Configure<HotelAppDBSettings>(
    builder.Configuration.GetSection("HotelAppDatabase"));

builder.Services.AddSingleton<Apartment>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var settings = Options.Create(new HotelAppDBSettings
{
    ConnectionString = "mongodb+srv://abojovic:abojovic@hotel-app.ezsvejd.mongodb.net/?retryWrites=true&w=majority&appName=Hotel-App",
    DatabaseName = "Hotel-App",
    CollectionName = "Apartments"
});

var apartment = new Apartment(settings);
apartment.IsConnectedAsync();

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
