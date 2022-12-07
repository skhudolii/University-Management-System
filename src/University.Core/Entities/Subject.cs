using System.ComponentModel.DataAnnotations;

namespace University.Core.Entities
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // n-n relationships
        public List<SubjectAcademicEmployee> SubjectsAcademicEmployees { get; set; }
    }
}
