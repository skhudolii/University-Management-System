using University.Core.Entities;
using University.Web.ViewModels.Base;
using X.PagedList;

namespace University.Web.ViewModels
{
    public class GroupsListViewModel : BaseViewModel
    {
        public string NameSortParm { get; set; }
        public string FacultySortParm { get; set; }
        public IPagedList<Group> PagedGroups { get; set; }
    }
}
