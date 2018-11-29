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
            List<StudentsDb> add_student_temp_lst = new List<StudentsDb>();
            List<StudentsDb> add_student_list = entity.StudentsDbs.ToList();
            foreach (StudentsDb obj in add_student_list)
            {
                add_student_temp_lst.Add(obj);
            }
            return View(add_student_temp_lst);
        }

        public ActionResult AllTeachers()
        {
            List<TeachersDb> add_teacher_temp_lst = new List<TeachersDb>();
            List<TeachersDb> add_teacher_list = entity.TeachersDbs.ToList();
            foreach (TeachersDb obj in add_teacher_list)
            {
                add_teacher_temp_lst.Add(obj);
            }
            return View(add_teacher_temp_lst);
        }

        public ActionResult AllClasses()
        {
            List<ClassesDb> add_class_temp_lst = new List<ClassesDb>();
            List<ClassesDb> add_class_list = entity.ClassesDbs.ToList();
            foreach (ClassesDb obj in add_class_list)
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
        public ActionResult UpdateProfile(AdminDb item)
        {
            using (entity)
            {
                var ProfilePicturePath = "abc";
                var Password = "abc";
                List<AdminDb> admin_list = entity.AdminDbs.ToList();
                foreach (AdminDb obj in admin_list)
                {
                    ProfilePicturePath = obj.ProfilePicturePath;
                    Password = obj.Password;
                    entity.AdminDbs.Remove(obj);
                    entity.SaveChanges();
                }
                item.ProfilePicturePath = ProfilePicturePath;
                item.Password = Password;
                entity.AdminDbs.Add(item);
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
        public ActionResult ChangePassword(AdminDb item)
        {
            using (entity)
            {
                var FirstName = "abc";
                var LastName = "abc";
                var EmailId = "abc";
                var ContactNumber = "abc";
                var ProiflePicturePath = "abc";
                List<AdminDb> admin_temp_list = entity.AdminDbs.ToList();
                foreach (AdminDb obj in admin_temp_list)
                {
                    FirstName = obj.FirstName;
                    LastName = obj.LastName;
                    EmailId = obj.EmailId;
                    ContactNumber = obj.ContactNumber;
                    ProiflePicturePath = obj.ProfilePicturePath;
                    entity.AdminDbs.Remove(obj);
                    entity.SaveChanges();
                }
                item.FirstName = FirstName;
                item.LastName = LastName;
                item.EmailId = EmailId;
                item.ContactNumber = ContactNumber;
                item.ProfilePicturePath = ProiflePicturePath;
                entity.AdminDbs.Add(item);
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
        public ActionResult ChangePicture(AdminDb item)
        {
            using (entity)
            {
                var FirstName = "abc";
                var LastName = "abc";
                var EmailId = "abc";
                var ContactNumber = "abc";
                var Password = "abc";
                List<AdminDb> admin_temp_list = entity.AdminDbs.ToList();
                foreach (AdminDb obj in admin_temp_list)
                {
                    FirstName = obj.FirstName;
                    LastName = obj.LastName;
                    EmailId = obj.EmailId;
                    ContactNumber = obj.ContactNumber;
                    Password = obj.Password;
                    entity.AdminDbs.Remove(obj);
                    entity.SaveChanges();
                }
                item.FirstName = FirstName;
                item.LastName = LastName;
                item.EmailId = EmailId;
                item.ContactNumber = ContactNumber;
                item.Password = Password;
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
                item.ProfilePicturePath = "~/AppFile/AdminProfilePhoto" + fileName;
                item.ProfilePic.SaveAs(Path.Combine(Server.MapPath("~/AppFile/AdminProfilePhoto"), fileName));
                entity.AdminDbs.Add(item);
                entity.SaveChanges();
                var result = "Successfully added";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
