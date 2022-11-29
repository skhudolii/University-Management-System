using University.Core.Entities;

namespace University.Core.Repositories
{
    public interface IFacultyRepository
    {
        IQueryable<Faculty> Faculties { get; }
    }
}
