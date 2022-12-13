using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Core.Entities
{
    public class Lecture
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime LectureDate { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

        // n-1 relationships
        public int ScheduleId { get; set; }
        [ForeignKey("SheduleId")]
        public Schedule Schedule { get; set; }

        // 1-1 relationships
        public Subject Subject { get; set; }
        public AcademicEmployee Teacher { get; set; }
        public LectureRoom LectureRoom { get; set; }

        // n-n relationships
        public List<LectureGroup> LecturesGroups { get; set; }
    }
}
