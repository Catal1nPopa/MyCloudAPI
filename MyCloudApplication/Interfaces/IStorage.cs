﻿using MyCloudApplication.DTOs.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloudApplication.Interfaces
{
    public interface IStorage
    {
        Task<List<DriveInfoDTO>> GetDriveInfo();
    }
}
