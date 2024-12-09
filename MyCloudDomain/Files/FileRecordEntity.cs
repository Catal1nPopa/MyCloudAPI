namespace MyCloudDomain.Files
{
    public class FileRecordEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? GroupId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileLength { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
