﻿using System.ComponentModel.DataAnnotations;

namespace University.Core.Entities
{
    public class Person
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
