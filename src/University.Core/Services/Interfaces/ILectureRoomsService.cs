using University.Core.Entities;
using University.Core.Response.Interfeces;

namespace University.Core.Services.Interfaces
{
    public interface ILectureRoomsService
    {
        Task<IBaseResponse<IEnumerable<LectureRoom>>> GetLectureRoomsList();
    }
}
