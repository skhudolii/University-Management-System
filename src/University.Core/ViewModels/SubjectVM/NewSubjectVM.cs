using System.ComponentModel.DataAnnotations;

namespace University.Core.ViewModels.SubjectVM
{
    public class NewSubjectVM
    {
        [Display(Name = "Subject name")]
        [Required(ErrorMessage = "Subject name is required")]
        public string Name { get; set; }

        [Display(Name = "Select a faculty")]
        [Required(ErrorMessage = "Faculty is required")]
        public int FacultyId { get; set; }
    }
}
