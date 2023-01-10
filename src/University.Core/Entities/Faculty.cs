using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Core.Entities
{
    public class Faculty
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }

        // 1-n relationships
        public List<Lecture> Lectures { get; set; }
        public List<AcademicEmployee> AcademicEmployees { get; set; }
        public List<Group> Groups { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<LectureRoom> LectureRooms { get;set; }
    }
}
