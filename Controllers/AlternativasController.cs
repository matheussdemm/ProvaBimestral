using Microsoft.AspNetCore.Mvc;
using ProvaBimestral.Models;

namespace ProvaBimestral.Controllers
{
    public class AlternativasController : Controller
    {
        public static List<Alternativas> IsAlternativas = new List<Alternativas>();
        public IActionResult Index()
        {
            return View(IsAlternativas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Alternativas objeto)
        {
            IsAlternativas.Add(objeto); 
            return RedirectToAction("Index");
        }
    }
}
