using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Campus_Social_Network.Models
{
    public class PublicNewsFeedViewModel
    {
        [Required(ErrorMessage = "Post can not be empty")]
        public string postText { get; set; }

        [Required]
        public bool isPublic { get; set; }

        [Required]
        public int likes { get; set; }

        [Required]
        public string postedBy { get; set; }

        public int? postedIn { get; set; }

        public DateTime postTime { get; set; }

        public List<Post> publicPosts = new List<Post>();

        public List<PrivacyOption> privacyOptions = new List<PrivacyOption>();

        public ApplicationUser getUserByPost(Post p)
        {
            //var user = new ApplicationUser();
            var user = new ApplicationDbContext().Users.Single(c => c.Id == p.postedBy);
            return user;
        }
    }
}