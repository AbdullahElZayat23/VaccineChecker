using Microsoft.AspNetCore.Mvc;
using VaccineChecker.Models;

namespace VaccineChecker.Controllers
{
    public class AdminController : Controller
    {
        VaccinecheckerContext db;
        public AdminController() {
            db = new VaccinecheckerContext();
        }

        public IActionResult Index()
        {
            var result = db.students.ToList();

            if (result.Count > 0)
            {
                ViewBag.studentcount = result.Count;
                ViewBag.vaccined = result.Count(s => s.vaccined == true);
                ViewBag.notvaccined = result.Count(s => s.vaccined == false);
            }
            else {
                ViewBag.studentcount = 0;
                ViewBag.vaccined = 0;
                ViewBag.notvaccined = 0;
            }
            


            return View("dashboard");
        }
    }
}
