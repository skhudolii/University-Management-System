using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.AcademicEmployeeVM;

namespace University.Core.Services.Interfaces
{
    public interface IAcademicEmployeesService
    {
        Task<IBaseResponse<IEnumerable<AcademicEmployee>>> GetSortedAcademicEmployeesList(string sortOrder, string searchString);
        Task<IBaseResponse<NewAcademicEmployeeModel>> GetAcademicEmployeeById(int id);
        Task<IBaseResponse<AcademicEmployee>> GetAcademicEmployeeWithIncludePropertiesById(int id);
        Task<IBaseResponse<NewAcademicEmployeeDropdownsModel>> GetNewAcademicEmployeeDropdownsValues();
        Task<IBaseResponse<AcademicEmployee>> AddNewAcademicEmployee(NewAcademicEmployeeModel model);
        Task<IBaseResponse<AcademicEmployee>> UpdateAcademicEmployee(NewAcademicEmployeeModel model);
        Task<IBaseResponse<bool>> DeleteAcademicEmployee(int id);
    }
}
