using System.ComponentModel.DataAnnotations;

namespace University.Core.Entities
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}
