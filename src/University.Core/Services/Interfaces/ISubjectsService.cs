using University.Core.Entities;
using University.Core.Response.Interfeces;

namespace University.Core.Services.Interfaces
{
    public interface ISubjectsService
    {
        Task<IBaseResponse<IEnumerable<Subject>>> GetSubjectsList();
    }
}
