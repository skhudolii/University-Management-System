using University.Core.Entities;
using University.Core.Repositories.Base;
using University.Core.ViewModels.Lecture;

namespace University.Core.Repositories
{
    public interface ILecturesRepository : IEntityBaseRepository<Lecture>
    {
        Task<Lecture> GetLectureByIdAsync(int id);
        Task<NewLectureDropdownsVM> GetNewLectureDropdownsValues();
        Task AddNewLectureAsync(NewLectureVM data);
        Task UpdateLectureAsync(NewLectureVM data);
    }
}
