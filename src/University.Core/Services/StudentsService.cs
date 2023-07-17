using University.Core.Entities;
using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Response;
using University.Core.Response.Interfeces;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.StudentVM;

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
                var students = (await _studentsRepository.GetAllAsync(n => n.Group, f => f.Group.Faculty))
                    .Where(f => f.Group.FacultyId != null);

                if (!students.Any())
                {
                    return new BaseResponse<IEnumerable<Student>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<IEnumerable<Student>>()
                {
                    Data = students,
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

        public async Task<IBaseResponse<Student>> GetStudentWithIncludePropertiesById(int id)
        {
            try
            {
                var studentDetails = await _studentsRepository.GetByIdAsync(id, g =>g.Group, f => f.Group.Faculty);
                if (studentDetails == null || studentDetails.Group.FacultyId == null)
                {
                    return new BaseResponse<Student>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                return new BaseResponse<Student>()
                {
                    Data = studentDetails,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Student>()
                {
                    Description = $"[GetStudentWithIncludePropertiesById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }       

        public async Task<IBaseResponse<Student>> AddNewStudent(NewStudentVM model)
        {
            try
            {
                var newStudent = new Student()
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    ProfilePictureURL = model.ProfilePictureURL,
                    GroupId = (int)model.GroupId
                };
                await _studentsRepository.AddAsync(newStudent);

                return new BaseResponse<Student>()
                {
                    Data = newStudent,
                    Description = "New Student successfully added",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Student>()
                {
                    Description = $"[AddNewStudent] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<NewStudentVM>> GetStudentById(int id)
        {            
            try
            {
                var student = await _studentsRepository.GetByIdAsync(id, g => g.Group);
                if (student == null || student.Group.FacultyId == null)
                {
                    return new BaseResponse<NewStudentVM>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                var data = new NewStudentVM()
                {
                    Id = student.Id,
                    FullName = student.FullName,
                    Email = student.Email,
                    ProfilePictureURL = student.ProfilePictureURL,
                    GroupId = (int)student.GroupId,
                    FacultyId = student.Group.FacultyId
                };

                return new BaseResponse<NewStudentVM>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewStudentVM>()
                {
                    Description = $"[StudentService.GetStudentById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Student>> UpdateStudent(NewStudentVM model)
        {
            try
            {
                var dbStudent = await _studentsRepository.GetByIdAsync(model.Id);
                if (dbStudent == null || dbStudent.Group.FacultyId == null)
                {
                    return new BaseResponse<Student>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                dbStudent.Id = model.Id;
                dbStudent.FullName = model.FullName;
                dbStudent.Email = model.Email;
                dbStudent.ProfilePictureURL = model.ProfilePictureURL;
                dbStudent.GroupId = (int)model.GroupId;

                await _studentsRepository.UpdateAsync(dbStudent.Id, dbStudent);

                return new BaseResponse<Student>()
                {
                    Description = "Student successfully updated",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Student>()
                {
                    Description = $"[StudentServuce.UpdateStudent] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteStudent(int id)
        {
            try
            {
                var student = await _studentsRepository.GetByIdAsync(id);
                if (student == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                await _studentsRepository.DeleteAsync(id);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "Student successfully deleted",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[StudentsService.DeleteStudent] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
