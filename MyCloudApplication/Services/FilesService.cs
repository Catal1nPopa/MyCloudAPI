using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MyCloudApplication.DTOs.Files;
using MyCloudApplication.DTOs.Storage;
using MyCloudApplication.Interfaces;
using MyCloudDomain.Files;
using MyCloudDomain.Interfaces;
using System.Text.RegularExpressions;

namespace MyCloudApplication.Services
{
    public class FilesService(IFilesRepository filesRepository,
                            IOptions<StorageSettingsDTO> storageSettings,
                            IHubContext<FileShareHub> hubContext,
                            IConfiguration configuration) : IFiles
    {
        private readonly IFilesRepository _fileRepository = filesRepository;
        private readonly StorageSettingsDTO _storageSettings = storageSettings.Value;
        private readonly IHubContext<FileShareHub> _hubContext = hubContext;
        private readonly IConfiguration _configuration = configuration;

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

            FileEncryptionService encryptionService = new FileEncryptionService(_configuration);
            await encryptionService.EncryptFileAsync(file, filePath, fileRecord.UserId);

            await _fileRepository.UploadFile(fileRecord.Adapt<FileRecordEntity>());

            // Dacă există un GroupId, trimitem o notificare către grupul respectiv
            // if (fileRecord.GroupId.HasValue)
            // {
            //     await _hubContext.Clients.Group(fileRecord.GroupId.ToString()).SendAsync("ReceiveFileNotification", fileRecord.FileName);
            // }

            // Trimiterea informațiilor legate de spațiul disponibil
            // double remainingSpace = 324242324223423423 / (1024 * 1024);
            // await _hubContext.Clients.User(fileRecord.UserId.ToString()).SendAsync("ReceiveRemainingSpace", remainingSpace);
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

                    FileEncryptionService encryptionService = new FileEncryptionService(_configuration);
                    byte[] decryptedBytes = await encryptionService.DecryptFileAsync(file.FilePath);
                    var memoryStream = new MemoryStream(decryptedBytes);

                    fileRecord.File = new FormFile(memoryStream, 0, memoryStream.Length, null, file.FileName)
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "application/octet-stream"
                    };

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
