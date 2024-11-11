using MyCloudDomain.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloudDomain.Interfaces
{
    public interface IGroupRepository
    {
        Task createGroup(GroupsEntity groupsEntity);
        Task<List<GroupsEntity>> getGroupByUserId(int groupId);
    }
}
