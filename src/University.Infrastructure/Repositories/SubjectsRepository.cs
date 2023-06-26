using Microsoft.EntityFrameworkCore;
using University.Core.Entities;
using University.Core.Repositories;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories.Base;

namespace University.Infrastructure.Repositories
{
    public class SubjectsRepository : EntityBaseRepository<Subject>, ISubjectsRepository
    {
        private readonly UniversityDbContext _context;

        public SubjectsRepository(UniversityDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Subject> GetSubjectByIdAsync(int id)
        {
            var facultyDetails = await _context.Subjects
                .Include(f => f.Faculty)
                .FirstOrDefaultAsync(n => n.Id == id);

            return facultyDetails;
        }
    }
}
