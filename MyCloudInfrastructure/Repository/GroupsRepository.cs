using Microsoft.EntityFrameworkCore;
using MyCloudDomain.Groups;
using MyCloudDomain.Interfaces;

namespace MyCloudInfrastructure.Repository
{
    public class GroupsRepository(MyDbContext myDbContext) : IGroupRepository
    {
        private readonly MyDbContext _myDbContext = myDbContext;

        public async Task createGroup(GroupsEntity groupsEntity)
        {
            _myDbContext.groups.Add(groupsEntity);
            _myDbContext.SaveChangesAsync();
        }

        public async Task<List<GroupsEntity>> getGroupByUserId(int userId)
        {
            return await _myDbContext.groups
        .Where(group => group.Users.ToList().Contains(userId))
        .ToListAsync();
        }
    }
}
