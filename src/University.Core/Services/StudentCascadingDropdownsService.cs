using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Response;
using University.Core.Response.Interfeces;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.StudentVM;

namespace University.Core.Services
{
    public class StudentCascadingDropdownsService : IStudentCascadingDropdownsService
    {
        private readonly IFacultiesRepository _facultiesRepository;
        private readonly IGroupsRepository _groupsRepository;

        public StudentCascadingDropdownsService(IFacultiesRepository facultiesRepository, IGroupsRepository groupsRepository)
        {
            _facultiesRepository = facultiesRepository;
            _groupsRepository = groupsRepository;
        }

        public async Task<IBaseResponse<NewStudentDropdownsModel>> GetFaculties()
        {
            try
            {
                var studentDropdownsFaculties = new NewStudentDropdownsModel()
                {
                    Faculties = (await _facultiesRepository.GetAllAsync()).OrderBy(n => n.Name).ToList()
                };                

                return new BaseResponse<NewStudentDropdownsModel>()
                {
                    Data = studentDropdownsFaculties,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewStudentDropdownsModel>()
                {
                    Description = $"[StudentCascadingDropdownsService.GetFaculties] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<NewStudentDropdownsModel>> GetDependentGroups()
        {
            try
            {
                var groups = (await _groupsRepository.GetAllAsync()).Where(f => f.FacultyId != null);
                var studentDropdownsGroups = new NewStudentDropdownsModel()
                {
                    Groups = groups.OrderBy(n => n.Name).ToList()
                };

                return new BaseResponse<NewStudentDropdownsModel>()
                {
                    Data = studentDropdownsGroups,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewStudentDropdownsModel>()
                {
                    Description = $"[StudentCascadingDropdownsService.GetDependentGroups] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
