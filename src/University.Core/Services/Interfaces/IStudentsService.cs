using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.StudentVM;

namespace University.Core.Services.Interfaces
{
    public interface IStudentsService
    {
        Task<IBaseResponse<IEnumerable<Student>>> GetSortedStudentsList(string sortOrder, string searchString);
        Task<IBaseResponse<NewStudentModel>> GetStudentById(int id);
        Task<IBaseResponse<Student>> GetStudentWithIncludePropertiesById(int id);
        Task<IBaseResponse<Student>> AddNewStudent(NewStudentModel model);
        Task<IBaseResponse<Student>> UpdateStudent(NewStudentModel model);
        Task<IBaseResponse<bool>> DeleteStudent(int id);
    }
}
