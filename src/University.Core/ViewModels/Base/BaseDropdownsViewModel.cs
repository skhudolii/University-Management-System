using University.Core.Entities;

namespace University.Core.ViewModels.Base
{
    public class BaseDropdownsViewModel
    {
        public List<Faculty> Faculties { get; set; }

        public BaseDropdownsViewModel()
        {
            Faculties = new List<Faculty>();
        }
    }
}
