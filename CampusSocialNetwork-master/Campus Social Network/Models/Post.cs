using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Campus_Social_Network.Models
{
    public class Post
    {
        [Required]
        public int id { get; set; }

        [Required]
        public string postText { get; set; }

        [Required]
        public bool isPublic { get; set; }

        [Required]
        public int likes { get; set; }

        [Required]
        public string postedBy { get; set; }

        public int? postedIn { get; set; }

        public DateTime postTime { get; set; }  
    }
}