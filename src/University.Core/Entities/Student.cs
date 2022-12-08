using System.ComponentModel.DataAnnotations.Schema;

namespace University.Core.Entities
{
    public class Student : Person
    {
        // n-1 relationships
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
    }
}
