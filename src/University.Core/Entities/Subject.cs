﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using University.Core.Entities.Base;

namespace University.Core.Entities
{
    public class Subject : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Subject Name")]
        public string Name { get; set; }

        // n-1 relationships
        public int? FacultyId { get; set; }
        [ForeignKey("FacultyId")]
        public Faculty? Faculty { get; set; }

        // 1-n
        public List<Lecture> Lectures { get; set; }
    }
}
