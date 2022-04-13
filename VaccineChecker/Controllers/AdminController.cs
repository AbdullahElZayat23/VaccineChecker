using Microsoft.AspNetCore.Mvc;
using VaccineChecker.Models;
using static VaccineChecker.Models.CurrentuserData;

namespace VaccineChecker.Controllers
{
    public class AdminController : Controller
    {
        VaccinecheckerContext db;
        public AdminController()
        {
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
                    ViewBag.percentage = (ViewBag.vaccined / (double)ViewBag.studentcount) * 100;
                }
                else
                {
                    ViewBag.studentcount = 0;
                    ViewBag.vaccined = 0;
                    ViewBag.notvaccined = 0;
                    ViewBag.percentage = 0;
                }           
                return View("dashboard");
            }
            else if (logged && isadmin == false)
            {
                //serach
                return RedirectToAction(actionName: "index", controllerName: "search");
            }

            //not authorized
            Response.StatusCode = 403;
            return View("NotAuthorized");

        }

        public IActionResult showlist()
        {

            if (isadmin && logged)
            {
                var result = db.students.ToList();

                return View("studentsList", result);
            }

            //not authorized
            Response.StatusCode = 403;
            return View("NotAuthorized");

        }

        public IActionResult CreateStudent()
        {

            if (isadmin && logged)
            {
                return View("createstudent", new student());
            }

            //not authorized
            Response.StatusCode = 403;
            return View("NotAuthorized");


        }

        public IActionResult SavestudentData(student std)
        {

            if (isadmin && logged)
            {
                db.students.Add(new student()
                {
                    NationalID = std.NationalID,
                    name = std.name,
                    email = std.email,
                    faculty = std.faculty,
                    vaccined = std.vaccined

                });
                db.SaveChanges();

                return RedirectToAction(actionName: "Index");
            }

            //not authorized
            Response.StatusCode = 403;
            return View("NotAuthorized");



        }

        public IActionResult checkNationalID(string nationalID)
        {

            if (logged && isadmin)
            {
                var result = db.students.Where(u => u.NationalID == nationalID).ToList();
                if (result.Count > 0)
                {
                    return Json(false);
                }
                return Json(true);
            }
            Response.StatusCode = 403;
            return View("NotAuthorized");

        }

        public IActionResult editstudent(string id)
        {


            if (isadmin && logged)
            {

                ViewBag.id = id;
                return View();
            }

            //not authorized
            Response.StatusCode = 403;
            return View("NotAuthorized");

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

            //not authorized
            Response.StatusCode = 403;
            return View("NotAuthorized");


        }

        public IActionResult showdelete(string id)
        {

            if (isadmin && logged)
            {
                var result = db.students.Where(s => s.NationalID == id).First();
                return View("deletestudent", result);
            }

            //not authorized
            Response.StatusCode = 403;
            return View("NotAuthorized");

        }
        public IActionResult deletestudent(string id)
        {

            if (isadmin && logged)
            {

                var result = db.students.Where(s => s.NationalID == id).First();

                db.students.Remove(result);
                db.SaveChanges();

                return View("studentsList", db.students.ToList());
            }

            //not authorized
            Response.StatusCode = 403;
            return View("NotAuthorized");
        }

        public IActionResult adduser()
        {
            if (logged && isadmin)
            {
                return View();
            }
            //not authorized
            Response.StatusCode = 403;
            return View("NotAuthorized");
        }

        public IActionResult saveuser(user usr)
        {
            if (logged && isadmin)
            {
                db.users.Add(new user()
                {
                    isadmin = usr.isadmin,
                    password = usr.password,
                    username = usr.username,
                });
                db.SaveChanges();

                return RedirectToAction(actionName: "Index");

            }
            //not authorized
            Response.StatusCode = 403;
            return View("NotAuthorized");

        }

        public IActionResult showusers()
        {

            if (logged && isadmin)
            {
                var result = db.users;
                return View(result);
            }
            //not authorized
            Response.StatusCode = 403;
            return View("NotAuthorized");

        }

        public IActionResult checkusername(string username)
        {

            if (logged && isadmin)
            {
                var result = db.users.Where(u => u.username == username).ToList();

                if (result.Count > 0)
                {
                    return Json(false);
                }
                return Json(true);

            }
            //not authorized
            Response.StatusCode = 403;
            return View("NotAuthorized");

        }

        public IActionResult deleteuser(string id)
        {

            if (logged && isadmin)
            {
                var result = db.users.Where(u => u.username == username).First();
                return View("deleteuser", result);
            }
            //not authorized
            Response.StatusCode = 403;
            return View("NotAuthorized");


        }
        [HttpPost]
        public IActionResult deleteuserData(string id)
        {

            if (logged && isadmin)
            {
                var result = db.users.Where(u => u.username == username).First();
                db.users.Remove(result);
                db.SaveChanges();

                return RedirectToAction(actionName: "showusers");
            }
            //not authorized
            Response.StatusCode = 403;
            return View("NotAuthorized");


        }
        public IActionResult edituser(string id)
        {

            if (logged && isadmin)
            {
                var result = db.users.Where(u => u.username == username).First();

                return View("edituser", result);
            }
            //not authorized
            Response.StatusCode = 403;
            return View("NotAuthorized");


        }
        [HttpPost]
        public IActionResult edituserData(user usr)
        {

            if (logged && isadmin)
            {
                var result = db.users.Where(u => u.username == username).First();
                result.isadmin = usr.isadmin;
                result.username = usr.username;
                result.password = usr.password;

                db.SaveChanges();
                isadmin = (bool)usr.isadmin;
                return RedirectToAction(actionName: "showusers");
            }
            //not authorized
            Response.StatusCode = 403;
            return View("NotAuthorized");


        }

    }



}
