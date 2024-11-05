using Microsoft.AspNetCore.Http;
using MyCloudApplication.DTOs;
using MyCloudDomain.Files;

namespace MyCloudApplication.Interfaces
{
    public interface IFiles
    {
        Task UploadFile(IFormFile file, FileRecordDTO fileRecord);
        Task<List<GetFileRecordDTO>> GetUserFiles(int userId);
        Task<List<FileRecordDTO>> GetGroupFiles(int groupId);
    }
}
