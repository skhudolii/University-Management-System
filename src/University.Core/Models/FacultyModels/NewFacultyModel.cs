using System.ComponentModel.DataAnnotations;
using University.Core.ViewModels.Base;

namespace University.Core.ViewModels.FacultyVM
{
    public class NewFacultyModel : BaseModel
    {
        [Display(Name = "Faculty Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} chars")]
        public string Name { get; set; }

        [Display(Name = "Faculty Logo")]
        [Required(ErrorMessage = "{0} is required")]
        public string Logo { get; set; }
    }
}
