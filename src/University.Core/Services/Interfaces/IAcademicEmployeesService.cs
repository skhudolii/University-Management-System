using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.AcademicEmployeeVM;

namespace University.Core.Services.Interfaces
{
    public interface IAcademicEmployeesService
    {
        Task<IBaseResponse<IEnumerable<AcademicEmployee>>> GetSortedAcademicEmployeesList(string sortOrder);
        Task<IBaseResponse<NewAcademicEmployeeVM>> GetAcademicEmployeeById(int id);
        Task<IBaseResponse<AcademicEmployee>> GetAcademicEmployeeWithIncludePropertiesById(int id);
        Task<IBaseResponse<NewAcademicEmployeeDropdownsVM>> GetNewAcademicEmployeeDropdownsValues();
        Task<IBaseResponse<AcademicEmployee>> AddNewAcademicEmployee(NewAcademicEmployeeVM model);
        Task<IBaseResponse<AcademicEmployee>> UpdateAcademicEmployee(NewAcademicEmployeeVM model);
        Task<IBaseResponse<bool>> DeleteAcademicEmployee(int id);
    }
}
