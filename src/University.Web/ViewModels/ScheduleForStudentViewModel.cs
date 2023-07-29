using University.Core.Entities;
using University.Web.ViewModels.Base;
using X.PagedList;

namespace University.Web.ViewModels
{
    public class ScheduleForStudentViewModel : BaseViewModel
    {
        public int DaysForward { get; set; }
        public string DateSortParm { get; set; }
        public string SubjectSortParm { get; set; }
        public string LectureRoomSortParm { get; set; }
        public IPagedList<Lecture> PagedLectures { get; set; }
    }
}
