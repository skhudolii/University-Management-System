using System.ComponentModel.DataAnnotations;

namespace University.Core.Entities
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
