using University.Core.Entities;
using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Response;
using University.Core.Response.Interfeces;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.FacultyVM;

namespace University.Core.Services
{
    public class FacultiesService : IFacultiesService
    {
        private readonly IFacultiesRepository _facultiesRepository;

        public FacultiesService(IFacultiesRepository facultiesRepository)
        {
            _facultiesRepository = facultiesRepository;
        }

        public async Task<IBaseResponse<Faculty>> AddNewFaculty(NewFacultyVM model)
        {
            try
            {
                var newFaculty = new Faculty()
                {
                    Name = model.Name,
                    Logo = model.Logo
                };
                await _facultiesRepository.AddAsync(newFaculty);

                return new BaseResponse<Faculty>()
                {
                    Data = newFaculty,
                    Description = "New Faculty successfully added",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Faculty>()
                {
                    Description = $"[FacultiesService.AddNewFaculty] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Faculty>>> GetFacultiesList()
        {
            try
            {
                var faculties = await _facultiesRepository.GetAllAsync();

                if (!faculties.Any())
                {
                    return new BaseResponse<IEnumerable<Faculty>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<IEnumerable<Faculty>>()
                {
                    Data = faculties,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Faculty>>()
                {
                    Description = $"[FacultiesService.GetFacultiesList] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<NewFacultyVM>> GetFacultyById(int id)
        {
            try
            {
                var facultyDetails = await _facultiesRepository.GetByIdAsync(id);
                if (facultyDetails == null)
                {
                    return new BaseResponse<NewFacultyVM>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                var data = new NewFacultyVM()
                {
                    Id = facultyDetails.Id,
                    Name = facultyDetails.Name,
                    Logo = facultyDetails.Logo
                };

                return new BaseResponse<NewFacultyVM>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewFacultyVM>()
                {
                    Description = $"[FacultiesService.GetFacultyById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Faculty>> GetFacultyWithIncludePropertiesById(int id)
        {
            try
            {
                var facultyDetails = await _facultiesRepository.GetByIdAsync(id,
                                                                             a => a.AcademicEmployees,
                                                                             g => g.Groups,
                                                                             s => s.Subjects);
                if (facultyDetails == null)
                {
                    return new BaseResponse<Faculty>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                return new BaseResponse<Faculty>()
                {
                    Data = facultyDetails,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Faculty>()
                {
                    Description = $"[FacultiesService.GetFacultyWithIncludePropertiesById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Faculty>> UpdateFaculty(NewFacultyVM model)
        {
            try
            {
                var dbFaculty = await _facultiesRepository.GetByIdAsync(model.Id);
                if (dbFaculty == null)
                {
                    return new BaseResponse<Faculty>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                dbFaculty.Id = model.Id;
                dbFaculty.Name = model.Name;
                dbFaculty.Logo = model.Logo;

                await _facultiesRepository.UpdateAsync(dbFaculty.Id, dbFaculty);

                return new BaseResponse<Faculty>()
                {
                    Description = "Faculty successfully updated",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Faculty>()
                {
                    Description = $"[FacultiesService.UpdateFaculty] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteFaculty(int id)
        {
            try
            {
                var faculty = await _facultiesRepository.GetByIdAsync(id);
                if (faculty == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                await _facultiesRepository.DeleteAsync(id);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "Faculty successfully deleted",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[FacultiesService.DeleteFaculty] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
