using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.LectureVM;

namespace University.Core.Services.Interfaces
{
    public interface ILecturesService
    {
        Task<IBaseResponse<IEnumerable<Lecture>>> GetSortedLecturesList(string sortOrder, string searchString);
        Task<IBaseResponse<NewLectureModel>> GetLectureById(int id);
        Task<IBaseResponse<Lecture>> GetLectureWithIncludePropertiesById(int id);
        Task<IBaseResponse<Lecture>> AddNewLecture(NewLectureModel newLectureVM);
        Task<IBaseResponse<Lecture>> UpdateLecture(NewLectureModel newLectureVM);
        Task<IBaseResponse<bool>> DeleteLecture(int id);
    }
}
