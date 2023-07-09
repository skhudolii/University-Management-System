using University.Core.Entities;

namespace University.Core.ViewModels.AcademicEmployeeVM
{
    public class NewAcademicEmployeeDropdownsVM
    {
        public List<Faculty> Faculties { get; set; }

        public NewAcademicEmployeeDropdownsVM()
        {
            Faculties = new List<Faculty>();
        }
    }
}
