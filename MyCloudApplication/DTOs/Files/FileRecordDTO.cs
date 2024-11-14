using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloudApplication.DTOs.Files
{
    public class FileRecordDTO
    {
        public int UserId { get; set; }
        public int? GroupId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileLength { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
