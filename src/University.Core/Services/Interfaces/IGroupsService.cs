using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.GroupVM;

namespace University.Core.Services.Interfaces
{
    public interface IGroupsService
    {
        Task<IBaseResponse<IEnumerable<Group>>> GetSortedGroupsList(string sortOrder);
        Task<IBaseResponse<NewGroupVM>> GetGroupById(int id);
        Task<IBaseResponse<Group>> GetGroupWithIncludePropertiesById(int id);
        Task<IBaseResponse<NewGroupDropdownsVM>> GetNewGroupDropdownsValues();
        Task<IBaseResponse<Group>> AddNewGroup(NewGroupVM model);
        Task<IBaseResponse<Group>> UpdateGroup(NewGroupVM model);
        Task<IBaseResponse<bool>> DeleteGroup(int id);
    }
}
