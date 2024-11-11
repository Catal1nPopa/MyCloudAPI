using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCloudApplication.DTOs;
using MyCloudApplication.Interfaces;

namespace MyCloudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController(IGroups groups) : ControllerBase
    {
        private readonly IGroups _groups = groups;

        [HttpPost("createGroups")]
        public async Task <IActionResult> createGroup([FromBody] CreateGroupDTO createGroupDTO)
        {
            await _groups.createGroup(createGroupDTO);
            return Ok();
        }

        [HttpGet("getGroups")]
        public async Task <IActionResult> getUserGroups(int id)
        {
            return Ok(await _groups.getGroupByUserId(id));
        }
    }
}
