using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Response;
using University.Core.Response.Interfeces;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.LectureVM;
using University.Core.ViewModels.StudentVM;

namespace University.Core.Services
{
    public class LectureCascadingDropdownsService : ILectureCascadingDropdownsService
    {
        private readonly IAcademicEmployeesRepository _academicEmployeesRepository;
        private readonly IFacultiesRepository _facultiesRepository;
        private readonly IGroupsRepository _groupsRepository;
        private readonly ILectureRoomsRepository _lectureRoomsRepository;
        private readonly ISubjectsRepository _subjectsRepository;

        public LectureCascadingDropdownsService(IAcademicEmployeesRepository academicEmployeesRepository,
                                                IFacultiesRepository facultiesRepository,
                                                IGroupsRepository groupsRepository,
                                                ILectureRoomsRepository lectureRoomsRepository,
                                                ISubjectsRepository subjectsRepository)
        {
            _academicEmployeesRepository = academicEmployeesRepository;
            _facultiesRepository = facultiesRepository;
            _groupsRepository = groupsRepository;
            _lectureRoomsRepository = lectureRoomsRepository;
            _subjectsRepository = subjectsRepository;
        }

        public async Task<IBaseResponse<NewLectureDropdownsVM>> GetFaculties()
        {
            try
            {
                var lectureDropdownsFaculties = new NewLectureDropdownsVM()
                {
                    Faculties = (await _facultiesRepository.GetAllAsync()).OrderBy(n => n.Name).ToList()
                };

                return new BaseResponse<NewLectureDropdownsVM>()
                {
                    Data = lectureDropdownsFaculties,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewLectureDropdownsVM>()
                {
                    Description = $"[LectureCascadingDropdownsService.GetFaculties] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<NewLectureDropdownsVM>> GetDependentDropdownsValues()
        {
            try
            {
                var academicEmployees = await _academicEmployeesRepository.GetAllAsync();
                var filteredAcademicEmployees = academicEmployees.Where(f => f.FacultyId != null);

                var groups = await _groupsRepository.GetAllAsync();
                var filteredGroups = groups.Where(f => f.FacultyId != null);

                var lectureRooms = await _lectureRoomsRepository.GetAllAsync();
                var fillteredLectureRooms = lectureRooms.Where(f => f.FacultyId != null);

                var subjects = await _subjectsRepository.GetAllAsync();
                var filteredSubjects = subjects.Where(f => f.FacultyId != null);

                var lectureDropdownsValues = new NewLectureDropdownsVM()
                {
                    AcademicEmployees = filteredAcademicEmployees.OrderBy(n => n.FullName).ToList(),
                    Groups = filteredGroups.OrderBy(n => n.Name).ToList(),
                    LectureRooms = fillteredLectureRooms.OrderBy(n => n.Name).ToList(),
                    Subjects = filteredSubjects.OrderBy(n => n.Name).ToList()
                };

                return new BaseResponse<NewLectureDropdownsVM>()
                {
                    Data = lectureDropdownsValues,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewLectureDropdownsVM>()
                {
                    Description = $"[LectureCascadingDropdownsService.GetDependentDropdownsValues] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
