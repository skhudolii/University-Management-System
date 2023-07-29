using University.Core.Entities;
using University.Web.ViewModels.Base;
using X.PagedList;

namespace University.Web.ViewModels
{
    public class LectureRoomsListViewModel : BaseViewModel
    {
        public string NameSortParm { get; set; }
        public string CapacitySortParm { get; set; }
        public string FacultySortParm { get; set; }
        public IPagedList<LectureRoom> PagedLectureRooms { get; set; }
    }
}
