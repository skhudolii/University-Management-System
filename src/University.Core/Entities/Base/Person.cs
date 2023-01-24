using System.ComponentModel.DataAnnotations;

namespace University.Core.Entities.Base
{
    public abstract class Person : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name must be between 3 and 50 chars")]
        public string FullName { get; set; }        

        [EmailAddress(ErrorMessage = "Email format is incorrect")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }        

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string ProfilePictureURL { get; set; }

    }
}
