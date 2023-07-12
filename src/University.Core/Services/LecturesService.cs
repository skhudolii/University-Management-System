using University.Core.Entities;
using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Response;
using University.Core.Response.Interfeces;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.LectureVM;

namespace University.Core.Services
{
    public class LecturesService : ILecturesService
    {
        private readonly ILecturesRepository _lecturesRepository;

        public LecturesService(ILecturesRepository lecturesRepository)
        {
            _lecturesRepository = lecturesRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Lecture>>> GetLecturesList()
        {
            try
            {
                var lectures = await _lecturesRepository.GetAllAsync(s => s.Subject, lr => lr.LectureRoom, f => f.Faculty);
                var filteredLectures = lectures
                    .Where(f => f.FacultyId != null)
                    .OrderBy(n => n.LectureDate).ThenBy(n => n.StartTime);

                if (!filteredLectures.Any())
                {
                    return new BaseResponse<IEnumerable<Lecture>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<IEnumerable<Lecture>>()
                {
                    Data = filteredLectures,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Lecture>>()
                {
                    Description = $"[LecturesService.GetLecturesList] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Lecture>> GetLectureWithIncludePropertiesById(int id)
        {
            try
            {
                var lectureDetails = await _lecturesRepository.GetLectureWithIncludePropertiesByIdAsync(id);
                if (lectureDetails == null || lectureDetails.Faculty == null)
                {
                    return new BaseResponse<Lecture>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                return new BaseResponse<Lecture>()
                {
                    Data = lectureDetails,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Lecture>()
                {
                    Description = $"[LecturesService.GetLectureWithIncludePropertiesById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Lecture>> AddNewLecture(NewLectureVM newLectureVM)
        {
            try
            {
                await _lecturesRepository.AddNewLectureAsync(newLectureVM);

                return new BaseResponse<Lecture>()
                {
                    Description = "New Lecture successfully added",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Lecture>()
                {
                    Description = $"[LecturesService.AddNewLecture] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<NewLectureVM>> GetLectureById(int id)
        {
            try
            {
                var lecture = await _lecturesRepository.GetByIdAsync(id, lg => lg.LecturesGroups);
                if (lecture == null)
                {
                    return new BaseResponse<NewLectureVM>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                var data = new NewLectureVM()
                {
                    Id = lecture.Id,
                    LectureDate = lecture.LectureDate,
                    StartTime = lecture.StartTime,
                    EndTime = lecture.EndTime,
                    FacultyId = lecture.FacultyId,
                    SubjectId = lecture.SubjectId,
                    LectureRoomId = lecture.LectureRoomId,
                    AcademicEmployeeId = lecture.AcademicEmployeeId,
                    GroupIds = lecture.LecturesGroups.Select(n => n.GroupId).ToList()
                };

                return new BaseResponse<NewLectureVM>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewLectureVM>()
                {
                    Description = $"[LecturesService.GetLectureById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Lecture>> UpdateLecture(NewLectureVM newLectureVM)
        {
            try
            {
                var dbLecture = await _lecturesRepository.GetByIdAsync(newLectureVM.Id);
                if (dbLecture == null)
                {
                    return new BaseResponse<Lecture>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                await _lecturesRepository.UpdateLectureAsync(newLectureVM);

                return new BaseResponse<Lecture>()
                {
                    Description = "Lecture successfully updated",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Lecture>()
                {
                    Description = $"[LecturesService.UpdateLecture] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
