using University.Core.Entities;
using University.Core.Repositories;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories.Base;

namespace University.Infrastructure.Repositories
{
    public class FacultiesRepository : EntityBaseRepository<Faculty>, IFacultiesRepository
    {
        public FacultiesRepository(UniversityDbContext context) : base(context)
        {
        }
    }
}
