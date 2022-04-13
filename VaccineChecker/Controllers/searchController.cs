using Microsoft.AspNetCore.Mvc;
using VaccineChecker.Models;
using static VaccineChecker.Models.CurrentuserData;

namespace VaccineChecker.Controllers
{
    public class searchController : Controller
    {
        VaccinecheckerContext db;

        public searchController() {
            db = new VaccinecheckerContext();
        }
        public IActionResult Index()
        {
            if (logged) {
                return View("searchForStudent");
            }

            Response.StatusCode = 403;
            return View("NotAuthorized");
           
        }
        public  IActionResult searchStudent()
        {
            if (logged) {

                try
                {
                    string term = HttpContext.Request.Query["id"].ToString();
                    string type = HttpContext.Request.Query["type"].ToString();
                    List<string> data;
                    if (type == "name")
                    {
                        data = db.students.Where(p => p.name.Contains(term))
                              .Select(s => s.name + $":{s.vaccined}:{s.faculty}").ToList();
                    }
                    else if (type == "email")
                    {
                        data = db.students.Where(s => s.email.Contains(term))
                             .Select(s => s.email + $":{s.vaccined}:{s.faculty}").ToList();
                    }
                    else
                    {
                        data = db.students.Where(s => s.NationalID.Contains(term))
                              .Select(s => s.NationalID + $":{s.vaccined}:{s.faculty}").ToList();
                    }
                    Response.StatusCode = 200;
                    return Json(data);


                }
                catch (Exception)
                {
                    return BadRequest();
                }

            }

            Response.StatusCode = 403;
            return View("NotAuthorized");
           
        }
    }
}
