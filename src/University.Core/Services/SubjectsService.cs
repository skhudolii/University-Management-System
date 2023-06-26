using University.Core.Entities;
using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Response;
using University.Core.Response.Interfeces;
using University.Core.Services.Interfaces;

namespace University.Core.Services
{
    public class SubjectsService : ISubjectsService
    {
        private readonly ISubjectsRepository _subjectsRepository;

        public SubjectsService(ISubjectsRepository subjectsRepository)
        {
            _subjectsRepository = subjectsRepository;
        }

        public async Task<IBaseResponse<Subject>> GetSubjectById(int id)
        {
            var baseResponse = new BaseResponse<Subject>();
            try
            {
                var subjectDetails = await _subjectsRepository.GetSubjectByIdAsync(id);
                if (subjectDetails == null)
                {
                    baseResponse.Description = "Not found";
                    baseResponse.StatusCode = StatusCode.NotFound;
                    return baseResponse;
                }

                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = subjectDetails;
                return baseResponse;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[GetSubjectById] : {ex.Message}";
                baseResponse.StatusCode = StatusCode.InternalServerError;
                return baseResponse;
            }
        }

        public async Task<IBaseResponse<IEnumerable<Subject>>> GetSubjectsList()
        {
            var baseResponse = new BaseResponse<IEnumerable<Subject>>();
            try
            {
                var subjects = await _subjectsRepository.GetAllAsync(n => n.Faculty);
                if (!subjects.Any())
                {
                    baseResponse.Description = "0 items found";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = subjects;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[GetSubjectsList] : {ex.Message}";
                baseResponse.StatusCode = StatusCode.InternalServerError;
                return baseResponse;
            }
        }
    }
}
