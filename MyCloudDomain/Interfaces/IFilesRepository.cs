using MyCloudDomain.Files;

namespace MyCloudDomain.Interfaces
{
    public interface IFilesRepository
    {
        Task UploadFile(FileRecordEntity fileRecord);
        Task<List<FileRecordEntity>> GetUserFiles(int userId);
        Task<List<FileRecordEntity>> GetGroupFiles(int groupId);
    }
}
