using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCloudAPI.Models;
using MyCloudApplication.DTOs.Files;
using MyCloudApplication.Interfaces;
using System.IO.Compression;
namespace MyCloudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController(IFiles files) : ControllerBase
    {
        private readonly IFiles _files = files;

        [Authorize]
        [HttpPost("uploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file, int userId, int groupId)
        {
            var testFile = new FileRecordModel(
                userId, groupId, file.FileName, file.Length, DateTime.UtcNow);
            await _files.UploadFile(file, testFile.Adapt<FileRecordDTO>());
            return Ok();
        }

        //[HttpGet("getUserFiles")]
        //public async Task<IActionResult> GetUserFiles(int userId)
        //{
        //    return Ok(await _files.GetUserFiles(userId));
        //}
        [Authorize]
        [HttpGet("downloadAllUserFiles")]
        public async Task<IActionResult> DownloadAllUserFiles(int userId)
        {
            var files = await _files.GetUserFiles(userId);

            if (files == null || !files.Any())
            {
                return NotFound("Nu au fost găsite fișiere pentru acest utilizator.");
            }

            var memoryStream = new MemoryStream();

            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var fileRecord in files)
                {
                    if (fileRecord.File != null)
                    {
                        var zipEntry = archive.CreateEntry(fileRecord.FileName, CompressionLevel.Fastest);

                        using (var entryStream = zipEntry.Open())
                        using (var fileStream = fileRecord.File.OpenReadStream())
                        {
                            await fileStream.CopyToAsync(entryStream);
                        }
                    }
                }
            }
            memoryStream.Position = 0;
            return File(memoryStream, "application/zip", $"User_{userId}_Files.zip");
        }


    }
}
