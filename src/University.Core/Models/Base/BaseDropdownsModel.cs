using University.Core.Entities;

namespace University.Core.ViewModels.Base
{
    public class BaseDropdownsModel
    {
        public List<Faculty> Faculties { get; set; }

        public BaseDropdownsModel()
        {
            Faculties = new List<Faculty>();
        }
    }
}
