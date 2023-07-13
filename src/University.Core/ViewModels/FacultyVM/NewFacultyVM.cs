using System.ComponentModel.DataAnnotations;
using University.Core.ViewModels.Base;

namespace University.Core.ViewModels.FacultyVM
{
    public class NewFacultyVM : BaseViewModel
    {
        [Display(Name = "Faculty Name")]
        [Required(ErrorMessage = "Faculty name is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Faculty name must be between 2 and 30 chars")]
        public string Name { get; set; }

        [Display(Name = "Faculty Logo")]
        [Required(ErrorMessage = "Faculty logo is required")]
        public string Logo { get; set; }
    }
}
