using University.Core.Entities;
using University.Core.Repositories;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories.Base;

namespace University.Infrastructure.Repositories
{
    public class SubjectsRepository : EntityBaseRepository<Subject>, ISubjectsRepository
    {
        public SubjectsRepository(UniversityDbContext context) : base(context)
        {
        }
    }
}
