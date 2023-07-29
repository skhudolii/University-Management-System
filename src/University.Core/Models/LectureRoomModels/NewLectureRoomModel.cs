using System.ComponentModel.DataAnnotations;
using University.Core.ViewModels.Base;

namespace University.Core.ViewModels.LectureRoomVM
{
    public class NewLectureRoomModel : BaseModel
    {
        [Display(Name = "Lecture room name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "{0} must be between {2} and {1} chars")]
        public string Name { get; set; }

        [Display(Name = "Lecture room capacity")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 100, ErrorMessage = "{0} should be from {1} to {2}")]
        public int Capacity { get; set; }

        [Display(Name = "Faculty")]
        [Required(ErrorMessage = "{0} is required")]
        public int? FacultyId { get; set; }
    }
}
