using University.Core.Entities;

namespace University.Core.Repositories
{
    public interface IAcademicEmployeesRepository
    {
        Task<IEnumerable<AcademicEmployee>> GetAllAsync();
        Task<AcademicEmployee> GetByIdAsync(int id);
        Task AddAsync(AcademicEmployee academicEmployee);
        Task<AcademicEmployee> UpdateAsync(int id, AcademicEmployee newAcademicEmployee);
        void Delete(int id);
    }
}
