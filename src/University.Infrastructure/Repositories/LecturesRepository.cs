using Microsoft.EntityFrameworkCore;
using University.Core.Entities;
using University.Core.Repositories;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories.Base;

namespace University.Infrastructure.Repositories
{
    public class LecturesRepository : EntityBaseRepository<Lecture>, ILecturesRepository
    {
        private readonly UniversityDbContext _context;

        public LecturesRepository(UniversityDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Lecture> GetLectureByIdAsync(int id)
        {
            var lectureDetails = await _context.Lectures
                .Include(s => s.Subject)
                .Include(l => l.LectureRoom)
                .Include(a => a.Teacher)
                .Include(f => f.Faculty)
                .Include(lg => lg.LecturesGroups).ThenInclude(g => g.Group)
                .FirstOrDefaultAsync(n => n.Id == id);

            return lectureDetails;
        }

        public async Task<NewLectureDropdowns> GetNewLectureDropdownsValues()
        {
            var responce = new NewLectureDropdowns()
            {
                Faculties = await _context.Faculties.OrderBy(n => n.Name).ToListAsync(),
                Subjects = await _context.Subjects.OrderBy(n => n.Name).ToListAsync(),
                LectureRooms = await _context.LectureRooms.OrderBy(n => n.Name).ToListAsync(),
                Teachers = await _context.AcademicEmployees.OrderBy(n => n.FullName).ToListAsync(),
                Groups = await _context.Groups.OrderBy(n => n.Name).ToListAsync()
            };

            return responce;
        }
    }
}
