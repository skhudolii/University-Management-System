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

        public async Task<IBaseResponse<IEnumerable<Student>>> GetSortedStudentsList(string sortOrder, string searchString)
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

                if (!String.IsNullOrEmpty(searchString))
                {
                    students = students.Where(ae => ae.FirstName.ToLower().Contains(searchString.ToLower())
                                           || ae.LastName.ToLower().Contains(searchString.ToLower()));
                }

                students = sortOrder switch
                {
                    "lastname_desc" => students.OrderByDescending(s => s.LastName),
                    "FirstName" => students.OrderBy(s => s.FirstName),
                    "firstname_desc" => students.OrderByDescending(s => s.FirstName),
                    "Group" => students.OrderBy(s => s.Group.Name),
                    "group_desc" => students.OrderByDescending(s => s.Group.Name),
                    "Faculty" => students.OrderBy(s => s.Group.Faculty.Name),
                    "faculty_desc" => students.OrderByDescending(s => s.Group.Faculty.Name),
                    _ => students.OrderBy(s => s.LastName),
                };
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
                    Description = $"[StudentsService.GetStudentsList] : {ex.Message}",
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
                    Description = $"[StudentsService.GetStudentWithIncludePropertiesById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }       

        public async Task<IBaseResponse<Student>> AddNewStudent(NewStudentModel model)
        {
            try
            {
                var newStudent = new Student()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
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
                    Description = $"[StudentsService.AddNewStudent] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<NewStudentModel>> GetStudentById(int id)
        {            
            try
            {
                var student = await _studentsRepository.GetByIdAsync(id, g => g.Group);
                if (student == null || student.Group.FacultyId == null)
                {
                    return new BaseResponse<NewStudentModel>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                var data = new NewStudentModel()
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email,
                    ProfilePictureURL = student.ProfilePictureURL,
                    GroupId = (int)student.GroupId,
                    FacultyId = student.Group.FacultyId
                };

                return new BaseResponse<NewStudentModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewStudentModel>()
                {
                    Description = $"[StudentsService.GetStudentById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Student>> UpdateStudent(NewStudentModel model)
        {
            try
            {
                var dbStudent = await _studentsRepository.GetByIdAsync(model.Id);
                if (dbStudent == null)
                {
                    return new BaseResponse<Student>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                dbStudent.Id = model.Id;
                dbStudent.FirstName = model.FirstName;
                dbStudent.LastName = model.LastName;
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
                    Description = $"[StudentsService.UpdateStudent] : {ex.Message}",
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
