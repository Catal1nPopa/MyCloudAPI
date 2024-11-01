using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCloudAPI.Models;
using MyCloudApplication.DTOs;
using MyCloudApplication.Interfaces;

namespace MyCloudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController(IFiles files) : ControllerBase
    {
        private readonly IFiles _files = files;
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file, int userId)
        {
            var testFile = new FileRecordModel(
                1, userId, null, file.FileName, "fake path", file.Length, DateTime.UtcNow);
            await _files.UploadFile(file, testFile.Adapt<FileRecordDTO>());
            return Ok();
        }
    }
}
