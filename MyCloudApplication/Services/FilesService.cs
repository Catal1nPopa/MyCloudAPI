using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MyCloudApplication.DTOs;
using MyCloudApplication.Interfaces;
using MyCloudDomain.Files;
using MyCloudDomain.Interfaces;

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

            if (File.Exists(filePath))
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileRecord.FileName);
                string fileExtension = Path.GetExtension(fileRecord.FileName);
                int count = 1;

                do
                {
                    string newFileName = $"{fileNameWithoutExtension} ({count}){fileExtension}";
                    filePath = Path.Combine(userFolderPath, newFileName);
                    count++;
                } while (File.Exists(filePath));
            }

            fileRecord.FilePath = filePath;

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                await _fileRepository.UploadFile(fileRecord.Adapt<FileRecordEntity>());
            }
        }
        public Task<List<FileRecordDTO>> GetGroupFiles(int groupId)
        {
            throw new NotImplementedException();
        }

        public Task<List<FileRecordDTO>> GetUserFiles(int userId)
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
            throw new Exception("Nu este disponibila memorie");//StatusCodes.Status507InsufficientStorage
        }
    }
}
