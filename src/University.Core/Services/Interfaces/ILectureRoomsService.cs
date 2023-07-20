using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.LectureRoomVM;

namespace University.Core.Services.Interfaces
{
    public interface ILectureRoomsService
    {
        Task<IBaseResponse<IEnumerable<LectureRoom>>> GetSortedLectureRoomsList(string sortOrder);
        Task<IBaseResponse<NewLectureRoomVM>> GetLectureRoomById(int id);
        Task<IBaseResponse<LectureRoom>> GetLectureRoomWithIncludePropertiesById(int id);
        Task<IBaseResponse<NewLectureRoomDropdownsVM>> GetNewLectureRoomDropdownsValues();
        Task<IBaseResponse<LectureRoom>> AddNewLectureRoom(NewLectureRoomVM model);
        Task<IBaseResponse<LectureRoom>> UpdateLectureRoom(NewLectureRoomVM model);
        Task<IBaseResponse<bool>> DeleteLectureRoom(int id);
    }
}
