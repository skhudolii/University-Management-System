using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.SubjectVM;

namespace University.Core.Services.Interfaces
{
    public interface ISubjectsService
    {
        Task<IBaseResponse<IEnumerable<Subject>>> GetSubjectsList();
        Task<IBaseResponse<NewSubjectVM>> GetSubjectById(int id);
        Task<IBaseResponse<Subject>> GetSubjectWithFacultyById(int id);
        Task<IBaseResponse<NewSubjectDropdownsVM>> GetNewSubjectDropdownsValues();
        Task<IBaseResponse<Subject>> AddNewSubject(NewSubjectVM model);
        Task<IBaseResponse<Subject>> UpdateSubject(NewSubjectVM model);
        Task<IBaseResponse<bool>> DeleteSubject(int id);
    }
}
