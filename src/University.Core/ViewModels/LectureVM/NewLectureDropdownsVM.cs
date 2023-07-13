using University.Core.Entities;
using University.Core.ViewModels.Base;

namespace University.Core.ViewModels.LectureVM
{
    public class NewLectureDropdownsVM : BaseDropdownsViewModel
    {
        public List<Subject> Subjects { get; set; }
        public List<LectureRoom> LectureRooms { get; set; }
        public List<AcademicEmployee> AcademicEmployees { get; set; }
        public List<Group> Groups { get; set; }

        public NewLectureDropdownsVM()
        { 
            Subjects = new List<Subject>();
            LectureRooms = new List<LectureRoom>();
            AcademicEmployees = new List<AcademicEmployee>();
            Groups = new List<Group>();
        }
    }
}
