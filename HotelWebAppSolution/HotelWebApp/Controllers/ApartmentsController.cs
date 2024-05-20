using HotelWebApp.Interfaces;
using HotelWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Threading.Tasks.Dataflow;


namespace HotelWebApp.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly IApartment _apartment;
        private readonly IWebHostEnvironment _hostingEnvironmentField;
        public ApartmentsController(IApartment apartment, IWebHostEnvironment _hostingEnvironment)
        {
            _apartment = apartment;
            _hostingEnvironmentField = _hostingEnvironment;
        }

        // Ova metoda/akcija dohvata sve dostupne apartmane 
        [HttpGet]
        public async Task<IActionResult> AllApartments()
        {
            var apartments = _apartment.GetAllApartments(); 
            return View("Index",apartments);
        }

        // Ova metoda/akcija dohvata podatke za jedan apartman
        [HttpGet]
        public async Task<IActionResult> OneApartment(string id)
        {
            var oneApt = _apartment.GetApartment(id);
            return View("ApartmentInfo",oneApt);
        }

        // Ova metoda/akcija na osnovu izabranog apartmana prikazuje formu za izmenu podataka o
        // apartmanu
        [HttpGet]
        public async Task<IActionResult> ChangeApartmentInfo(string id)
        {
            var oneApt = _apartment.GetApartment(id);
            return View("ChangeApartmentInfo", oneApt);
        }

        // Akcija koja popunjava model Apartment.cs sa podacima za azuriranje sa forme
        [HttpPost]
        public async Task<IActionResult> UpdateApartmentInfo(string id, List<IFormFile> ImagePaths)
        {
            Apartment apt = _apartment.GetApartment(id);

            apt.ApartmentName = Request.Form["ApartmentName"];
            apt.Address = Request.Form["Address"];
            apt.ApartmentDescription = Request.Form["ApartmentDescription"];
            apt.Location = Request.Form["Location"];

            if(apt.ImagePaths == null)
            {
                apt.ImagePaths = new List<string>();
            }
         
            foreach (IFormFile file in ImagePaths)
            {
                if (file.Length > 0)
                {
                    var imagesFolder = Path.Combine(_hostingEnvironmentField.WebRootPath, "images");
                    var uniqueFileName = $"{Guid.NewGuid().ToString()}_{file.FileName}";
                    var filePath = Path.Combine(imagesFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    apt.ImagePaths.Add($"/images/{uniqueFileName}");
                }
            }
            apt.City = Request.Form["City"];
            apt.Country = Request.Form["Country"];
            apt.MinimumStay = Int32.Parse(Request.Form["MinimumStay"]);
            apt.PricePerNight = Convert.ToDecimal(Request.Form["PricePerNight"]);
            apt.NumOfBeds = Int32.Parse(Request.Form["NumOfBeds"]);
            _apartment.UpdateApartment(id, apt);

            return RedirectToRoute("apartmentInfo", new { id = id });                                
        }
        // Akcija koja brise apartman po njegovom id-u i vraca korisnika na pocetnu stranicu na ruti allApartments/
        [HttpGet]
        public async Task<IActionResult> DeleteApartment(string id)
        {
             _apartment.DeleteApartment(id);

            return RedirectToRoute("allApartments");
        }

        // Akcija koja se okida kada se brisu fotografije za izabrani apartman
        [HttpPost]
        public async Task<IActionResult> DeletePhoto(string Id, string ImagePath)
        {
            Apartment apt = _apartment.GetApartment(Id);
            if (apt == null)
            {
                return Json(new { success = false, message = "Apartman sa ovim Id-em ne postoji!"});
            }

            if (apt.ImagePaths.Contains(ImagePath))
            {
                apt.ImagePaths.Remove(ImagePath);
                var PathToImages = Path.Combine(_hostingEnvironmentField.WebRootPath, ImagePath.TrimStart('/'));

                if (System.IO.File.Exists(PathToImages))
                {
                    System.IO.File.Delete(PathToImages);
                }

                _apartment.UpdateApartment(Id, apt);
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Fotografija nije pronadjena!" });
        }

        // Akcija koja vraca view sa formom za unos novog apartmana
        [HttpGet]
        public async Task<IActionResult> InsertFormPage()
        {
            return View("InsertForm");
        }
        // Akcija pomocu koje se objekat klase Apartment.cs puni podacima sa forme i onda se salje na bazu
        [HttpPost]
        public async Task<IActionResult> InsertNewApartment(List<IFormFile> ImagePaths)
        {
            Apartment apt = new Apartment();
            apt.ApartmentName = Request.Form["ApartmentName"];
            apt.Address = Request.Form["Address"];
            apt.ApartmentDescription = Request.Form["ApartmentDescription"];
            apt.Location = Request.Form["Location"];

            apt.ImagePaths = new List<string>();
            foreach(IFormFile file in ImagePaths)
            {
                if(file.Length > 0)
                {
                    var imagesFolder = Path.Combine(_hostingEnvironmentField.WebRootPath, "images");
                    var uniqueFileName = $"{Guid.NewGuid().ToString()}_{file.FileName}";
                    var filePath = Path.Combine(imagesFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                   apt.ImagePaths.Add($"/images/{uniqueFileName}");
                }
            }

            apt.City = Request.Form["City"];
            apt.Country = Request.Form["Country"];
            apt.MinimumStay = Int32.Parse(Request.Form["MinimumStay"]);
            apt.PricePerNight = Convert.ToDecimal(Request.Form["PricePerNight"]);
            apt.NumOfBeds = Int32.Parse(Request.Form["NumOfBeds"]);
            _apartment.CreateApartment(apt);

            return RedirectToRoute("allApartments");
        }
    }
}
