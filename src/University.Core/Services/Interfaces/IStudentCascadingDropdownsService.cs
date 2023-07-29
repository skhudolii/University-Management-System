using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.StudentVM;

namespace University.Core.Services.Interfaces
{
    public interface IStudentCascadingDropdownsService
    {
        Task<IBaseResponse<NewStudentDropdownsModel>> GetFaculties();
        Task<IBaseResponse<NewStudentDropdownsModel>> GetDependentGroups();
    }
}
