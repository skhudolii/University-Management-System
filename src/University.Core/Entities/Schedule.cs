using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Core.Entities
{
    public class Schedule
    {
        [Key]
        [ForeignKey("Faculty")]
        public int Id { get; set; }
        
        // Faculty
        public Faculty Faculty { get; set; }
    }
}
