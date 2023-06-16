using University.Core.Entities;
using University.Core.Repositories.Base;

namespace University.Core.Repositories
{
    public interface IFacultiesRepository : IEntityBaseRepository<Faculty>
    {
        Task<Faculty> GetFacultyByIdAsync(int id);
    }
}
