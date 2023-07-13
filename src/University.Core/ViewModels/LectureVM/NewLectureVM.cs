using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using University.Core.ViewModels.Base;

namespace University.Core.ViewModels.LectureVM
{
    public class NewLectureVM : BaseViewModel
    {
        [Display(Name = "Lecture date")]
        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:ddd, dd MMM yy}")]
        public DateTime LectureDate { get; set; }

        [Display(Name = "Start time")]
        [Required(ErrorMessage = "Start time is required")]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "End time")]
        [Required(ErrorMessage = "End time is required")]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

        [Display(Name = "Faculty")]
        [Required(ErrorMessage = "Faculty is required")]
        public int? FacultyId { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "Subject is required")]
        public int? SubjectId { get; set; }

        [Display(Name = "Lecture room")]
        [Required(ErrorMessage = "Lecture room is required")]
        public int? LectureRoomId { get; set; }

        [Display(Name = "Teacher")]
        [Required(ErrorMessage = "Teacher is required")]
        public int? AcademicEmployeeId { get; set; }
 
        [Display(Name = "Group(s) - Ctrl and click to multiple choose")]
        [Required(ErrorMessage = "At least one group is required")]
        public List<int> GroupIds { get; set; }
    }
}
