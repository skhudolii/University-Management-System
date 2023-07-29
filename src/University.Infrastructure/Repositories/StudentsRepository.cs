using University.Core.Entities;
using University.Core.Repositories;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories.Base;

namespace University.Infrastructure.Repositories
{
    public class StudentsRepository : EntityBaseRepository<Student>, IStudentsRepository
    {
        public StudentsRepository(UniversityDbContext context) : base(context)
        {
        }
    }
}
