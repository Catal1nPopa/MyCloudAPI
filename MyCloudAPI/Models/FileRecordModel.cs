namespace MyCloudAPI.Models
{
    public class FileRecordModel
    {
        public int UserId { get; set; }
        public int? GroupId { get; set; }
        public string FileName { get; set; }
        //public string FilePath { get; set; }
        public long FileLength { get; set; }
        public DateTime DateAdded { get; set; }

        public FileRecordModel(int userId, int? groupId, string fileName, long fileLength, DateTime dateAdded)
        {
            UserId = userId;
            GroupId = groupId;
            FileName = fileName;
            //FilePath = filePath;
            FileLength = fileLength;
            DateAdded = dateAdded;
        }
    }
}
