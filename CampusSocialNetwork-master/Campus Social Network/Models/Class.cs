using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Campus_Social_Network.Models
{
    public class Class
    {
        [Required]
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Key, Column(Order = 1)]
        [RegularExpression(@"^[0-9]{4}-(([A-Z]{3})|([A-Z]{2}))-[A-Z]{1}$", ErrorMessage = "Class Name must be in the format 'dddd-XXX-X or dddd-XX-X' ")]
        public string name { get; set; }

        [Required]
        public int capacity { get; set; }

        public int numberOfStudentsRegistered()
        {
            var db = new ApplicationDbContext();
            var list = db.Users.ToList();
            int studentsRegistered = 0;
            foreach (ApplicationUser u in list)
            {
                if (u.classId == id)
                {
                    studentsRegistered++;
                }
            }
            return studentsRegistered;
        }
    }
}