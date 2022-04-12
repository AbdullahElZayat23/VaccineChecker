using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VaccineChecker.Models;

namespace VaccineChecker.Controllers
{
    public class HomeController : Controller
    {
        VaccinecheckerContext db;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            db = new VaccinecheckerContext();
        }

        public IActionResult Index()
        {
            ViewBag.isok = true;
            return View();
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

        public IActionResult LoginCheck(user usr) {

            var result = db.users.Where(u=>u.username==usr.username && u.password==usr.password).ToList();
            if (result.Count > 0) {
                return RedirectToAction(actionName: "Index",controllerName:"admin");
            }
            ViewBag.isok = false;
          return  View("index");
            
        }
        
    }
}