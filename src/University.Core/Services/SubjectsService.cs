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

        public SubjectsService(ISubjectsRepository subjectsRepository)
        {
            _subjectsRepository = subjectsRepository;
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
                var dropdownsValues = await _subjectsRepository.GetNewSubjectDropdownsValuesAsync();

                return new BaseResponse<NewSubjectDropdownsVM>()
                {
                    Data = dropdownsValues,
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

        public async Task<IBaseResponse<Subject>> GetSubjectById(int id)
        {
            try
            {
                var subjectDetails = await _subjectsRepository.GetSubjectByIdAsync(id);
                if (subjectDetails == null)
                {
                    return new BaseResponse<Subject>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.InternalServerError
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
                    Description = $"[GetSubjectById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Subject>>> GetSubjectsList()
        {
            try
            {
                var subjects = await _subjectsRepository.GetAllAsync(n => n.Faculty);
                if (!subjects.Any())
                {
                    return new BaseResponse<IEnumerable<Subject>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.OK
                    };
                }

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
                    Description = $"[GetSubjectsList] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
