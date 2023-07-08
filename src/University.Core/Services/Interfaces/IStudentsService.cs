using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.StudentVM;

namespace University.Core.Services.Interfaces
{
    public interface IStudentsService
    {
        Task<IBaseResponse<IEnumerable<Student>>> GetStudentsList();
        Task<IBaseResponse<Student>> GetStudentWithIncludePropertiesById(int id);
        Task<IBaseResponse<Student>> AddNewStudent(NewStudentVM model);
    }
}
