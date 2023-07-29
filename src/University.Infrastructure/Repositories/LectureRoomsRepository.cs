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
        public LectureRoomsRepository(UniversityDbContext context) : base(context)
        {
        }
    }
}
