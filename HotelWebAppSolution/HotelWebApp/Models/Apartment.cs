using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Cryptography;
using HotelWebApp.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Runtime.Serialization;
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
        public string? ApartmentName { get; set; }
        public string? Address { get; set; }
        public string? ApartmentDescription { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Location { get; set; }
        public List<string>? ImagePaths { get; set; }
        public int? MinimumStay { get; set; }
        public int? NumOfBeds { get; set; }
        public decimal? PricePerNight { get; set; }


        /*public Apartment()
        {
            this.ApartmentName = "test";
            this.ApartmentDescription = "test";
            this.Address = "test";
            this.City = "test";
            this.Country = "test";
            this.Location = "test";
            this.ImagePaths = new List<string> { "test", "test" };
            this.MinimumStay = 5;
            this.NumOfBeds = 5;
            this.PricePerNight = 100;
        }*/
        //////////////////////////     Konekcija     ////////////////////////////////

        public static IMongoCollection<Apartment> _apatrmentsCollection; // instanca kolekcije u koju se smestaju podaci sa baze        





        ////////////////////////// Metode za pristup ////////////////////////////////

    }


}
