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
                return new BaseResponse<IEnumerable<Subject>>()
                {
                    Description = $"[GetSubjectsList] : {ex.Message}"
                };
            }
        }
    }
}
