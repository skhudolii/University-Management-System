using System.ComponentModel.DataAnnotations;
using University.Core.Enums;
using University.Core.ViewModels.Base;

namespace University.Core.ViewModels.AcademicEmployeeVM
{
    public class NewAcademicEmployeeVM : BaseViewModel
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

        [Display(Name = "Academic Position")]
        [Required(ErrorMessage = "Academic Position is required")]
        public AcademicPosition? AcademicPosition { get; set; }

        [Display(Name = "Faculty")]
        [Required(ErrorMessage = "Faculty is required")]
        public int? FacultyId { get; set; }
    }
}
