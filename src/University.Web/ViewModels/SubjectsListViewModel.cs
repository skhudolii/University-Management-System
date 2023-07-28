using University.Core.Entities;
using University.Web.ViewModels.Base;
using X.PagedList;

namespace University.Web.ViewModels
{
    public class SubjectsListViewModel : BaseViewModel
    {
        public string NameSortParm { get; set; }
        public string FacultySortParm { get; set; }
        public IPagedList<Subject> PagedSubjects { get; set; }
    }
}
