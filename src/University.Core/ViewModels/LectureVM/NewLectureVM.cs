﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace University.Core.ViewModels.LectureVM
{
    public class NewLectureVM
    {
        public int Id { get; set; }

        [Display(Name = "Lecture date")]
        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:ddd, dd MMM yy}")]
        public DateTime LectureDate { get; set; }

        [Display(Name = "Lecture start time")]
        [Required(ErrorMessage = "Start time is required")]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "Lecture end time")]
        [Required(ErrorMessage = "End time is required")]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

        [Display(Name = "Select a faculty")]
        [Required(ErrorMessage = "Faculty is required")]
        public int FacultyId { get; set; }

        [Display(Name = "Select a subject")]
        [Required(ErrorMessage = "Subject is required")]
        public int SubjectId { get; set; }

        [Display(Name = "Select a lecture room")]
        [Required(ErrorMessage = "Lecture room is required")]
        public int LectureRoomId { get; set; }

        [Display(Name = "Select a teacher")]
        [Required(ErrorMessage = "Teacher is required")]
        public int AcademicEmployeeId { get; set; }
 
        [Display(Name = "Select group(s)")]
        [Required(ErrorMessage = "Lecture group(s) is required")]
        public List<int> GroupIds { get; set; }
    }
}