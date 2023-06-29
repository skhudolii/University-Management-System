using University.Core.Entities;

namespace University.Core.ViewModels.SubjectVM
{
    public class NewSubjectDropdownsVM
    {
        public List<Faculty> Faculties { get; set; }

        public NewSubjectDropdownsVM()
        {
            Faculties = new List<Faculty>();
        }
    }
}
