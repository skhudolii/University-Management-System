using University.Core.Entities;
using University.Core.ViewModels.Base;

namespace University.Core.ViewModels.StudentVM
{
    public class NewStudentDropdownsVM : BaseDropdownsViewModel
    {
        public List<Group> Groups { get; set; }

        public NewStudentDropdownsVM()
        {
            Groups = new List<Group>();
        }
    }
}
