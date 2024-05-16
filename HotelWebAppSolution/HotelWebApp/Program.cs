using HotelWebApp.Data;
using HotelWebApp.Interfaces;
using Microsoft.Extensions.Options;
using HotelWebApp.Models;
using HotelWebApp.Controllers;

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
    app.UseExceptionHandler("/Apartments/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "allApartments",
    pattern: "allApartments/",
     defaults: new { controller = "Apartments", action = "AllApartments" });

app.MapControllerRoute(
    name: "apartmentInfo",
    pattern: "apartmentInfo/{id}",
     defaults: new { controller = "Apartments", action = "OneApartment" }
    );

app.MapControllerRoute(
    name: "changeApartmentInfo",
    pattern: "changeApartmentInfo/{id}",
     defaults: new { controller = "Apartments", action = "ChangeApartmentInfo" }
    );

app.MapControllerRoute(
    name: "updateApartmentInfo",
    pattern: "updateApartmentInfo/{id}",
     defaults: new { controller = "Apartments", action = "UpdateApartmentInfo" }
    );

app.MapControllerRoute(
    name: "deleteApartment",
    pattern: "deleteApartment/{id}",
     defaults: new { controller = "Apartments", action = "DeleteApartment" }
    );

app.MapControllerRoute(
    name: "insertApartmentForm",
    pattern: "insertApartmentForm/",
     defaults: new { controller = "Apartments", action = "InsertFormPage" }
    );

app.MapControllerRoute(
    name: "insertNewApartment",
    pattern: "insertNewApartment/",
     defaults: new { controller = "Apartments", action = "InsertNewApartment" }
    );

app.Run();
