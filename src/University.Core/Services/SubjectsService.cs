﻿using University.Core.Entities;
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
                    Description = $"[AddNewSubject] : {ex.Message}",
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
                    Description = $"[GetNewSubjectDropdownsValues] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<NewSubjectVM>> GetSubjectById(int id)
        {
            try
            {
                var subjectDetails = await _subjectsRepository.GetByIdAsync(id);
                if (subjectDetails == null)
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
                    Description = $"[GetSubjectById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Subject>> GetSubjectWithIncludePropertiesById(int id)
        {
            try
            {
                var subjectDetails = await _subjectsRepository.GetByIdAsync(id, f => f.Faculty);
                if (subjectDetails == null || subjectDetails.Faculty == null)
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
                    Description = $"[GetSubjectWithIncludePropertiesById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Subject>>> GetSubjectsList()
        {
            try
            {
                var subjects = await _subjectsRepository.GetAllAsync(n => n.Faculty);
                var filteredSubjects = subjects.Where(f => f.FacultyId != null);

                if (!filteredSubjects.Any())
                {
                    return new BaseResponse<IEnumerable<Subject>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<IEnumerable<Subject>>()
                {
                    Data = filteredSubjects,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Subject>>()
                {
                    Description = $"[GetSubjectsList] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Subject>> UpdateSubject(NewSubjectVM model)
        {
            try
            {
                var dbSubject = await _subjectsRepository.GetByIdAsync(model.Id);
                if (dbSubject == null)
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
                    Description = $"[UpdateSubject] : {ex.Message}",
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
                    Description = $"[DeleteSubject] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
