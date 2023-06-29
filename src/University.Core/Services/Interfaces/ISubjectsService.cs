using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.SubjectVM;

namespace University.Core.Services.Interfaces
{
    public interface ISubjectsService
    {
        Task<IBaseResponse<IEnumerable<Subject>>> GetSubjectsList();
        Task<IBaseResponse<Subject>> GetSubjectById(int id);
        Task<IBaseResponse<NewSubjectDropdownsVM>> GetNewSubjectDropdownsValues();
        Task<IBaseResponse<Subject>> AddNewSubject(NewSubjectVM model);
    }
}
