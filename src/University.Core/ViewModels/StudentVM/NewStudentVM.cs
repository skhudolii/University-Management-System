﻿using System.ComponentModel.DataAnnotations;

namespace University.Core.ViewModels.StudentVM
{
    public class NewStudentVM
    {
        public int Id { get; set; }

        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full name must be between 3 and 50 chars")]
        public string FullName { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Display(Name = "Profile picture URL")]
        [Required(ErrorMessage = "Profile picture URL is required")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = "Faculty")]
        [Required(ErrorMessage = "Faculty is required")]
        public int? FacultyId { get; set; } // int? for model validation for <select> element in Web-Views-Students-Create

        [Display(Name = "Group")]
        [Required(ErrorMessage = "Group is required")]
        public int? GroupId { get; set; } // int? for model validation for <select> element in Web-Views-Students-Create
    }
}
