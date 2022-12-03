using System.ComponentModel.DataAnnotations;

namespace University.Core.Entities
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
