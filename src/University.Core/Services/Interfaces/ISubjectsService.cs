using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.SubjectVM;

namespace University.Core.Services.Interfaces
{
    public interface ISubjectsService
    {
        Task<IBaseResponse<IEnumerable<Subject>>> GetSortedSubjectsList(string sortOrder, string searchString);
        Task<IBaseResponse<NewSubjectModel>> GetSubjectById(int id);
        Task<IBaseResponse<Subject>> GetSubjectWithIncludePropertiesById(int id);
        Task<IBaseResponse<NewSubjectDropdownsModel>> GetNewSubjectDropdownsValues();
        Task<IBaseResponse<Subject>> AddNewSubject(NewSubjectModel model);
        Task<IBaseResponse<Subject>> UpdateSubject(NewSubjectModel model);
        Task<IBaseResponse<bool>> DeleteSubject(int id);
    }
}
