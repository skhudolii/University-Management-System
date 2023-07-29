using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.FacultyVM;

namespace University.Core.Services.Interfaces
{
    public interface IFacultiesService
    {
        Task<IBaseResponse<IEnumerable<Faculty>>> GetFacultiesList(string searchString);
        Task<IBaseResponse<NewFacultyModel>> GetFacultyById(int id);
        Task<IBaseResponse<Faculty>> GetFacultyWithIncludePropertiesById(int id);
        Task<IBaseResponse<Faculty>> AddNewFaculty(NewFacultyModel model);
        Task<IBaseResponse<Faculty>> UpdateFaculty(NewFacultyModel model);
        Task<IBaseResponse<bool>> DeleteFaculty(int id);
    }
}
