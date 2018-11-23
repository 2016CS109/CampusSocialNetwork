using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus_Social_Network.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Newsfeed()
        {
            return View();
        }
        public ActionResult StudentProfile()
        {
            return View();
        }
        public ActionResult Messages()
        {
            return View();
        }
        public ActionResult ViewTeachers()
        {
            return View();
        }
        public ActionResult AccountSettings()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        public ActionResult ChangePicture()
        {
            return View();
        }
    }
}