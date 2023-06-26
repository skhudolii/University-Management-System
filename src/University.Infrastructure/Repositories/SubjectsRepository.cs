using Microsoft.EntityFrameworkCore;
using University.Core.Entities;
using University.Core.Repositories;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories.Base;

namespace University.Infrastructure.Repositories
{
    public class SubjectsRepository : EntityBaseRepository<Subject>, ISubjectsRepository
    {
        private readonly UniversityDbContext _dbContext;

        public SubjectsRepository(UniversityDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Subject> GetSubjectByIdAsync(int id)
        {
            var subjectDetails = await _dbContext.Subjects
                .Include(f => f.Faculty)
                .FirstOrDefaultAsync(n => n.Id == id);

            return subjectDetails;
        }
    }
}
