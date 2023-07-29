using University.Core.Entities;
using University.Web.ViewModels.Base;
using X.PagedList;

namespace University.Web.ViewModels
{
    public class AcademicEmployeesListViewModel : BaseViewModel
    {
        public string LastNameSortParm { get; set; }
        public string AcademicPositionSortParm { get; set; }
        public string FacultySortParm { get; set; }
        public IPagedList<AcademicEmployee> PagedAcademicEmployees { get; set; }
    }
}
