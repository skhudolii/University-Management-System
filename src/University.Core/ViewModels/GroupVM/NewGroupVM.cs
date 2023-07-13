using System.ComponentModel.DataAnnotations;
using University.Core.ViewModels.Base;

namespace University.Core.ViewModels.GroupVM
{
    public class NewGroupVM : BaseViewModel
    {
        [Display(Name = "Group name")]
        [Required(ErrorMessage = "Group name is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Group name must be between 2 and 30 chars")]
        public string Name { get; set; }

        [Display(Name = "Select a faculty")]
        [Required(ErrorMessage = "Faculty is required")]
        public int? FacultyId { get; set; }
    }
}
