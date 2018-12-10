using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Campus_Social_Network.Models
{
    public class Teacher
    {
        [Required]
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabets are allowed.")]
        public string firstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabets are allowed.")]
        public string lastName { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "Length must be 11.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Contact Number must be numeric.")]
        public string contactNumber { get; set; }

        [Required]
        [Key, Column(Order = 1)]
        [EmailAddress]
        public string emailId { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabets are allowed.")]
        public string designation { get; set; }
    }
}