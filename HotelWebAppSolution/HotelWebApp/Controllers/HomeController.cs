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
        public async Task<IActionResult> Index(Apartment apartment)
        {
            // Vraca Apartman sa datim ID 
            // return Json(_apartment.GetApartment("6626c9d6b3538a3c2827f0d5"));

            // Vraca sve Apartmane
             return Json(_apartment.GetAllApartments());

            // Dodavanje apartmana
            // Potrebno je dodati [HttpPost] dekorator iznad metode
            // Objekat koji prima metoda za dodavanje se pravi pomocu POST parametara,
            // znaci napraviti formu sa Name atributima koji imaju nazive kao atributi
            // modela Apartment.cs
            //
            //Ovo staviti u metodu za dodavanje apartmana
            // _apartment.CreateApartment(apartment);
            // return Json(_apartment.GetAllApartments());
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
