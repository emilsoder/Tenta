using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Tenta.Controllers
{
    public class Uppgift1Model
    {
        [Required(ErrorMessage = "Fältet får inte vara tomt")]
        [MaxLength(50, ErrorMessage = "Max 50 tecken.")]
        [Display(Name = "Titel")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Fältet får inte vara tomt")]
        [MaxLength(50, ErrorMessage = "Max 50 tecken.")]
        [Display(Name = "Författare")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Fältet får inte vara tomt")]
        [Range(1, 10000, ErrorMessage = "Antal sidor måste vara mellan 1 och 10 000")]
        [Display(Name = "Antal sidor")]
        public int Pages { get; set; }

        [Required(ErrorMessage = "Du måste välja en kategori.")]
        [Display(Name = "Kategori")]
        public string SelectedCategory { get; set; }

        [Required(ErrorMessage = "Du måste ange lagerstatus.")]
        public bool IsInStock { get; set; }

        // Jag hade hellre satt listans värden dess "get"-accessor här i modellklassen, istället för att behöva ge den nya värden inuti Actionmetoderna.
        // Exempelvis: List<string> Categories { get { return new List<string> { "Barn", "Deckare", "Roman", "Fakta" };}
        // Men jag följer uppgiftens direktiv istället.
        public List<string> Categories { get; set; }
    }

    public class Uppgift1Controller : Controller
    {
        public IActionResult Index()
        {
            var model = new Uppgift1Model();
            model.Categories = new List<string> { "Barn", "Deckare", "Roman", "Fakta" };
            return View(model);
        }

        [HttpPost, ActionName("InsertBook")]
        [ValidateAntiForgeryToken]
        public IActionResult InsertBook(Uppgift1Model model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ConfirmBook", model);
            }
            model.Categories = new List<string> { "Barn", "Deckare", "Roman", "Fakta" };
            return View("Index", model);
        }

        public IActionResult ConfirmBook(Uppgift1Model model)
        {
            return View(model);
        }
    }
}
