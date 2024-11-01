namespace MyCloudAPI.Models
{
    public class FileRecordModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? GroupId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileLength { get; set; }
        public DateTime DateAdded { get; set; }

        public FileRecordModel(int id, int userId, int? groupId, string fileName, string filePath, long fileLength, DateTime dateAdded)
        {
            Id = id;
            UserId = userId;
            GroupId = groupId;
            FileName = fileName;
            FilePath = filePath;
            FileLength = fileLength;
            DateAdded = dateAdded;
        }
    }
}
