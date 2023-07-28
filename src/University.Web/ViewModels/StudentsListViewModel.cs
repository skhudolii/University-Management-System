using University.Core.Entities;
using University.Web.ViewModels.Base;
using X.PagedList;

namespace University.Web.ViewModels
{
    public class StudentsListViewModel : BaseViewModel
    {
        public string LastNameSortParm { get; set; }
        public string FirstNameSortParm { get; set; }
        public string GroupSortParm { get; set; }
        public string FacultySortParm { get; set; }
        public IPagedList<Student> PagedStudents { get; set; }
    }
}
