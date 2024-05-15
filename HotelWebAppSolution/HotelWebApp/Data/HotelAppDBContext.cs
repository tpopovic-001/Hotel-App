using HotelWebApp.Models;
using HotelWebApp.Interfaces;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace HotelWebApp.Data
{
    public class HotelAppDBContext : IApartment
    {
        public readonly IMongoDatabase _db;
        public HotelAppDBContext(IOptions<DBSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.DatabaseName);
        }
        public IMongoCollection<Apartment> apartmentsCollection => _db.GetCollection<Apartment>("Apartments");
        public Apartment GetApartment(string id) 
        {
            var apartment = apartmentsCollection.Find(a => a.Id == id).FirstOrDefault();
            return apartment;
        }
        public List<Apartment> GetAllApartments() 
        {
            return apartmentsCollection.Find(a=>true).ToList();
        }
        public void CreateApartment(Apartment apartment)
        {
            apartmentsCollection.InsertOne(apartment);
        }        
        public void UpdateApartment(string id, Apartment apartment) 
        {
            
            var filter = Builders<Apartment>.Filter.Eq(c => c.Id, id);
            var update = Builders<Apartment>.Update
                .Set("ApartmentName", apartment.ApartmentName)
                .Set("Address", apartment.Address)
                .Set("ApartmentDescription", apartment.ApartmentDescription)
                .Set("Location", apartment.Location)
                .Set("City", apartment.City)
                .Set("Country", apartment.Country)
                .Set("ImagePaths", apartment.ImagePaths)
                .Set("MinimumStay", apartment.MinimumStay)
                .Set("NumOfBeds", apartment.NumOfBeds)
                .Set("PricePerNight", apartment.PricePerNight);

            apartmentsCollection.UpdateOne(filter, update);

                
        }
        public void DeleteApartment(string id) 
        {
            var filter = Builders<Apartment>.Filter.Eq(c => c.Id, id);
            apartmentsCollection.DeleteOne(filter); 
        }
    }
}
