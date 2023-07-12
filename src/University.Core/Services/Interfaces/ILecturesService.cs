using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.LectureVM;

namespace University.Core.Services.Interfaces
{
    public interface ILecturesService
    {
        Task<IBaseResponse<IEnumerable<Lecture>>> GetLecturesList();
        Task<IBaseResponse<Lecture>> GetLectureWithIncludePropertiesById(int id);
        Task<IBaseResponse<Lecture>> AddNewLecture(NewLectureVM newLectureVM);
    }
}
