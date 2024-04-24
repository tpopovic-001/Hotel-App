using HotelApp.Models;
using HotelWebApp.Models;
using Microsoft.Extensions.Options;

class ModelTesting
{
    static async Task Main(string[] args)
    {
        // Instantiate Apartment class with proper settings
        var settings = Options.Create(new HotelAppDBSettings
        {
            ConnectionString = "mongodb+srv://abojovic:abojovic@hotel-app.ezsvejd.mongodb.net/?retryWrites=true&w=majority&appName=Hotel-App",
            DatabaseName = "Hotel-App",
            CollectionName = "Apartments"
        });

        var apartment = new Apartment(settings);

        // Create a new apartment
        var newApartment = new Apartment();

        await apartment.CreateAsync(newApartment);

        // Retrieve all apartments
        var allApartments = await apartment.GetAsync();

        foreach (var apt in allApartments)
        {
            Console.WriteLine($"Apartment ID: {apt.Id}, Name: {apt.ApartmentName}");
        }

        // Retrieve a specific apartment by ID
        var specificApartment = await apartment.GetAsync(allApartments.First().Id);
        Console.WriteLine($"Specific Apartment ID: {specificApartment.Id}, Name: {specificApartment.ApartmentName}");

        // Update an existing apartment
        specificApartment.ApartmentName = "Updated Apartment Name";
        await apartment.UpdateAsync(specificApartment.Id, specificApartment);

        // Remove an apartment
        await apartment.RemoveAsync(specificApartment.Id);

        // Ensure apartment was removed
        var removedApartment = await apartment.GetAsync(specificApartment.Id);
        if (removedApartment == null)
        {
            Console.WriteLine("Apartment successfully removed.");
        }
        else
        {
            Console.WriteLine("Failed to remove apartment.");
        }
    }
}
