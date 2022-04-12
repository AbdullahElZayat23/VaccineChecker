using Microsoft.AspNetCore.Mvc;
using VaccineChecker.Models;
using static VaccineChecker.Models.CurrentuserData;

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
            if (isadmin && logged)
            {
                var result = db.students.ToList();

                if (result.Count > 0)
                {
                    ViewBag.studentcount = result.Count;
                    ViewBag.vaccined = result.Count(s => s.vaccined == true);
                    ViewBag.notvaccined = result.Count(s => s.vaccined == false);
                }
                else
                {
                    ViewBag.studentcount = 0;
                    ViewBag.vaccined = 0;
                    ViewBag.notvaccined = 0;
                }
                return View("dashboard");
            }
            else if (logged && isadmin == false)
            {
                //serach
                return View();
            }
            else {
                //not authorized
                Response.StatusCode = 403;
                return View("NotAuthorized");
            }
        }

        public IActionResult showlist() {

            if (isadmin && logged)
            {
                var result = db.students.ToList();

                return View("studentsList", result);
            }
            else
            {
                //not authorized
                Response.StatusCode = 403;
                return View("NotAuthorized");
            }
        }

        public IActionResult CreateStudent() {

            if (isadmin && logged)
            {
                return View("createstudent", new student());
            }
            else
            {
                //not authorized
                Response.StatusCode = 403;
                return View("NotAuthorized");
            }

        }


        public IActionResult SavestudentData(student std)
        {

            if (isadmin && logged)
            {
                db.students.Add(new student() {
                    NationalID = std.NationalID,
                    name = std.name,
                    email = std.email,
                    faculty = std.faculty,
                    vaccined = std.vaccined

                });
                db.SaveChanges();

                var result = db.students.ToList();

                if (result.Count > 0)
                {
                    ViewBag.studentcount = result.Count;
                    ViewBag.vaccined = result.Count(s => s.vaccined == true);
                    ViewBag.notvaccined = result.Count(s => s.vaccined == false);
                }
                else
                {
                    ViewBag.studentcount = 0;
                    ViewBag.vaccined = 0;
                    ViewBag.notvaccined = 0;
                }
                return View("dashboard");
            }
            else
            {
                //not authorized
                Response.StatusCode = 403;
                return View("NotAuthorized");
            }


        }

        public IActionResult checkNationalID(string nationalID) {
            var result =  db.students.Where(u => u.NationalID == nationalID).ToList();
            if (result.Count > 0) {
                return Json(false);
            }
            return Json(true);
        }

        public IActionResult editstudent(string id){


            if (isadmin && logged)
            {

                ViewBag.id = id;
                return View();
            }
            else
            {
                //not authorized
                Response.StatusCode = 403;
                return View("NotAuthorized");
            }



        }


        public IActionResult Saveeditstudent(student std)
        {
            if (isadmin && logged)
            {
                var student = db.students.Where(s => s.NationalID == std.NationalID).First();

                student.email = std.email;
                student.faculty = std.faculty;
                student.vaccined = std.vaccined;
                student.name = std.name;
                db.SaveChanges();

                return View("studentsList", db.students);
            }
            else
            {
                //not authorized
                Response.StatusCode = 403;
                return View("NotAuthorized");
            }
           
        }

        public IActionResult showdelete(string id) {
            
            if (isadmin && logged)
            {
                var result = db.students.Where(s => s.NationalID == id).First();
                return View("deletestudent", result);
            }
            else
            {
                //not authorized
                Response.StatusCode = 403;
                return View("NotAuthorized");
            }
        }
        public IActionResult deletestudent(string id) {

            if (isadmin && logged)
            {

                var result = db.students.Where(s => s.NationalID == id).First();

                db.students.Remove(result);
                db.SaveChanges();

                return View("studentsList", db.students.ToList());
            }
            else
            {
                //not authorized
                Response.StatusCode = 403;
                return View("NotAuthorized");
            }

        
        }

        public IActionResult adduser() {

            return View();
        }



    }
}
