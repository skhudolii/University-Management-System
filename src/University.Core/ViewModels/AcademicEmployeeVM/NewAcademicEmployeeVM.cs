using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using University.Core.Enums;

namespace University.Core.ViewModels.AcademicEmployeeVM
{
    public class NewAcademicEmployeeVM
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

        [Display(Name = "Academic Position")]
        [Required(ErrorMessage = "Academic Position is required")]
        public AcademicPosition? AcademicPosition { get; set; }

        [Display(Name = "Faculty")]
        [Required(ErrorMessage = "Faculty is required")]
        public int? FacultyId { get; set; }
    }
}
