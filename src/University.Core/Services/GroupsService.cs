using University.Core.Entities;
using University.Core.Enums;
using University.Core.Repositories;
using University.Core.Response;
using University.Core.Response.Interfeces;
using University.Core.Services.Interfaces;
using University.Core.ViewModels.GroupVM;

namespace University.Core.Services
{
    public class GroupsService : IGroupsService
    {
        private readonly IGroupsRepository _groupsRepository;
        private readonly IFacultiesRepository _facultiesRepository;

        public GroupsService(IGroupsRepository groupsRepository, IFacultiesRepository facultiesRepository)
        {
            _groupsRepository = groupsRepository;
            _facultiesRepository = facultiesRepository;
        }

        public async Task<IBaseResponse<Group>> AddNewGroup(NewGroupVM model)
        {
            try
            {
                if (await IsGroupNameAlreadyTaken(model.Name))
                {
                    return new BaseResponse<Group>()
                    {
                        Description = "Group name is already taken",
                        StatusCode = StatusCode.PreconditionFailed
                    };
                }

                var newGroup = new Group()
                {
                    Name = model.Name,
                    FacultyId = model.FacultyId
                };
                await _groupsRepository.AddAsync(newGroup);

                return new BaseResponse<Group>()
                {
                    Data = newGroup,
                    Description = "New Group successfully added",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Group>()
                {
                    Description = $"[GroupsService.AddNewGroup] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Group>>> GetSortedGroupsList(string sortOrder, string searchString)
        {
            try
            {
                var groups = (await _groupsRepository.GetAllAsync(n => n.Faculty)).Where(f => f.FacultyId != null);
                if (!groups.Any())
                {
                    return new BaseResponse<IEnumerable<Group>>()
                    {
                        Description = "0 items found",
                        StatusCode = StatusCode.OK
                    };
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    groups = groups.Where(g => g.Name.ToLower().Contains(searchString.ToLower()));
                }

                groups = sortOrder switch
                {
                    "name_desc" => groups.OrderByDescending(g => g.Name),
                    "Faculty" => groups.OrderBy(g => g.Faculty.Name),
                    "faculty_desc" => groups.OrderByDescending(g => g.Faculty.Name),
                    _ => groups.OrderBy(g => g.Name),
                };
                return new BaseResponse<IEnumerable<Group>>()
                {
                    Data = groups.ToList(),
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Group>>()
                {
                    Description = $"[GroupsService.GetGroupsList] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<NewGroupVM>> GetGroupById(int id)
        {
            try
            {
                var groupDetails = await _groupsRepository.GetByIdAsync(id);
                if (groupDetails == null || groupDetails.FacultyId == null)
                {
                    return new BaseResponse<NewGroupVM>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }
                var data = new NewGroupVM()
                {
                    Id = groupDetails.Id,
                    Name = groupDetails.Name,
                    FacultyId = (int)groupDetails.FacultyId,
                };

                return new BaseResponse<NewGroupVM>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewGroupVM>()
                {
                    Description = $"[GroupsService.GetGroupById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Group>> GetGroupWithIncludePropertiesById(int id)
        {
            try
            {
                var groupDetails = await _groupsRepository.GetByIdAsync(id, f => f.Faculty, s => s.Students);
                if (groupDetails == null || groupDetails.FacultyId == null)
                {
                    return new BaseResponse<Group>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                return new BaseResponse<Group>()
                {
                    Data = groupDetails,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Group>()
                {
                    Description = $"[GroupsService.GetGroupWithIncludePropertiesById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<NewGroupDropdownsVM>> GetNewGroupDropdownsValues()
        {
            try
            {
                var groupDropdownsValues = new NewGroupDropdownsVM()
                {
                    Faculties = (await _facultiesRepository.GetAllAsync()).OrderBy(n => n.Name).ToList()
                };

                return new BaseResponse<NewGroupDropdownsVM>()
                {
                    Data = groupDropdownsValues,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<NewGroupDropdownsVM>()
                {
                    Description = $"[GroupsService.GetNewGroupDropdownsValues] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Group>> UpdateGroup(NewGroupVM model)
        {
            try
            {
                var dbGroup = await _groupsRepository.GetByIdAsync(model.Id);
                if (dbGroup == null || dbGroup.FacultyId == null)
                {
                    return new BaseResponse<Group>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                if (await IsGroupNameAlreadyTaken(model.Name))
                {
                    return new BaseResponse<Group>()
                    {
                        Description = "Group name is already taken",
                        StatusCode = StatusCode.PreconditionFailed
                    };
                }

                dbGroup.Id = model.Id;
                dbGroup.Name = model.Name;
                dbGroup.FacultyId = model.FacultyId;

                await _groupsRepository.UpdateAsync(dbGroup.Id, dbGroup);

                return new BaseResponse<Group>()
                {
                    Description = "Group successfully updated",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Group>()
                {
                    Description = $"[GroupsService.UpdateGroup] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteGroup(int id)
        {
            try
            {
                var group = await _groupsRepository.GetByIdAsync(id);
                if (group == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                if (group.Students.Any())
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Group can not be deleted if there is at least one student in this group",
                        StatusCode = StatusCode.PreconditionFailed
                    };
                }

                await _groupsRepository.DeleteAsync(id);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "Group successfully deleted",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[GroupsService.DeleteGroup] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        private async Task<bool> IsGroupNameAlreadyTaken(string groupName)
        {
            // Retrieve the list of all existing groups
            IEnumerable<Group> existingGroups = await _groupsRepository.GetAllAsync();

            // Check if any group in the list has the same name
            return existingGroups.Any(g => g.Name == groupName);
        }
    }
}
