namespace University.Core.Entities
{
    public class AcademicEmployee_Subject
    {
        public int AcademicEmployeeId { get; set; }
        public AcademicEmployee AcademicEmployee { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
