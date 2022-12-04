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

        // 1-0 or 1 relationships
        public Schedule Schedule { get; set; }

        // 1-n relationships
        public List<AcademicEmployee> AcademicEmployees { get; set; }
        public List<Group> Groups { get; set; }
    }
}
