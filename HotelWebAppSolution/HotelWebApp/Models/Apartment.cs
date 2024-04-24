using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Cryptography;
using HotelWebApp.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using HotelApp.Models;
/*_id
apartment_name
address
apartment_description
apartment_location
city
country
image_paths string[]
minimum_stay
num_of_beds
price_per_night
*/

namespace HotelWebApp.Models
{
    public class Apartment
    {
        // dekoratori za id (unique, auto-increment i ostale gluposti)
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement]
        public string ApartmentName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public List<string> ImagePaths { get; set; }
        public int MinimumStay { get; set; }
        public int NumOfBeds { get; set; }
        public decimal PricePerNight { get; set; }
        

        public Apartment()
        {
            this.ApartmentName = "test";
            this.Address = "test";
            this.City = "test";
            this.Country = "test";
            this.Location = "test";
            this.ImagePaths = new List<string> { "test", "test" };
            this.MinimumStay = 5;
            this.NumOfBeds = 5;
            this.PricePerNight = 100;
        }
        //////////////////////////     Konekcija     ////////////////////////////////

        public IMongoCollection<Apartment> _apatrmentsCollection; // instanca kolekcije u koju se smestaju podaci sa baze
        public Apartment(IOptions<HotelAppDBSettings> hotelAppDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                hotelAppDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                hotelAppDatabaseSettings.Value.DatabaseName);

            _apatrmentsCollection = mongoDatabase.GetCollection<Apartment>(
                hotelAppDatabaseSettings.Value.CollectionName);            
        }


        public async Task<bool> IsConnectedAsync()
        {
            bool dalije;
            try
            {
                // Perform a simple operation to check the connection
                _apatrmentsCollection.Find(_ => true).Limit(1).FirstOrDefaultAsync();                
                Console.WriteLine("Ima konekcije");
                return true;
            }
            catch (Exception)
            {                
                Console.WriteLine("Nema konekcije");
                return false;
            }
        }


        ////////////////////////// Metode za pristup ////////////////////////////////

        public async Task<List<Apartment>> GetAsync() =>
            await _apatrmentsCollection.Find(_ => true).ToListAsync();

        public async Task<Apartment> GetAsync(string id) =>
        await _apatrmentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Apartment newApartment) =>
            await _apatrmentsCollection.InsertOneAsync(newApartment);

        public async Task UpdateAsync(string id, Apartment updatedApartment) =>
            await _apatrmentsCollection.ReplaceOneAsync(x => x.Id == id, updatedApartment);

        public async Task RemoveAsync(string id) =>
            await _apatrmentsCollection.DeleteOneAsync(x => x.Id == id);
    }


}
