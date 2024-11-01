using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCloudApplication.Interfaces;

namespace MyCloudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController(IStorage storage) : ControllerBase
    {
        private readonly IStorage _storage = storage;

        [HttpGet("getAllSpace")]
        public async Task<IActionResult> GetDiskInfo()
        {
            return Ok(await _storage.GetDriveInfo());
        }
    }
}
