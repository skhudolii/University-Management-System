using University.Core.Entities;
using University.Core.Repositories;
using University.Infrastructure.Data;

namespace University.Infrastructure.Repositories
{
    public class EFFacultyRepository : IFacultyRepository
    {
        private UniversityDbContext _context;

        public EFFacultyRepository(UniversityDbContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Faculty> Faculties => _context.Faculties;
    }
}
