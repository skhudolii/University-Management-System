using System.ComponentModel.DataAnnotations;

namespace University.Core.Entities
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // n-n relationships
        public List<AcademicEmployee_Subject> AcademicEmployees_Subjects { get; set; }
    }
}
