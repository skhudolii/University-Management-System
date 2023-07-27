using University.Core.Entities;
using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Response;
using University.Core.Response.Interfeces;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.SubjectVM;

namespace University.Core.Services
{
    public class SubjectsService : ISubjectsService
    {
        private readonly ISubjectsRepository _subjectsRepository;
        private readonly IFacultiesRepository _facultiesRepository;

        public SubjectsService(ISubjectsRepository subjectsRepository, IFacultiesRepository facultiesRepository)
        {
            _subjectsRepository = subjectsRepository;
            _facultiesRepository = facultiesRepository;
        }

        public async Task<IBaseResponse<Subject>> AddNewSubject(NewSubjectVM model)
        {            
            try
            {
                var newSubject = new Subject()
                {
                    Name = model.Name,
                    FacultyId = model.FacultyId
                };
                await _subjectsRepository.AddAsync(newSubject);

                return new BaseResponse<Subject>()
                {
                    Data = newSubject,
                    Description = "New Subject successfully added",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Subject>()
                {
                    Description = $"[SubjectsService.AddNewSubject] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<NewSubjectDropdownsVM>> GetNewSubjectDropdownsValues()
        {
            try
            {
                var subjectDropdownsValues = new NewSubjectDropdownsVM()
                {
                    Faculties = (await _facultiesRepository.GetAllAsync()).OrderBy(n => n.Name).ToList()
                };

                return new BaseResponse<NewSubjectDropdownsVM>()
                {
                    Data = subjectDropdownsValues,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewSubjectDropdownsVM>()
                {
                    Description = $"[SubjectsService.GetNewSubjectDropdownsValues] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<NewSubjectVM>> GetSubjectById(int id)
        {
            try
            {
                var subjectDetails = await _subjectsRepository.GetByIdAsync(id);
                if (subjectDetails == null || subjectDetails.FacultyId == null)
                {
                    return new BaseResponse<NewSubjectVM>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                var data = new NewSubjectVM()
                {
                    Id = subjectDetails.Id,
                    Name = subjectDetails.Name,
                    FacultyId = (int)subjectDetails.FacultyId,
                };

                return new BaseResponse<NewSubjectVM>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewSubjectVM>()
                {
                    Description = $"[SubjectsService.GetSubjectById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Subject>> GetSubjectWithIncludePropertiesById(int id)
        {
            try
            {
                var subjectDetails = await _subjectsRepository.GetByIdAsync(id, f => f.Faculty);
                if (subjectDetails == null || subjectDetails.FacultyId == null)
                {
                    return new BaseResponse<Subject>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                return new BaseResponse<Subject>()
                {
                    Data = subjectDetails,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Subject>()
                {
                    Description = $"[SubjectsService.GetSubjectWithIncludePropertiesById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Subject>>> GetSortedSubjectsList(string sortOrder, string searchString)
        {
            try
            {
                var subjects = (await _subjectsRepository.GetAllAsync(n => n.Faculty)).Where(f => f.FacultyId != null);
                if (!subjects.Any())
                {
                    return new BaseResponse<IEnumerable<Subject>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.OK
                    };
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    subjects = subjects.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
                }

                subjects = sortOrder switch
                {
                    "name_desc" => subjects.OrderByDescending(s => s.Name),
                    "FacultyName" => subjects.OrderBy(s => s.Faculty.Name),
                    "facultyname_desc" => subjects.OrderByDescending(s => s.Faculty.Name),
                    _ => subjects.OrderBy(s => s.Name),
                };
                return new BaseResponse<IEnumerable<Subject>>()
                {
                    Data = subjects,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Subject>>()
                {
                    Description = $"[SubjectsService.GetSubjectsList] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Subject>> UpdateSubject(NewSubjectVM model)
        {
            try
            {
                var dbSubject = await _subjectsRepository.GetByIdAsync(model.Id);
                if (dbSubject == null || dbSubject.FacultyId == null)
                {
                    return new BaseResponse<Subject>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                dbSubject.Id = model.Id;
                dbSubject.Name = model.Name;
                dbSubject.FacultyId = model.FacultyId;

                await _subjectsRepository.UpdateAsync(dbSubject.Id, dbSubject);

                return new BaseResponse<Subject>()
                {
                    Description = "Subject successfully updated",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Subject>()
                {
                    Description = $"[SubjectsService.UpdateSubject] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteSubject(int id)
        {
            try
            {
                var subject = await _subjectsRepository.GetByIdAsync(id);
                if (subject == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };                    
                }

                await _subjectsRepository.DeleteAsync(id);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "Subject successfully deleted",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[SubjectsService.DeleteSubject] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
