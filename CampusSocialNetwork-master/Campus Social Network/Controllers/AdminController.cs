﻿using Campus_Social_Network.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Campus_Social_Network.Controllers
{
    public class AdminController : Controller
    {
        private CSNDBEntities1 entity = new CSNDBEntities1();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent(StudentsDb item)
        {
            string fileName = Path.GetFileNameWithoutExtension(item.ProfilePic.FileName);
            string extension = Path.GetExtension(item.ProfilePic.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
            item.ImagePath = "~/AppFile/StudentsImagesByAdmin" + fileName;
            item.ProfilePic.SaveAs(Path.Combine(Server.MapPath("~/AppFile/StudentsImagesByAdmin"), fileName));
            using (entity)
            {
                entity.StudentsDbs.Add(item);
                entity.SaveChanges();
                var result = "Successfully Added!";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddTeacher()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTeacher(TeachersDb item)
        {
            string fileName = Path.GetFileNameWithoutExtension(item.ProfilePic.FileName);
            string extension = Path.GetExtension(item.ProfilePic.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
            item.ImagePath = "~/AppFile/TeachersImagesByAdmin" + fileName;
            item.ProfilePic.SaveAs(Path.Combine(Server.MapPath("~/AppFile/TeachersImagesByAdmin"), fileName));
            using (entity)
            {
                entity.TeachersDbs.Add(item);
                entity.SaveChanges();
                var result = "Successfully Added!";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult AddClass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddClass(ClassesDb item)
        {
            using (entity)
            {
                entity.ClassesDbs.Add(item);
                entity.SaveChanges();
                var result = "Successfully Added!";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AllStudents()
        {
            return View();
        }

        public ActionResult AllTeachers()
        {
            return View();
        }

        public ActionResult AllClasses()
        {
            return View();
        }

        public ActionResult UpdateProfile()
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
