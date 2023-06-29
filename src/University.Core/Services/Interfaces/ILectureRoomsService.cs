using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.LectureRoomVM;

namespace University.Core.Services.Interfaces
{
    public interface ILectureRoomsService
    {
        Task<IBaseResponse<IEnumerable<LectureRoom>>> GetLectureRoomsList();
        Task<IBaseResponse<LectureRoom>> GetLectureRoomById(int id);
        Task<IBaseResponse<NewLectureRoomDropdownsVM>> GetNewLectureRoomDropdownsValues();
        Task<IBaseResponse<LectureRoom>> AddNewLectureRoom(NewLectureRoomVM model);
    }
}
