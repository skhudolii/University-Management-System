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

        public async Task<IBaseResponse<IEnumerable<Lecture>>> GetSortedLecturesList(string sortOrder, string searchString)
        {
            try
            {
                var lectures = (await _lecturesRepository.GetAllAsync(
                    a => a.AcademicEmployee,
                    f => f.Faculty,
                    lr => lr.LectureRoom,
                    s => s.Subject))
                    .Where(f => f.FacultyId != null);

                if (!lectures.Any())
                {
                    return new BaseResponse<IEnumerable<Lecture>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.OK
                    };
                }

                if (!string.IsNullOrEmpty(searchString))
                {
                    lectures = lectures.Where(n =>
                    n.Subject.Name.ToLower().Contains(searchString.ToLower()) ||
                    n.AcademicEmployee.FirstName.ToLower().Contains(searchString.ToLower()) ||
                    n.AcademicEmployee.LastName.ToLower().Contains(searchString.ToLower()) ||
                    n.LectureDate.ToString().Contains(searchString))
                    .OrderBy(n => n.LectureDate)
                    .ThenBy(n => n.StartTime).ToList();                   
                }

                lectures = sortOrder switch
                {
                    "date_desc" => lectures.OrderByDescending(l => l.LectureDate),
                    "Subject" => lectures.OrderBy(l => l.Subject.Name),
                    "subject_desc" => lectures.OrderByDescending(l => l.Subject.Name),
                    "LectureRoom" => lectures.OrderBy(l => l.LectureRoom.Name),
                    "lectureRoom_desc" => lectures.OrderByDescending(l => l.LectureRoom.Name),
                    "Faculty" => lectures.OrderBy(l => l.Faculty.Name),
                    "faculty_desc" => lectures.OrderByDescending(l => l.Faculty.Name),
                    _ => lectures.OrderBy(l => l.LectureDate).ThenBy(l => l.StartTime),
                };
                return new BaseResponse<IEnumerable<Lecture>>()
                {
                    Data = lectures.ToList(),
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
                if (lectureDetails == null || lectureDetails.FacultyId == null)
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

        public async Task<IBaseResponse<Lecture>> AddNewLecture(NewLectureModel newLectureVM)
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

        public async Task<IBaseResponse<NewLectureModel>> GetLectureById(int id)
        {
            try
            {
                var lecture = await _lecturesRepository.GetByIdAsync(id, lg => lg.LecturesGroups);
                if (lecture == null || lecture.FacultyId == null)
                {
                    return new BaseResponse<NewLectureModel>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                var data = new NewLectureModel()
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

                return new BaseResponse<NewLectureModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewLectureModel>()
                {
                    Description = $"[LecturesService.GetLectureById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Lecture>> UpdateLecture(NewLectureModel newLectureVM)
        {
            try
            {
                var dbLecture = await _lecturesRepository.GetByIdAsync(newLectureVM.Id);
                if (dbLecture == null || dbLecture.FacultyId == null)
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

        public async Task<IBaseResponse<bool>> DeleteLecture(int id)
        {
            try
            {
                var lecture = await _lecturesRepository.GetByIdAsync(id);
                if (lecture == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                await _lecturesRepository.DeleteAsync(id);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "Lecture successfully deleted",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[LecturesService.DeleteLecture] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
