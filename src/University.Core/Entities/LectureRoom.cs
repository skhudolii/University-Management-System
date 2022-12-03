using System.ComponentModel.DataAnnotations;

namespace University.Core.Entities
{
    public class LectureRoom
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
    }
}
