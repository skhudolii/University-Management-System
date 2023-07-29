using System.ComponentModel.DataAnnotations;

namespace University.Core.Entities.Base
{
    public abstract class Person : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "First name must be between 1 and 25 chars")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Lastt Name is required")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 25 chars")]
        public string LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string ProfilePictureURL { get; set; }

    }
}
