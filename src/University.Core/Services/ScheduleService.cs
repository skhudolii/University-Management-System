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

        public async Task<IBaseResponse<IEnumerable<Lecture>>> GetScheduleForFaculty(int id, string sortOrder, string searchString)
        {
            try
            {
                var scheduleForFaculty = (await _lecturesRepository.GetAllAsync(
                    a => a.AcademicEmployee,
                    f => f.Faculty,
                    lr => lr.LectureRoom,
                    s => s.Subject))
                    .Where(f => f.FacultyId == id)
                    .Where(d => d.LectureDate >= DateTime.Now.Date)
                    .OrderBy(n => n.LectureDate).ThenBy(n => n.StartTime)
                    .ToList();

                if (!scheduleForFaculty.Any())
                {
                    return new BaseResponse<IEnumerable<Lecture>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.NoContent
                    };
                }

                if (!string.IsNullOrEmpty(searchString))
                {
                    scheduleForFaculty = scheduleForFaculty.Where(n =>
                    n.Subject.Name.ToLower().Contains(searchString.ToLower()) ||
                    n.AcademicEmployee.FirstName.ToLower().Contains(searchString.ToLower()) ||
                    n.AcademicEmployee.LastName.ToLower().Contains(searchString.ToLower()) ||
                    n.LectureDate.ToString().Contains(searchString))
                    .OrderBy(n => n.LectureDate)
                    .ThenBy(n => n.StartTime).ToList();
                }

                scheduleForFaculty = sortOrder switch
                {
                    "date_desc" => scheduleForFaculty.OrderByDescending(l => l.LectureDate).ToList(),
                    "Subject" => scheduleForFaculty.OrderBy(l => l.Subject.Name).ToList(),
                    "subject_desc" => scheduleForFaculty.OrderByDescending(l => l.Subject.Name).ToList(),
                    "LectureRoom" => scheduleForFaculty.OrderBy(l => l.LectureRoom.Name).ToList(),
                    "lectureRoom_desc" => scheduleForFaculty.OrderByDescending(l => l.LectureRoom.Name).ToList(),
                    "Faculty" => scheduleForFaculty.OrderBy(l => l.Faculty.Name).ToList(),
                    "faculty_desc" => scheduleForFaculty.OrderByDescending(l => l.Faculty.Name).ToList(),
                    _ => scheduleForFaculty.OrderBy(l => l.LectureDate).ThenBy(l => l.StartTime).ToList(),
                };

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

        public async Task<IBaseResponse<IEnumerable<Lecture>>> GetScheduleForTeacher(int id, DateTime lastDateOfPeriod, string sortOrder, string searchString)
        {
            try
            {
                var scheduleForTeacher = (await _lecturesRepository.GetAllAsync(
                    a => a.AcademicEmployee,
                    f => f.Faculty,
                    lr => lr.LectureRoom,
                    s => s.Subject))
                    .Where(f => f.FacultyId != null)
                    .Where(a => a.AcademicEmployeeId == id)
                    .Where(d => d.LectureDate >= DateTime.Now.Date && d.LectureDate <= lastDateOfPeriod.Date);


                if (!scheduleForTeacher.Any())
                {
                    return new BaseResponse<IEnumerable<Lecture>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.NoContent
                    };
                }

                if (!string.IsNullOrEmpty(searchString))
                {
                    scheduleForTeacher = scheduleForTeacher.Where(n =>
                    n.Subject.Name.ToLower().Contains(searchString.ToLower()) ||
                    n.AcademicEmployee.FirstName.ToLower().Contains(searchString.ToLower()) ||
                    n.AcademicEmployee.LastName.ToLower().Contains(searchString.ToLower()) ||
                    n.LectureDate.ToString().Contains(searchString))
                    .OrderBy(n => n.LectureDate)
                    .ThenBy(n => n.StartTime).ToList();
                }

                scheduleForTeacher = sortOrder switch
                {
                    "date_desc" => scheduleForTeacher.OrderByDescending(l => l.LectureDate).ToList(),
                    "Subject" => scheduleForTeacher.OrderBy(l => l.Subject.Name).ToList(),
                    "subject_desc" => scheduleForTeacher.OrderByDescending(l => l.Subject.Name).ToList(),
                    "LectureRoom" => scheduleForTeacher.OrderBy(l => l.LectureRoom.Name).ToList(),
                    "lectureRoom_desc" => scheduleForTeacher.OrderByDescending(l => l.LectureRoom.Name).ToList(),
                    "Faculty" => scheduleForTeacher.OrderBy(l => l.Faculty.Name).ToList(),
                    "faculty_desc" => scheduleForTeacher.OrderByDescending(l => l.Faculty.Name).ToList(),
                    _ => scheduleForTeacher.OrderBy(l => l.LectureDate).ThenBy(l => l.StartTime).ToList(),
                };

                return new BaseResponse<IEnumerable<Lecture>>()
                {
                    Data = scheduleForTeacher.ToList(),
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

        public async Task<IBaseResponse<IEnumerable<Lecture>>> GetScheduleForStudent(int id, DateTime lastDateOfPeriod, string sortOrder, string searchString)
        {
            try
            {
                var lectures = (await _lecturesRepository.GetLecturesByStudentIdAsync(id)).Where(f => f.FacultyId != null);
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

                var scheduleForStudent = lecturesWithProperties
                    .Where(d => d.LectureDate >= DateTime.Now.Date && d.LectureDate <= lastDateOfPeriod.Date)
                    .OrderBy(n => n.LectureDate)
                    .ThenBy(n => n.StartTime);

                if (!string.IsNullOrEmpty(searchString))
                {
                    scheduleForStudent = scheduleForStudent.Where(n =>
                    n.Subject.Name.ToLower().Contains(searchString.ToLower()) ||
                    n.AcademicEmployee.FirstName.ToLower().Contains(searchString.ToLower()) ||
                    n.AcademicEmployee.LastName.ToLower().Contains(searchString.ToLower()) ||
                    n.LectureDate.ToString().Contains(searchString))
                    .OrderBy(n => n.LectureDate)
                    .ThenBy(n => n.StartTime);
                }

                scheduleForStudent = sortOrder switch
                {
                    "date_desc" => scheduleForStudent.OrderByDescending(l => l.LectureDate),
                    "Subject" => scheduleForStudent.OrderBy(l => l.Subject.Name),
                    "subject_desc" => scheduleForStudent.OrderByDescending(l => l.Subject.Name),
                    "LectureRoom" => scheduleForStudent.OrderBy(l => l.LectureRoom.Name),
                    "lectureRoom_desc" => scheduleForStudent.OrderByDescending(l => l.LectureRoom.Name),
                    "Faculty" => scheduleForStudent.OrderBy(l => l.Faculty.Name),
                    "faculty_desc" => scheduleForStudent.OrderByDescending(l => l.Faculty.Name),
                    _ => scheduleForStudent.OrderBy(l => l.LectureDate).ThenBy(l => l.StartTime)
                };

                return new BaseResponse<IEnumerable<Lecture>>()
                {
                    Data = scheduleForStudent,
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
    }
}
