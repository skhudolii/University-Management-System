using System.ComponentModel.DataAnnotations;

namespace University.Core.Entities.Base
{
    public abstract class Person
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string ProfilePictureURL { get; set; }

    }
}
