using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using University.Core.Entities.Base;

namespace University.Core.Entities
{
    public class Lecture : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [Display(Name = "Lecture Date")]
        [DisplayFormat(DataFormatString = "{0:ddd, dd MMM yy}")]
        public DateTime LectureDate { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }

        // n-1 relationships
        public int? FacultyId { get; set; }
        [ForeignKey("FacultyId")]
        public Faculty? Faculty { get; set; }        
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }
        public int LectureRoomId { get; set; }
        [ForeignKey("LectureRoomId")]
        public LectureRoom LectureRoom { get; set; }
        public int AcademicEmployeeId { get; set; }
        [ForeignKey("AcademicEmployeeId")]
        public AcademicEmployee Teacher { get; set; }        

        // n-n relationships
        public List<LectureGroup> LecturesGroups { get; set; }
    }
}
