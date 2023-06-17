using University.Core.Entities;
using University.Core.Repositories.Base;

namespace University.Core.Repositories
{
    public interface ILecturesRepository : IEntityBaseRepository<Lecture>
    {
        Task<Lecture> GetLectureByIdAsync(int id);
        Task<NewLectureDropdowns> GetNewLectureDropdownsValues();
    }
}
