using University.Core.Entities;
using University.Core.Response.Interfeces;
using University.Core.ViewModels.GroupVM;

namespace University.Core.Services.Interfaces
{
    public interface IGroupsService
    {
        Task<IBaseResponse<IEnumerable<Group>>> GetSortedGroupsList(string sortOrder, string searchString);
        Task<IBaseResponse<NewGroupModel>> GetGroupById(int id);
        Task<IBaseResponse<Group>> GetGroupWithIncludePropertiesById(int id);
        Task<IBaseResponse<NewGroupDropdownsModel>> GetNewGroupDropdownsValues();
        Task<IBaseResponse<Group>> AddNewGroup(NewGroupModel model);
        Task<IBaseResponse<Group>> UpdateGroup(NewGroupModel model);
        Task<IBaseResponse<bool>> DeleteGroup(int id);
    }
}
