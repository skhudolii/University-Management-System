using University.Core.Entities;

namespace University.Core.ViewModels.StudentVM
{
    public class NewStudentDropdownsVM
    {
        public List<Group> Groups { get; set; }

        public NewStudentDropdownsVM()
        {
            Groups = new List<Group>();
        }
    }
}
