using University.Core.Entities;

namespace University.Core.ViewModels.Lecture
{
    public class NewLectureDropdownsVM
    {
        public List<Faculty> Faculties { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<LectureRoom> LectureRooms { get; set; }
        public List<AcademicEmployee> Teachers { get; set; }
        public List<Group> Groups { get; set; }

        public NewLectureDropdownsVM()
        {
            Faculties = new List<Faculty>();
            Subjects = new List<Subject>();
            LectureRooms = new List<LectureRoom>();
            Teachers = new List<AcademicEmployee>();
            Groups = new List<Group>();
        }
    }
}
