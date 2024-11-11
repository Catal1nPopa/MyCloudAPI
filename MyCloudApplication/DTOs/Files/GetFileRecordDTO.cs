using Microsoft.AspNetCore.Http;
using System.Buffers.Text;

namespace MyCloudApplication.DTOs.Files
{
    public class GetFileRecordDTO
    {
        public int UserId { get; set; }
        public int? GroupId { get; set; }
        public string FileName { get; set; }
        public long FileLength { get; set; }
        public DateTime DateAdded { get; set; }
        public string FileBase64 { get; set; }
    }
}
