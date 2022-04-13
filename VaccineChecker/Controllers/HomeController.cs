using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VaccineChecker.Models;
using static VaccineChecker.Models.CurrentuserData;

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

        public IActionResult contact()
        {
            return View();
        }

        public IActionResult about()
        {
            return View();
        }

        public IActionResult logout()
        {

            logged = false;
            isadmin = false;
            return RedirectToAction(actionName:"index",controllerName: "home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult LoginCheck(user usr) {
            if (!string.IsNullOrEmpty(usr.username)) {
                var Curuser = db.users.Where(u => u.username == usr.username && u.password == usr.password).ToList();
                if (Curuser.Count > 0)
                {
                    logged = true;
                    username = usr.username;
                    isadmin = false;
                    if ((bool)Curuser[0].isadmin)
                    {
                        isadmin = true;
                    }
                    return RedirectToAction(actionName: "Index", controllerName: "admin");
                }
            }
            
            ViewBag.isok = false;
          return  View("index");
            
        }
        
    }
}