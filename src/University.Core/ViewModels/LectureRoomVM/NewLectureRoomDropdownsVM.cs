using University.Core.Entities;

namespace University.Core.ViewModels.LectureRoomVM
{
    public class NewLectureRoomDropdownsVM
    {
        public List<Faculty> Faculties { get; set; }

        public NewLectureRoomDropdownsVM()
        {
            Faculties = new List<Faculty>();
        }
    }
}
