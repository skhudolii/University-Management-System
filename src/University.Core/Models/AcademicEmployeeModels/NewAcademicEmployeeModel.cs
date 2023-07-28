using System.ComponentModel.DataAnnotations;
using University.Core.Enums;
using University.Core.ViewModels.Base;

namespace University.Core.ViewModels.AcademicEmployeeVM
{
    public class NewAcademicEmployeeModel : BaseModel
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} chars")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} chars")]
        public string LastName { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        public string Email { get; set; }

        [Display(Name = "Profile picture URL")]
        [Required(ErrorMessage = "{0} is required")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = "Academic Position")]
        [Required(ErrorMessage = "{0} is required")]
        public AcademicPosition? AcademicPosition { get; set; }

        [Display(Name = "Faculty")]
        [Required(ErrorMessage = "{0} is required")]
        public int? FacultyId { get; set; }
    }
}
