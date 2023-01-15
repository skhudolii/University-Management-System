using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Core.Entities
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Group Name")]
        public string Name { get; set; }

        // n-1 relationships
        public int FacultyId { get; set; }
        [ForeignKey("FacultyId")]
        public Faculty Faculty { get; set; }

        // 1-n relationships
        public List<Student> Students { get; set; }

        // n-n relationships
        public List<SubjectGroup> SubjectsGroups { get; set; }
        public List<LectureGroup> LecturesGroups { get; set;}
    }
}
