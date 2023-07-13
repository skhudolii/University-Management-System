using System.ComponentModel.DataAnnotations;
using University.Core.ViewModels.Base;

namespace University.Core.ViewModels.LectureRoomVM
{
    public class NewLectureRoomVM : BaseViewModel
    {
        [Display(Name = "Lecture room name")]
        [Required(ErrorMessage = "Lecture room name is required")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Lecture room name must be between 1 and 20 chars")]
        public string Name { get; set; }

        [Display(Name = "Lecture room capacity")]
        [Required(ErrorMessage = "Lecture room capacity is required")]
        [Range(1, 100, ErrorMessage = "Capacity should be from 1 to 100")]
        public int Capacity { get; set; }

        [Display(Name = "Select a faculty")]
        [Required(ErrorMessage = "Faculty is required")]
        public int? FacultyId { get; set; }
    }
}
