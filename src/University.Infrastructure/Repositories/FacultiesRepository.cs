using Microsoft.EntityFrameworkCore;
using University.Core.Entities;
using University.Core.Repositories;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories.Base;

namespace University.Infrastructure.Repositories
{
    public class FacultiesRepository : EntityBaseRepository<Faculty>, IFacultiesRepository
    {
        private readonly UniversityDbContext _context;
        public FacultiesRepository(UniversityDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Faculty> GetFacultyByIdAsync(int id)
        {
            var facultyDetails = await _context.Faculties
                .Include(a => a.AcademicEmployees)
                .Include(g => g.Groups)
                .Include(l => l.Lectures)
                .Include(lr => lr.LectureRooms)
                .Include(s => s.Subjects)
                .FirstOrDefaultAsync(n => n.Id == id);

            return facultyDetails;
        }
    }
}
