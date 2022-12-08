using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Core.Entities
{
    public class Lecture
    {
        [Key]
        public int Id { get; set; }
        public DateTime LectureDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        // 1-1 relationships
        public Subject Subject { get; set; }
        public AcademicEmployee Teacher { get; set; }
        public LectureRoom LectureRoom { get; set; }

        // 1-n relationships
        public List<Group> Groups { get; set; }

        // n-1 relationships
        public int ScheduleId { get; set; }
        [ForeignKey("SheduleId")]
        public Schedule Schedule { get; set; }
    }
}
