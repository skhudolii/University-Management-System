using University.Core.Entities;

namespace University.Core.ViewModels.StudentVM
{
    public class NewStudentDropdownsVM
    {
        public List<Faculty> Faculties { get; set; }
        public List<Group> Groups { get; set; }

        public NewStudentDropdownsVM()
        {
            Faculties = new List<Faculty>();
            Groups = new List<Group>();
        }
    }
}
