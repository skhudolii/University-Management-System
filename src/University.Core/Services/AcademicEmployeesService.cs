using University.Core.Entities;
using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Response;
using University.Core.Response.Interfeces;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.AcademicEmployeeVM;

namespace University.Core.Services
{
    public class AcademicEmployeesService : IAcademicEmployeesService
    {
        private readonly IAcademicEmployeesRepository _academicEmployeesRepository;
        private readonly IFacultiesRepository _facultiesRepository;

        public AcademicEmployeesService(IAcademicEmployeesRepository academicEmployeesRepository,
                                        IFacultiesRepository facultiesRepository)
        {
            _academicEmployeesRepository = academicEmployeesRepository;
            _facultiesRepository = facultiesRepository;
        }

        public async Task<IBaseResponse<AcademicEmployee>> AddNewAcademicEmployee(NewAcademicEmployeeModel model)
        {
            try
            {
                var newAcademicEmployee = new AcademicEmployee()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    ProfilePictureURL = model.ProfilePictureURL,
                    AcademicPosition = (AcademicPosition)model.AcademicPosition,
                    FacultyId = model.FacultyId
                };
                await _academicEmployeesRepository.AddAsync(newAcademicEmployee);

                return new BaseResponse<AcademicEmployee>()
                {
                    Data = newAcademicEmployee,
                    Description = "New academic employee successfully added",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AcademicEmployee>()
                {
                    Description = $"[AcademicEmployeesService.AddNewAcademicEmployee] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteAcademicEmployee(int id)
        {
            try
            {
                var academicEmployee = await _academicEmployeesRepository.GetByIdAsync(id);
                if (academicEmployee == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                await _academicEmployeesRepository.DeleteAsync(id);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "Academic employee successfully deleted",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[[AcademicEmployeesService.DeleteAcademicEmployee] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<NewAcademicEmployeeModel>> GetAcademicEmployeeById(int id)
        {
            try
            {
                var academicEmployeeDetails = await _academicEmployeesRepository.GetByIdAsync(id);
                if (academicEmployeeDetails == null || academicEmployeeDetails.FacultyId == null)
                {
                    return new BaseResponse<NewAcademicEmployeeModel>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                var data = new NewAcademicEmployeeModel()
                {
                    Id = academicEmployeeDetails.Id,
                    FirstName = academicEmployeeDetails.FirstName,
                    LastName = academicEmployeeDetails.LastName,
                    Email = academicEmployeeDetails.Email,
                    ProfilePictureURL = academicEmployeeDetails.ProfilePictureURL,
                    AcademicPosition = academicEmployeeDetails.AcademicPosition,
                    FacultyId = (int)academicEmployeeDetails.FacultyId,
                };

                return new BaseResponse<NewAcademicEmployeeModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewAcademicEmployeeModel>()
                {
                    Description = $"[AcademicEmployeesService.GetAcademicEmployeeById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<AcademicEmployee>>> GetSortedAcademicEmployeesList(string sortOrder, string searchString)
        {
            try
            {
                var academicEmployees = (await _academicEmployeesRepository.GetAllAsync(n => n.Faculty)).
                    Where(f => f.FacultyId != null);

                if (!academicEmployees.Any())
                {
                    return new BaseResponse<IEnumerable<AcademicEmployee>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.OK
                    };
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    academicEmployees = academicEmployees.Where(ae => ae.FirstName.ToLower().Contains(searchString.ToLower())
                                           || ae.LastName.ToLower().Contains(searchString.ToLower()));
                }

                academicEmployees = sortOrder switch
                {
                    "name_desc" => academicEmployees.OrderByDescending(ae => ae.LastName),
                    "AcademicPosition" => academicEmployees.OrderBy(ae => ae.AcademicPosition),
                    "academicPosition_desc" => academicEmployees.OrderByDescending(ae => ae.AcademicPosition),
                    "Faculty" => academicEmployees.OrderBy(ae => ae.Faculty.Name),
                    "faculty_desc" => academicEmployees.OrderByDescending(ae => ae.Faculty.Name),
                    _ => academicEmployees.OrderBy(ae => ae.LastName),
                };
                return new BaseResponse<IEnumerable<AcademicEmployee>>()
                {
                    Data = academicEmployees.ToList(),
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<AcademicEmployee>>()
                {
                    Description = $"[AcademicEmployeesService.GetAcademicEmployeesList] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<AcademicEmployee>> GetAcademicEmployeeWithIncludePropertiesById(int id)
        {
            try
            {
                var academicEmployeeDetails = await _academicEmployeesRepository.GetByIdAsync(id, f => f.Faculty);
                if (academicEmployeeDetails == null || academicEmployeeDetails.Faculty == null)
                {
                    return new BaseResponse<AcademicEmployee>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                return new BaseResponse<AcademicEmployee>()
                {
                    Data = academicEmployeeDetails,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AcademicEmployee>()
                {
                    Description = $"[AcademicEmployeesService.GetAcademicEmployeeWithIncludePropertiesById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<NewAcademicEmployeeDropdownsModel>> GetNewAcademicEmployeeDropdownsValues()
        {
            try
            {
                var academicEmployeeDropdownsValues = new NewAcademicEmployeeDropdownsModel()
                {
                    Faculties = (await _facultiesRepository.GetAllAsync()).OrderBy(n => n.Name).ToList()
                };

                return new BaseResponse<NewAcademicEmployeeDropdownsModel>()
                {
                    Data = academicEmployeeDropdownsValues,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewAcademicEmployeeDropdownsModel>()
                {
                    Description = $"[AcademicEmployeesService.GetNewAcademicEmployeeDropdownsValues] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<AcademicEmployee>> UpdateAcademicEmployee(NewAcademicEmployeeModel model)
        {
            try
            {
                var dbAcademicEmployee = await _academicEmployeesRepository.GetByIdAsync(model.Id);
                if (dbAcademicEmployee == null || dbAcademicEmployee.FacultyId == null)
                {
                    return new BaseResponse<AcademicEmployee>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                dbAcademicEmployee.Id = model.Id;
                dbAcademicEmployee.FirstName = model.FirstName;
                dbAcademicEmployee.LastName = model.LastName;
                dbAcademicEmployee.Email = model.Email;
                dbAcademicEmployee.ProfilePictureURL = model.ProfilePictureURL;
                dbAcademicEmployee.AcademicPosition = (AcademicPosition)model.AcademicPosition;
                dbAcademicEmployee.FacultyId = model.FacultyId;

                await _academicEmployeesRepository.UpdateAsync(dbAcademicEmployee.Id, dbAcademicEmployee);

                return new BaseResponse<AcademicEmployee>()
                {
                    Description = "Academic employee successfully updated",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AcademicEmployee>()
                {
                    Description = $"[AcademicEmployeesService.UpdateAcademicEmployee] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
