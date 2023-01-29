using University.Core.Entities;
using University.Core.Repositories;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories.Base;

namespace University.Infrastructure.Repositories
{
    public class GroupsRepository : EntityBaseRepository<Group>, IGroupsRepository
    {
        public GroupsRepository(UniversityDbContext context) : base(context)
        {
        }
    }
}
