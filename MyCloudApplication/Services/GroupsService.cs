using Mapster;
using MyCloudApplication.DTOs.Groups;
using MyCloudApplication.Interfaces;
using MyCloudDomain.Groups;
using MyCloudDomain.Interfaces;

namespace MyCloudApplication.Services
{
    public class GroupsService(IGroupRepository groupRepository) : IGroups
    {
        private readonly IGroupRepository _groupRepository = groupRepository;

        public async Task createGroup(CreateGroupDTO createGroupDTO)
        {
            await _groupRepository.createGroup(createGroupDTO.Adapt<GroupsEntity>());       
        }

        public async Task<List<CreateGroupDTO>> getGroupByUserId(int userId)
        {
            return (await _groupRepository.getGroupByUserId(userId)).Adapt<List<CreateGroupDTO>>();
        } 
    }
}
