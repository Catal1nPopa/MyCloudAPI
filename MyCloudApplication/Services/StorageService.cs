using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MyCloudApplication.DTOs;
using MyCloudApplication.Interfaces;
using MyCloudHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloudApplication.Services
{
    public class StorageService(IOptions<StorageSettingsDTO> options) : IStorage
    {
        private readonly List<string> _storages = options.Value.Storages;
        public Task<List<DriveInfoDTO>> GetDriveInfo()
        {
            var driveList = new List<DriveInfoDTO>();
            foreach (var driveletter in _storages)
            {
                try
                {
                    DriveInfo drive = new DriveInfo(driveletter);

                    if (drive.IsReady)
                    {
                        var diskInfo = new DriveInfoDTO
                        {
                            DriveName = drive.Name,
                            TotalSize = FormatBytes(drive.TotalSize),
                            AvailableFreeSpace = FormatBytes(drive.AvailableFreeSpace)
                        };
                        driveList.Add(diskInfo);
                    }
                    else
                    {
                        LoggerHelper.LogWarning($"Spatiul de stocare nu poate fi accesat: {driveletter}");
                    }
                }
                catch
                {
                    LoggerHelper.LogWarning($"Spatiile de stocare nu pot fi accesate: {driveletter}");
                    throw new Exception($"Spatiile de stocare nu pot fi accesate {driveletter}");
                }
            }
            return Task.FromResult(driveList);
        }

        private string FormatBytes(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }
}
