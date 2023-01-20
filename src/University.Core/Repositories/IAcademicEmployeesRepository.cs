using University.Core.Entities;

namespace University.Core.Repositories
{
    public interface IAcademicEmployeesRepository
    {
        Task<IEnumerable<AcademicEmployee>> GetAll();
        AcademicEmployee GetById(int id);
        void Add(AcademicEmployee academicEmployee);
        AcademicEmployee Update(int id, AcademicEmployee academicEmployee);
        void Delete(int id);
    }
}
