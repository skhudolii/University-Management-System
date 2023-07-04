using University.Core.Entities;
using University.Core.Repositories.Base;
using University.Core.ViewModels.StudentVM;

namespace University.Core.Repositories
{
    public interface IStudentsRepository : IEntityBaseRepository<Student>
    {
        Task<NewStudentDropdownsVM> GetNewStudentDropdownsValuesAsync();
    }
}
