using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using University.Core.ViewModels.Base;
using University.Core.Attributes;

namespace University.Core.ViewModels.LectureVM
{
    public class NewLectureVM : BaseViewModel
    {
        [Display(Name = "Lecture date")]        
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:ddd, dd MMM yy}")]
        [Required(ErrorMessage = "{0} is required")]
        [LectureDate(ErrorMessage = "{0} cannot be before today")]
        public DateTime LectureDate { get; set; }

        [Display(Name = "Start time")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "{0} is required")]
        [StartTime(ErrorMessage = "{0} cannot be before the current time for today's lecture")]        
        public TimeSpan StartTime { get; set; }

        [Display(Name = "End time")]        
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "{0} is required")]
        [EndTime(StartTimePropertyName = "StartTime", ErrorMessage = "{0} must be greater than start time")]
        public TimeSpan EndTime { get; set; }

        [Display(Name = "Faculty")]
        [Required(ErrorMessage = "{0} is required")]
        public int? FacultyId { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "{0} is required")]
        public int? SubjectId { get; set; }

        [Display(Name = "Lecture room")]
        [Required(ErrorMessage = "{0} is required")]
        public int? LectureRoomId { get; set; }

        [Display(Name = "Teacher")]
        [Required(ErrorMessage = "{0} is required")]
        public int? AcademicEmployeeId { get; set; }
 
        [Display(Name = "Group(s) - Ctrl and click to multiple choose")]
        [Required(ErrorMessage = "At least one group is required")]
        public List<int> GroupIds { get; set; }
    }
}
