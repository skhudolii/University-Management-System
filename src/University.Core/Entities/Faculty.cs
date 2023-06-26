using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using University.Core.Entities.Base;

namespace University.Core.Entities
{
    public class Faculty : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Faculty")]
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
