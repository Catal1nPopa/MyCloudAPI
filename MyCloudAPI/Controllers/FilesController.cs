using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCloudAPI.Models;
using MyCloudApplication.DTOs.Files;
using MyCloudApplication.Interfaces;
using System.IO.Compression;
using System.IO.Compression;
namespace MyCloudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController(IFiles files) : ControllerBase
    {
        private readonly IFiles _files = files;
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
        [HttpGet("downloadAllUserFiles")]
        public async Task<IActionResult> DownloadAllUserFiles(int userId)
        {
            // Obține lista de fișiere ale utilizatorului
            var files = await _files.GetUserFiles(userId);

            if (files == null || !files.Any())
            {
                return NotFound("Nu au fost găsite fișiere pentru acest utilizator.");
            }

            // Creează un stream de memorie pentru fișierul ZIP (fără blocul using)
            var memoryStream = new MemoryStream();

            // Creează arhiva ZIP
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var fileRecord in files)
                {
                    if (fileRecord.File != null)
                    {
                        // Creează o intrare în arhiva ZIP pentru fiecare fișier
                        var zipEntry = archive.CreateEntry(fileRecord.FileName, CompressionLevel.Fastest);

                        using (var entryStream = zipEntry.Open())
                        using (var fileStream = fileRecord.File.OpenReadStream())
                        {
                            await fileStream.CopyToAsync(entryStream);  // Copiază fișierul în ZIP
                        }
                    }
                }
            }

            // Setează poziția stream-ului la început pentru a fi trimis corect
            memoryStream.Position = 0;

            // Returnează fișierul ZIP ca răspuns HTTP pentru descărcare
            return File(memoryStream, "application/zip", $"User_{userId}_Files.zip");
        }


    }
}
