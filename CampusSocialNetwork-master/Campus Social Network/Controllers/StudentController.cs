using Campus_Social_Network.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

namespace Campus_Social_Network.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public StudentController()
        {
        }

        public StudentController(ApplicationUserManager userManager)
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
        // GET: Student
        public ActionResult Newsfeed()
        {
            var viewModel = new PublicNewsFeedViewModel();
            PrivacyOption _private = new PrivacyOption();
            PrivacyOption _public = new PrivacyOption();
            _private.title = "Classmates";
            _private.isPublic = false;
            _public.title = "Everyone";
            _public.isPublic = true;
            viewModel.privacyOptions.Add(_public);
            viewModel.privacyOptions.Add(_private);
            foreach(Post p in db.Posts.ToList())
            {
                if(p.isPublic)
                {
                    viewModel.publicPosts.Add(p);
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddPost(PublicNewsFeedViewModel model)
        {
            var currentUser = db.Users.Single(c => c.UserName == User.Identity.Name);
            Post post = new Post();
            post.isPublic = model.isPublic;
            post.likes = 0;
            post.postedBy = currentUser.Id;
            post.postText = model.postText;
            post.postTime = DateTime.Now;
            if(!post.isPublic)
            {
                post.postedIn = currentUser.classId;
            }
            db.Posts.Add(post);
            db.SaveChanges();
            TempData["Msg"] = "Post Added";
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult ClassFeed()
        {
            var currentUser = db.Users.Single(c => c.UserName == User.Identity.Name);
            var viewModel = new PublicNewsFeedViewModel();
            PrivacyOption _private = new PrivacyOption();
            PrivacyOption _public = new PrivacyOption();
            _private.title = "Classmates";
            _private.isPublic = false;
            _public.title = "Everyone";
            _public.isPublic = true;
            viewModel.privacyOptions.Add(_public);
            viewModel.privacyOptions.Add(_private);
            foreach (Post p in db.Posts.ToList())
            {
                if ((!p.isPublic) && (p.postedIn == currentUser.classId))
                {
                    viewModel.publicPosts.Add(p);
                }
            }
            return View(viewModel);

        }
        public ActionResult ViewProfile(string id)
        {
            var viewModel = new PublicNewsFeedViewModel();
            PrivacyOption _private = new PrivacyOption();
            PrivacyOption _public = new PrivacyOption();
            var currentUser = db.Users.Single(c => c.UserName == User.Identity.Name);
            _private.title = "Classmates";
            _private.isPublic = false;
            _public.title = "Everyone";
            _public.isPublic = true;
            viewModel.privacyOptions.Add(_public);
            viewModel.privacyOptions.Add(_private);
            foreach (Post p in db.Posts.ToList())
            {
                if ((p.isPublic) && (p.postedBy == id))
                {
                    viewModel.publicPosts.Add(p);
                }
            }
            return View(viewModel);
        }

        public ActionResult ClassFeedByProfile(string id)
        {
            var viewModel = new PublicNewsFeedViewModel();
            PrivacyOption _private = new PrivacyOption();
            PrivacyOption _public = new PrivacyOption();
            var currentUser = db.Users.Single(c => c.UserName == User.Identity.Name);
            _private.title = "Classmates";
            _private.isPublic = false;
            _public.title = "Everyone";
            _public.isPublic = true;
            viewModel.privacyOptions.Add(_public);
            viewModel.privacyOptions.Add(_private);
            foreach (Post p in db.Posts.ToList())
            {
                if ((!p.isPublic) && (p.postedBy == id) && (p.postedIn == currentUser.classId))
                {
                    viewModel.publicPosts.Add(p);
                }
            }
            return View(viewModel);
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
            var student = db.Users.Single(c => c.UserName == User.Identity.Name);
            student.dateOfBirth = student.dateOfBirth;
            return View(student);
        }

        [HttpPost]
        public ActionResult AccountSettings(ApplicationUser model)
        {
            var student = db.Users.Single(c => c.UserName == User.Identity.Name);
            student.firstName = model.firstName;
            student.lastName = model.lastName;
            student.dateOfBirth = model.dateOfBirth;
            student.cnic = model.cnic;
            student.Email = model.Email;
            student.PhoneNumber = model.PhoneNumber;
            student.UserName = model.Email;
            db.SaveChanges();
            TempData["Msg"] = "Changes Saved";
            return View("AccountSettings");
        }
        
        public ActionResult ChangePicture()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePicture(HttpPostedFileBase userImage)
        {
            var user = db.Users.Single(c => c.UserName == User.Identity.Name);
            deleteUserImage(user.registrationNumber);
            string fileName = user.registrationNumber + DateTime.Now.ToString("_MMddyyyy_HHmmss") + Path.GetExtension(userImage.FileName);

            //Set the Image File Path.
            string filePath = "~/UserImages/" + fileName;

            //Save the Image File in Folder.
            userImage.SaveAs(Server.MapPath(filePath));
            user.imagePath = filePath;
            db.SaveChanges();
            return RedirectToAction("ChangePicture");
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
                return View("Newsfeed");
            }
            AddErrors(result);
            return View(model);
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}