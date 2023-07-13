using System.ComponentModel.DataAnnotations;
using University.Core.ViewModels.Base;

namespace University.Core.ViewModels.SubjectVM
{
    public class NewSubjectVM : BaseViewModel
    {
        [Display(Name = "Subject name")]
        [Required(ErrorMessage = "Subject name is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Subject name must be between 2 and 30 chars")]
        public string Name { get; set; }

        [Display(Name = "Select a faculty")]
        [Required(ErrorMessage = "Faculty is required")]
        public int? FacultyId { get; set; }
    }
}
