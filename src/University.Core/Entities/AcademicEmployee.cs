using System.ComponentModel.DataAnnotations.Schema;
using University.Core.Entities.Base;
using University.Core.Enums;

namespace University.Core.Entities
{
    public class AcademicEmployee : Person
    {
        public AcademicPosition AcademicPosition { get; set; }

        // n-1 relationships
        public int FacultyId { get; set; }
        [ForeignKey("FacultyId")]
        public Faculty Faculty { get; set; }

        // 1-n
        public List<Lecture> Lectures { get; set; }

        // n-n relationships
        public List<SubjectAcademicEmployee> SubjectsAcademicEmployees { get; set; }        
    }
}
