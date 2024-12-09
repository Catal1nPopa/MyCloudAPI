using Microsoft.AspNetCore.Mvc;
using MyCloudApplication.DTOs.Groups;
using MyCloudApplication.Interfaces;

namespace MyCloudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController(IGroups groups) : ControllerBase
    {
        private readonly IGroups _groups = groups;

        [HttpPost]
        public async Task <IActionResult> createGroup([FromBody] CreateGroupDTO createGroupDTO)
        {
            await _groups.createGroup(createGroupDTO);
            return Ok();
        }

        [HttpGet]
        public async Task <IActionResult> getUserGroups(int id)
        {
            return Ok(await _groups.getGroupByUserId(id));
        }
    }
}
