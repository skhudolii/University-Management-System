using System.ComponentModel.DataAnnotations.Schema;
using University.Core.Enums;

namespace University.Core.Entities
{
    public class AcademicEmployee : Person
    {
        public AcademicPosition AcademicPosition { get; set; }

        // n-n relationships
        public List<SubjectAcademicEmployee> SubjectsAcademicEmployees { get; set; }

        // Faculty
        public int FacultyId { get; set; }
        [ForeignKey("FacultyId")]
        public Faculty Faculty { get;set; }        
    }
}
