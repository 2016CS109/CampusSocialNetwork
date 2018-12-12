using Campus_Social_Network.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Campus_Social_Network.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddStudent()
        {
            var classes = db.Classes.ToList();
            var viewModel = new AddStudentViewModel
            {
                allClasses = classes.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddStudent(AddStudentViewModel model)
        {
            if (IsRegistrationNumberExists(model.registrationNumber))
            {
                ModelState.AddModelError("registrationNumber", "This registration number already exists.");
                model.allClasses = db.Classes.ToList();
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, firstName = model.firstName,
                    lastName = model.lastName, dateOfBirth = model.dateOfBirth, Email = model.Email,
                    registrationNumber = model.registrationNumber, cnic = model.cnic,
                    PhoneNumber = model.PhoneNumber, classId = model.classId };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "Student");
                    deleteUserImage(user.registrationNumber);
                    string fileName = user.registrationNumber + DateTime.Now.ToString("_MMddyyyy_HHmmss") + Path.GetExtension(model.UserImage.FileName);

                    //Set the Image File Path.
                    string filePath = "~/UserImages/" + fileName;

                    //Save the Image File in Folder.
                    model.UserImage.SaveAs(Server.MapPath(filePath));
                    var addedUser = db.Users.Single(c => c.UserName == user.UserName);
                    addedUser.imagePath = filePath;
                    db.SaveChanges();
                    TempData["Msg"] = "Successfully Added";
                    return RedirectToAction("AddStudent");
                }
                AddErrors(result);
            }
            // If we got this far, something failed, redisplay form
            model.allClasses = db.Classes.ToList();
            return View(model);
        }

        public ActionResult AddTeacher()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTeacher(Teacher item)
        {
            if (IsTeacherExists(item.emailId))
            {
                ModelState.AddModelError("emailId", "This email id already exists.");
                Teacher model = new Teacher
                {
                    emailId = item.emailId
                };
                return View("AddTeacher", model);
            }
            using (db)
            {
                db.Teachers.Add(item);
                db.SaveChanges();
                TempData["Msg"] = "Successfully Added";
                return RedirectToAction("AddTeacher");
            }
        }
       
        public ActionResult AddClass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddClass(Class item)
        {
            if(IsClassExists(item.name))
            {
                ModelState.AddModelError("name", "This name already exists.");
                Class model = new Class
                {
                    name = item.name
                };
                return View("AddClass", model);
            }
            using (db)
            {
                db.Classes.Add(item);
                db.SaveChanges();
                TempData["Msg"] = "Successfully Added";
                return RedirectToAction("AddClass");
            }
        }

        public ActionResult AllStudents() {
            List<ApplicationUser> allStudents = db.Users.ToList();
            return View(allStudents);
        }

        public ActionResult AllTeachers()
        {
            List<Teacher> allTeachers = db.Teachers.ToList();
            return View(allTeachers);
        }

        public ActionResult AllClasses()
        {
            List<Class> allClasses = db.Classes.ToList();
            return View(allClasses);
        }

        public ActionResult AccountSettings()
        {

            var admin = db.Users.Single(c => c.UserName == User.Identity.Name);
            return View(admin);
        }

        [HttpPost]
        public ActionResult AccountSettings(ApplicationUser model)
        {
            var admin = db.Users.Single(c => c.UserName == User.Identity.Name);
            admin.firstName = model.firstName;
            admin.lastName = model.lastName;
            admin.Email = model.Email;
            admin.PhoneNumber = model.PhoneNumber;
            admin.UserName = model.Email;
            db.SaveChanges();
            TempData["Msg"] = "Changes Saved";
            return View("AccountSettings");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                TempData["Msg"] = "Password Changed Successfully";
                return View("Index");
            }
            AddErrors(result);
            return View(model);
        }
        public ActionResult ChangePicture()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePicture(HttpPostedFileBase userImage)
        {
            var user = db.Users.Single(c => c.UserName == User.Identity.Name);
            deleteUserImage("admin");
            string fileName = "admin" + DateTime.Now.ToString("_MMddyyyy_HHmmss") + Path.GetExtension(userImage.FileName);

            //Set the Image File Path.
            string filePath = "~/UserImages/" + fileName;

            //Save the Image File in Folder.
            userImage.SaveAs(Server.MapPath(filePath));
            user.imagePath = filePath;
            db.SaveChanges();
            return RedirectToAction("ChangePicture");
        }

        private bool IsClassExists(string name)
        {
            List<Class> allClasses = db.Classes.ToList();
            foreach(Class c in allClasses)
            {
                if(c.name == name)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsTeacherExists(string email)
        {
            List<Teacher> allTeachers = db.Teachers.ToList();
            foreach (Teacher t in allTeachers)
            {
                if (t.emailId == email)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsRegistrationNumberExists(string registrationNumber)
        {
            List<ApplicationUser> allStudents = db.Users.ToList();
            foreach (ApplicationUser s in allStudents)
            {
                if (s.registrationNumber == registrationNumber)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsStudentEmailExists(string email)
        {
            List<ApplicationUser> allStudents = db.Users.ToList();
            foreach (ApplicationUser s in allStudents)
            {
                if (s.Email == email)
                {
                    return true;
                }
            }
            return false;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private void deleteUserImage(string name)
        {
            var folderPath = Server.MapPath("~/UserImages");
            DirectoryInfo folderInfo = new DirectoryInfo(folderPath);
            foreach (FileInfo file in folderInfo.GetFiles())
            {
                if (file.Name.Contains(name))
                {
                    file.Delete();
                }
            }
        }
    }
}