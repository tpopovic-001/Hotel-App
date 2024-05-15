using HotelWebApp.Interfaces;
using HotelWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace HotelWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApartment _apartment;
        public HomeController(IApartment apartment)
        {
            _apartment = apartment; 
        }
        // [HttpPost]
        // [HttpPut("/{id}")]
        [HttpDelete("/{id}")]
        public async Task<IActionResult> Index(string id, Apartment apartment)
        {
            // Vraca Apartman sa datim ID 
            // return Json(_apartment.GetApartment("6644e4c7eb03339ca1b50daa"));

            // Vraca sve Apartmane
            // return Json(_apartment.GetAllApartments());

            // Dodavanje apartmana
            // Potrebno je dodati [HttpPost] dekorator iznad metode
            // Objekat koji prima metoda za dodavanje se pravi pomocu POST parametara,
            // znaci napraviti formu sa Name atributima koji imaju nazive kao atributi
            // modela Apartment.cs
            //
            //Ovo staviti u metodu za dodavanje apartmana
            // _apartment.CreateApartment(apartment);
            // return Json(_apartment.GetAllApartments());

            // Update apartmana            
            // Koristiti HttpPut dekorator kao u komentaru iznad ove funkcije
            // poziva se sa localhost/id_apartmana i u body idu podaci sa forme kao i na insert-u

            // Problem je sto nije klasican update, nego replace, pa ako u formi ne budu po default-u
            // uneseni svi podaci, za te podatke ce biti null u bazi
            // _apartment.UpdateApartment(id, apartment);
            // return Json(apartment);

            // Brisanje
            _apartment.DeleteApartment(id);
            return Json("Deleted: " + id);

        }

        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
