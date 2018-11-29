using Campus_Social_Network.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus_Social_Network.Controllers
{
    public class HomeController : Controller
    {
        private CSNDBEntities1 entity = new CSNDBEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel item)
        {
            var UserName = "abc";
            var Password = "abc";
            List<AdminDb> admin_temp_lst = new List<AdminDb>();
            admin_temp_lst = entity.AdminDbs.ToList();
            foreach (AdminDb obj in admin_temp_lst)
            {
                UserName = obj.EmailId;
                Password = obj.Password;
                break;
            }
            if (UserName == item.Email && Password == item.Password)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
    }
}