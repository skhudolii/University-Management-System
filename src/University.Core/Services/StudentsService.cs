using University.Core.Entities;
using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Response;
using University.Core.Response.Interfeces;
using University.Core.Services.Interfaces;

namespace University.Core.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly IStudentsRepository _studentsRepository;

        public StudentsService(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Student>>> GetStudentsList()
        {
            try
            {
                var students = await _studentsRepository.GetAllAsync(n => n.Group, f => f.Group.Faculty);
                var filteredStudents = students.Where(f => f.Group.FacultyId != null);

                if (!filteredStudents.Any())
                {
                    return new BaseResponse<IEnumerable<Student>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<IEnumerable<Student>>()
                {
                    Data = filteredStudents,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Student>>()
                {
                    Description = $"[GetStudentsList] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
