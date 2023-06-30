using University.Core.Entities;
using University.Core.Repositories.Base;
using University.Core.ViewModels.SubjectVM;

namespace University.Core.Repositories
{
    public interface ISubjectsRepository : IEntityBaseRepository<Subject>
    {
        Task<Subject> GetSubjectWithFacultyByIdAsync(int id);
        Task<NewSubjectDropdownsVM> GetNewSubjectDropdownsValuesAsync();
    }
}
