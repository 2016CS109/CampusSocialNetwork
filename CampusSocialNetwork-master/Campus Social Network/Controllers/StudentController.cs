using Campus_Social_Network.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace Campus_Social_Network.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
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
            return RedirectToAction("Newsfeed");
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