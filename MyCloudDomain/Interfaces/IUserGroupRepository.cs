using MyCloudDomain.Groups;

namespace MyCloudDomain.Interfaces
{
    public interface IUserGroupRepository
    {
        Task AssignUserToGroup(UserGroup userGroup);
        Task DeleteUserFromGroup(int userId, int groupId);
    }
}
