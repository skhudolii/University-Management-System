using University.Core.Entities;
using University.Core.ViewModels.Base;

namespace University.Core.ViewModels.StudentVM
{
    public class NewStudentDropdownsModel : BaseDropdownsModel
    {
        public List<Group> Groups { get; set; }

        public NewStudentDropdownsModel()
        {
            Groups = new List<Group>();
        }
    }
}
