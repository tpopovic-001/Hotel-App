using HotelWebApp.Models;
using MongoDB.Driver;
using System;
using System.Linq;



namespace HotelWebApp.Interfaces
{
    public interface IApartment
    {
        IMongoCollection<Apartment> apartmentsCollection { get; }
        Apartment GetApartment(string id);
        public List<Apartment> GetAllApartments();
        void CreateApartment(Apartment apartment);
        void UpdateApartment(string id, Apartment apartment);
        void DeleteApartment(string id);
    }
}
