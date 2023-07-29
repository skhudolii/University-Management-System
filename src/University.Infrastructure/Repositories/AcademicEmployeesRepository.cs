using University.Core.Entities;
using University.Core.Repositories;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories.Base;

namespace University.Infrastructure.Repositories
{
    public class AcademicEmployeesRepository : EntityBaseRepository<AcademicEmployee>, IAcademicEmployeesRepository
    {
        public AcademicEmployeesRepository(UniversityDbContext context) : base(context)
        { 
        }
    }
}
