using University.Core.Entities;
using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Response;
using University.Core.Response.Interfeces;
using University.Core.Services.Interfaces;

namespace University.Core.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ILecturesRepository _lecturesRepository;

        public ScheduleService(ILecturesRepository lecturesRepository)
        {
            _lecturesRepository = lecturesRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Lecture>>> GetScheduleForFaculty(int id)
        {
            try
            {
                var lectures = await _lecturesRepository.GetAllAsync(s => s.Subject, lr => lr.LectureRoom, f => f.Faculty);
                var scheduleForFaculty = lectures
                    .Where(f => f.FacultyId == id)
                    .Where(d => d.LectureDate >= DateTime.Now.Date)
                    .OrderBy(n => n.LectureDate).ThenBy(n => n.StartTime);

                if (!scheduleForFaculty.Any())
                {
                    return new BaseResponse<IEnumerable<Lecture>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.NoContent
                    };
                }

                return new BaseResponse<IEnumerable<Lecture>>()
                {
                    Data = scheduleForFaculty,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Lecture>>()
                {
                    Description = $"[ScheduleService.GetScheduleForFaculty] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Lecture>>> GetScheduleForStudent(int id, DateTime lastDateOfPeriod)
        {
            try
            {
                var lectures = await _lecturesRepository.GetLecturesByStudentIdAsync(id);
                if (!lectures.Any())
                {
                    return new BaseResponse<IEnumerable<Lecture>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.NoContent
                    };
                }

                var lecturesWithProperties = new List<Lecture>();
                foreach (var lecture in lectures)
                {
                    var lectureDetails = await _lecturesRepository.GetLectureWithIncludePropertiesByIdAsync(lecture.Id);
                    lecturesWithProperties.Add(lectureDetails);
                }

                var sheduleForStudent = lecturesWithProperties
                    .Where(d => d.LectureDate >= DateTime.Now.Date && d.LectureDate <= lastDateOfPeriod.Date)
                    .OrderBy(n => n.LectureDate).ThenBy(n => n.StartTime);

                return new BaseResponse<IEnumerable<Lecture>>()
                {
                    Data = sheduleForStudent,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Lecture>>()
                {
                    Description = $"[ScheduleService.GetScheduleForStudent] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Lecture>>> GetScheduleForTeacher(int id, DateTime lastDateOfPeriod)
        {
            try
            {
                var lectures = await _lecturesRepository.GetAllAsync(
                    s => s.Subject, 
                    lr => lr.LectureRoom, 
                    f => f.Faculty, 
                    a => a.AcademicEmployee);

                var sheduleForTeacher = lectures
                    .Where(a => a.AcademicEmployeeId == id)
                    .Where(d => d.LectureDate >= DateTime.Now.Date && d.LectureDate <= lastDateOfPeriod.Date)
                    .OrderBy(n => n.LectureDate).ThenBy(n => n.StartTime);


                if (!sheduleForTeacher.Any())
                {
                    return new BaseResponse<IEnumerable<Lecture>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.NoContent
                    };
                }

                return new BaseResponse<IEnumerable<Lecture>>()
                {
                    Data = sheduleForTeacher,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Lecture>>()
                {
                    Description = $"[ScheduleService.GetScheduleForTeacher] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
