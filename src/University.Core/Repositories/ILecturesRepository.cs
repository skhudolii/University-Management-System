using University.Core.Entities;
using University.Core.Repositories.Base;
using University.Core.ViewModels.LectureVM;

namespace University.Core.Repositories
{
    public interface ILecturesRepository : IEntityBaseRepository<Lecture>
    {
        Task<Lecture> GetLectureWithIncludePropertiesByIdAsync(int id);
        Task AddNewLectureAsync(NewLectureModel model);
        Task UpdateLectureAsync(NewLectureModel model);
        Task<IEnumerable<Lecture>> GetLecturesByStudentIdAsync(int id);
    }
}
