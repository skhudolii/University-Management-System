using University.Core.Entities;
using University.Core.Response.Interfeces;

namespace University.Core.Services.Interfaces
{
    public interface IStudentsService
    {
        Task<IBaseResponse<IEnumerable<Student>>> GetStudentsList();
    }
}
