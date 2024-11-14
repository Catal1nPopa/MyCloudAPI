using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloudApplication.DTOs.Storage
{
    public class DriveInfoDTO
    {
        public string DriveName { get; set; }
        public string TotalSize { get; set; }
        public string AvailableFreeSpace { get; set; }

    }
}
