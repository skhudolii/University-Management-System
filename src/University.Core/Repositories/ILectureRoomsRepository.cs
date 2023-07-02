using University.Core.Entities;
using University.Core.Repositories.Base;
using University.Core.ViewModels.LectureRoomVM;

namespace University.Core.Repositories
{
    public interface ILectureRoomsRepository : IEntityBaseRepository<LectureRoom>
    {
        Task<LectureRoom> GetLectureRoomWithFacultyByIdAsync(int id);
        Task<NewLectureRoomDropdownsVM> GetNewLectureRoomDropdownsValuesAsync();
    }
}
