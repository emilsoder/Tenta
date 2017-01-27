using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Tenta.Models;
using Microsoft.AspNetCore.Http;

namespace Tenta.Controllers
{
    public class Uppgift2ViewModel
    {
        public List<Bok> BokList { get; set; }
        public Bok bok { get; set; }
    }

    public class ViewModel2
    {
        public int BokId { get; set; }
        public string Titel { get; set; }
        public string Forfattare { get; set; }
        public bool Ilager { get; set; }
        public int AntalSidor { get; set; }
    }

    public class Uppgift2Controller : Controller
    {
        private readonly TentamenMVCContext _context;

        public Uppgift2Controller(TentamenMVCContext context)
        {
            _context = context;
        }

        public IActionResult Index(string error)
        {
            var model = new Uppgift2ViewModel
            {
                BokList = _context.Bok.ToList()
            };
            if (error == "tooMany")
            {
                ViewData["error"] = "tooMany";
            }
            if (error == "duplicate")
            {
                ViewData["error"] = "duplicate";
            }

            return View(model);
        }

        [HttpPost, ActionName("AddSelectedBook")]
        [ValidateAntiForgeryToken]
        public IActionResult AddSelectedBook(Uppgift2ViewModel vm)
        {
            if (HttpContext.Session.Keys.Any(x => x == vm.bok.BokId.ToString()))
            {
                return RedirectToAction("Index", new { error = "duplicate" });
            }
            if (HttpContext.Session.Keys.Count() >= 3)
            {
                return RedirectToAction("Index", new { error = "tooMany" });
            }


            if (ModelState.IsValid)
            {
                var model = _context.Bok.Where(x => x.BokId == vm.bok.BokId).SingleOrDefault();
                HttpContext.Session.SetString(model.BokId.ToString(), model.BokId.ToString());
            }

            return RedirectToAction("Index");
        }

        public IActionResult AllBooks()
        {
            var model = new ViewModel2();
            List<Bok> SelectedBooks = new List<Bok>();
            foreach (var item in HttpContext.Session.Keys)
            {
                int id = Convert.ToInt32(item);
                var bok = _context.Bok.Where(x => x.BokId == id).SingleOrDefault();
                model.BokId = bok.BokId;
                model.AntalSidor = bok.AntalSidor;
                model.Forfattare = bok.Forfattare;
                model.Ilager = bok.Ilager;
                model.Titel = bok.Titel;

                SelectedBooks.Add(new Bok
                {
                    BokId = model.BokId,
                    AntalSidor = bok.AntalSidor,
                    Forfattare = bok.Forfattare,
                    Ilager = bok.Ilager,
                    Titel = bok.Titel
                });
            }
            return View(SelectedBooks);
        }
    }
}

