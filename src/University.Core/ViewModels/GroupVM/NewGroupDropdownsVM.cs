using University.Core.Entities;

namespace University.Core.ViewModels.GroupVM
{
    public class NewGroupDropdownsVM
    {
        public List<Faculty> Faculties { get; set; }

        public NewGroupDropdownsVM()
        {
            Faculties = new List<Faculty>();
        }
    }
}
