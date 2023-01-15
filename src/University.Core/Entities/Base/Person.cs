using System.ComponentModel.DataAnnotations;

namespace University.Core.Entities.Base
{
    public abstract class Person
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Profile Picture")]
        public string ProfilePictureURL { get; set; }

    }
}
