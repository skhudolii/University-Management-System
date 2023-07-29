using University.Core.Response.Interfeces;
using University.Core.ViewModels.LectureVM;
using University.Core.ViewModels.StudentVM;

namespace University.Core.Services.Interfaces
{
    public interface ILectureCascadingDropdownsService
    {
        Task<IBaseResponse<NewLectureDropdownsModel>> GetFaculties();
        Task<IBaseResponse<NewLectureDropdownsModel>> GetDependentDropdownsValues();
    }
}
