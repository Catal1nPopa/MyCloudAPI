using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MyCloudApplication.DTOs;
using MyCloudApplication.Interfaces;
using MyCloudDomain.Files;
using MyCloudDomain.Interfaces;
using System.Text.RegularExpressions;

namespace MyCloudApplication.Services
{
    public class FilesService(IFilesRepository filesRepository,
        IOptions<StorageSettingsDTO> storageSettings,
        IWebHostEnvironment env) : IFiles
    {
        private readonly IFilesRepository _fileRepository = filesRepository;
        private readonly IWebHostEnvironment _env = env;
        private readonly StorageSettingsDTO _storageSettings = storageSettings.Value;

        public async Task UploadFile(IFormFile file, FileRecordDTO fileRecord)
        {
            var storagePath = GetStoragePath(fileRecord.FileLength);
            var userFolderPath = Path.Combine(storagePath, "files", fileRecord.UserId.ToString());
            Directory.CreateDirectory(userFolderPath);

            var filePath = Path.Combine(userFolderPath, fileRecord.FileName);
            int count = 1;

            while (File.Exists(filePath))
            {
                filePath = Path.Combine(userFolderPath, $"{Path.GetFileNameWithoutExtension(fileRecord.FileName)} ({count++}){Path.GetExtension(fileRecord.FileName)}");
            }

            fileRecord.FilePath = filePath;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                await _fileRepository.UploadFile(fileRecord.Adapt<FileRecordEntity>());
            }
        }
        public async Task<List<GetFileRecordDTO>> GetUserFiles(int userId)
        {
            var userFilesPath = await _fileRepository.GetUserFiles(userId);
            var fileRecords = new List<GetFileRecordDTO>();
            foreach (var file in userFilesPath)
            {
                if (File.Exists(file.FilePath))
                {
                    var fileRecord = new GetFileRecordDTO
                    {
                        UserId = file.UserId,
                        GroupId = file.GroupId,
                        FileName = file.FileName,
                        FileLength = file.FileLength,
                        DateAdded = file.DateAdded
                    };

                    byte[] fileBytes = await File.ReadAllBytesAsync(file.FilePath);
                    fileRecord.FileBase64 = Convert.ToBase64String(fileBytes);

                    fileRecords.Add(fileRecord);
                }
            }
            return fileRecords;
        }

        public Task<List<FileRecordDTO>> GetGroupFiles(int groupId)
        {
            throw new NotImplementedException();
        }

        private string GetStoragePath(long fileSize)
        {
            foreach (var driveLetter in _storageSettings.Storages)
            {
                var driveInfo = new DriveInfo(driveLetter);
                if (driveInfo.IsReady && driveInfo.AvailableFreeSpace > fileSize)
                {
                    return driveInfo.RootDirectory.FullName;
                }
            }
            throw new Exception("Nu este disponibila memorie");
        }
    }
}
