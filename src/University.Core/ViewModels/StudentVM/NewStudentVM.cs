using System.ComponentModel.DataAnnotations;
using University.Core.ViewModels.Base;

namespace University.Core.ViewModels.StudentVM
{
    public class NewStudentVM : BaseViewModel
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name is required")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "First name must be between 1 and 25 chars")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 25 chars")]
        public string LastName { get; set; }

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
