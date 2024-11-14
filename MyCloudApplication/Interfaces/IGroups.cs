using MyCloudApplication.DTOs.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloudApplication.Interfaces
{
    public interface IGroups
    {
        Task createGroup(CreateGroupDTO createGroupDTO);
        Task<List<CreateGroupDTO>> getGroupByUserId(int userId);
    }
}
