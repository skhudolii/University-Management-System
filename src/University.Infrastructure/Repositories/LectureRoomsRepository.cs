using Microsoft.EntityFrameworkCore;
using University.Core.Entities;
using University.Core.Repositories;
using University.Core.ViewModels.LectureRoomVM;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories.Base;

namespace University.Infrastructure.Repositories
{
    public class LectureRoomsRepository : EntityBaseRepository<LectureRoom>, ILectureRoomsRepository
    {
        private readonly UniversityDbContext _dbContext;

        public LectureRoomsRepository(UniversityDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LectureRoom> GetLectureRoomWithFacultyByIdAsync(int id)
        {
            var LectureRoomDetails = await _dbContext.LectureRooms
                .Include(f => f.Faculty)
                .FirstOrDefaultAsync(n => n.Id == id);

            return LectureRoomDetails; ;
        }

        public async Task<NewLectureRoomDropdownsVM> GetNewLectureRoomDropdownsValuesAsync()
        {
            var lectureRoomDropdowns = new NewLectureRoomDropdownsVM();
            lectureRoomDropdowns.Faculties = await _dbContext.Faculties.OrderBy(n => n.Name).ToListAsync();

            return lectureRoomDropdowns;
        }
    }
}
