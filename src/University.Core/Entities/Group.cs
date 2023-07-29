using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using University.Core.Entities.Base;

namespace University.Core.Entities
{
    public class Group : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Group Name")]
        [Required(ErrorMessage = "Group Name is required")]
        public string Name { get; set; }

        // n-1 relationships
        public int? FacultyId { get; set; }
        [ForeignKey("FacultyId")]
        public Faculty? Faculty { get; set; }

        // 1-n relationships
        public List<Student> Students { get; set; } = new List<Student>();

        // n-n relationships
        public List<LectureGroup> LecturesGroups { get; set;} = new List<LectureGroup>();
    }
}
