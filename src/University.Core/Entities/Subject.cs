using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Core.Entities
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Subject Name")]
        public string Name { get; set; }

        // n-1 relationships
        public int FacultyId { get; set; }
        [ForeignKey("FacultyId")]
        public Faculty Faculty { get; set; }

        // 1-n
        public List<Lecture> Lectures { get; set; }

        // n-n relationships
        public List<SubjectAcademicEmployee> SubjectsAcademicEmployees { get; set; }
        public List<SubjectGroup> SubjectsGroups { get; set; }
    }
}
