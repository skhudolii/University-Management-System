using System.ComponentModel.DataAnnotations;

namespace University.Core.Entities
{
    public class Lecture
    {
        [Key]
        public int Id { get; set; }
        public DateTime LectureDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
