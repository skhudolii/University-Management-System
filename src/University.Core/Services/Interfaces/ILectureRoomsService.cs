using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.LectureRoomVM;

namespace University.Core.Services.Interfaces
{
    public interface ILectureRoomsService
    {
        Task<IBaseResponse<IEnumerable<LectureRoom>>> GetSortedLectureRoomsList(string sortOrder, string searchString);
        Task<IBaseResponse<NewLectureRoomModel>> GetLectureRoomById(int id);
        Task<IBaseResponse<LectureRoom>> GetLectureRoomWithIncludePropertiesById(int id);
        Task<IBaseResponse<NewLectureRoomDropdownsModel>> GetNewLectureRoomDropdownsValues();
        Task<IBaseResponse<LectureRoom>> AddNewLectureRoom(NewLectureRoomModel model);
        Task<IBaseResponse<LectureRoom>> UpdateLectureRoom(NewLectureRoomModel model);
        Task<IBaseResponse<bool>> DeleteLectureRoom(int id);
    }
}
