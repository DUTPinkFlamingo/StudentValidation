using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentValidation.Models
{
    public class StudentDetails
    {

        [Key]
        [Required(ErrorMessage = "Student Number Required")]
        [MinLength(8)]
        [MaxLength(8)]
        public string studentNumber { get; set; }


        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        [StringLength(160)]
        public string firstName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("Surname ")]
        public string surname { get; set; }


        [Required(ErrorMessage = " Email Address is required")]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string email { get; set; }


        [Required(ErrorMessage = "Tel number is required")]
        [Display(Name = "Home number")]
        [Phone]
        public string tel { get; set; }


        [Required(ErrorMessage = "Phone is required")]
        [Display(Name = "Cell number")]
        [Phone]
        public string cell { get; set; }

        [Required(ErrorMessage = "Student status is required")]
        [Display(Name = "Is Active")]

        public bool isActive { get; set; }

    }
}