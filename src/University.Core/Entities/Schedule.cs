using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Core.Entities
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCurrent { get; set; }

        // n-1 relationships
        public int FacultyId { get; set; }
        [ForeignKey("FacultyId")]
        public Faculty Faculty { get; set; }

        // 1-n relationships
        public List<Lecture> Lectures { get; set; }
    }
}
