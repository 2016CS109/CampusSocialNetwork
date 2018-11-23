using Campus_Social_Network.Models;
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
        private CSNDBEntities entity = new CSNDBEntities();

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
        public ActionResult AddStudent(AddStudent item)
        {
            string fileName = Path.GetFileNameWithoutExtension(item.ProfilePic.FileName);
            string extension = Path.GetExtension(item.ProfilePic.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
            item.ImagePath = "~/AppFile/StudentsImagesByAdmin" + fileName;
            item.ProfilePic.SaveAs(Path.Combine(Server.MapPath("~/AppFile/StudentsImagesByAdmin"), fileName));
            using (entity)
            {
                entity.AddStudents.Add(item);
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
        public ActionResult AddTeacher(AddTeacher item)
        {
            string fileName = Path.GetFileNameWithoutExtension(item.ProfilePic.FileName);
            string extension = Path.GetExtension(item.ProfilePic.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
            item.ImagePath = "~/AppFile/TeachersImagesByAdmin" + fileName;
            item.ProfilePic.SaveAs(Path.Combine(Server.MapPath("~/AppFile/TeachersImagesByAdmin"), fileName));
            using (entity)
            {
                entity.AddTeachers.Add(item);
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
        public ActionResult AddClass(AddClass item)
        {
            using (entity)
            {
                entity.AddClasses.Add(item);
                entity.SaveChanges();
                var result = "Successfully Added!";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AllStudents()
        {
            List<AddStudent> add_student_temp_lst = new List<AddStudent>();
            List<AddStudent> add_student_list = entity.AddStudents.ToList();
            foreach (AddStudent obj in add_student_list)
            {
                add_student_temp_lst.Add(obj);
            }
            return View(add_student_temp_lst);
        }

        public ActionResult AllTeachers()
        {
            List<AddTeacher> add_teacher_temp_lst = new List<AddTeacher>();
            List<AddTeacher> add_teacher_list = entity.AddTeachers.ToList();
            foreach (AddTeacher obj in add_teacher_list)
            {
                add_teacher_temp_lst.Add(obj);
            }
            return View(add_teacher_temp_lst);
        }

        public ActionResult AllClasses()
        {
            List<AddClass> add_class_temp_lst = new List<AddClass>();
            List<AddClass> add_class_list = entity.AddClasses.ToList();
            foreach (AddClass obj in add_class_list)
            {
                add_class_temp_lst.Add(obj);
            }
            return View(add_class_temp_lst);
        }

        public ActionResult UpdateProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateProfile(Admin item)
        {
            using (entity)
            {
                List<Admin> admin_list = entity.Admins.ToList();
                foreach (Admin s in admin_list)
                {
                    entity.Admins.Remove(s);
                    entity.SaveChanges();
                }

                entity.Admins.Add(item);
                entity.SaveChanges();
                var result = "Successfully added";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(AdminPassword item)
        {
            using (entity)
            {
                List<AdminPassword> admin_password_list = entity.AdminPasswords.ToList();
                foreach (AdminPassword obj in admin_password_list)
                {
                    entity.AdminPasswords.Remove(obj);
                    entity.SaveChanges();
                }

                entity.AdminPasswords.Add(item);
                entity.SaveChanges();
                var result = "Successfully added";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ChangePicture()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePicture(AdminPofilePhotoPath item)
        {
            using (entity)
            {
                List<AdminPofilePhotoPath> admin_Profile_Pic_lst = entity.AdminPofilePhotoPaths.ToList();
                foreach (AdminPofilePhotoPath obj in admin_Profile_Pic_lst)
                {
                    entity.AdminPofilePhotoPaths.Remove(obj);
                    entity.SaveChanges();
                }
                var folderPath = Server.MapPath("~/AppFile/AdminProfilePhoto");
                System.IO.DirectoryInfo folderInfo = new DirectoryInfo(folderPath);

                foreach (FileInfo file in folderInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in folderInfo.GetDirectories())
                {
                    dir.Delete(true);
                }
                string fileName = Path.GetFileNameWithoutExtension(item.ProfilePic.FileName);
                string extension = Path.GetExtension(item.ProfilePic.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                item.AdminImagePath = "~/AppFile/AdminProfilePhoto" + fileName;
                item.ProfilePic.SaveAs(Path.Combine(Server.MapPath("~/AppFile/AdminProfilePhoto"), fileName));
                entity.AdminPofilePhotoPaths.Add(item);
                entity.SaveChanges();
                var result = "Successfully added";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }





    }
}
