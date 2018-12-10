using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    
namespace Campus_Social_Network.Models
{
    public class AddStudentViewModel
    {
        
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabets are allowed.")]
        public string firstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabets are allowed.")]
        public string lastName { get; set; }

        [Range(typeof(DateTime), "1/1/1900", "1/1/2099")]
        public DateTime? dateOfBirth { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{4}-(([A-Z]{3})|([A-Z]{2}))-[0-9]{3}$", ErrorMessage = "Registration Number must be in the format 'dddd-XXX-ddd or dddd-XX-ddd' ")]
        public string registrationNumber { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Length must be 13.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "CNIC Number must be numeric.")]
        public string cnic { get; set; }

        [Required]
        public int classId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "Length must be 11.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Phone Number must be numeric.")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IEnumerable<Class> allClasses { get; set; }

        public HttpPostedFileBase UserImage { get; set; }
    }
}