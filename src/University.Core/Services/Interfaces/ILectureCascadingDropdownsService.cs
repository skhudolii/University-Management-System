using University.Core.Response.Interfeces;
using University.Core.ViewModels.LectureVM;
using University.Core.ViewModels.StudentVM;

namespace University.Core.Services.Interfaces
{
    public interface ILectureCascadingDropdownsService
    {
        Task<IBaseResponse<NewLectureDropdownsVM>> GetFaculties();
        Task<IBaseResponse<NewLectureDropdownsVM>> GetDependentDropdownsValues();
    }
}
