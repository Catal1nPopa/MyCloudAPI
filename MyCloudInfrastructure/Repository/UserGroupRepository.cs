using Microsoft.EntityFrameworkCore;
using MyCloudDomain.Groups;
using MyCloudDomain.Interfaces;

namespace MyCloudInfrastructure.Repository
{
    public class UserGroupRepository(MyDbContext myDbContext) : IUserGroupRepository
    {
        private readonly MyDbContext _myDbContext = myDbContext;
        public async Task AssignUserToGroup(UserGroup userGroup)
        {
            _myDbContext.user_group.Add(userGroup);
            await _myDbContext.SaveChangesAsync();
        }

        public async Task DeleteUserFromGroup(int userId, int groupId)
        {
            throw new NotImplementedException();
        }
    }
}
