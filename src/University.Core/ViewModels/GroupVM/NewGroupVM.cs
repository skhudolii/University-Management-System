using System.ComponentModel.DataAnnotations;
using University.Core.ViewModels.Base;

namespace University.Core.ViewModels.GroupVM
{
    public class NewGroupVM : BaseViewModel
    {
        [Display(Name = "Group name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} chars")]
        public string Name { get; set; }

        [Display(Name = "Faculty")]
        [Required(ErrorMessage = "{0} is required")]
        public int? FacultyId { get; set; }
    }
}
