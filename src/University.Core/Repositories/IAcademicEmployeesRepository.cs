using University.Core.Entities;

namespace University.Core.Repositories
{
    public interface IAcademicEmployeesRepository
    {
        Task<IEnumerable<AcademicEmployee>> GetAllAsync();
        Task<AcademicEmployee> GetByIdAsync(int id);
        Task AddAsync(AcademicEmployee academicEmployee);
        AcademicEmployee Update(int id, AcademicEmployee academicEmployee);
        void Delete(int id);
    }
}
