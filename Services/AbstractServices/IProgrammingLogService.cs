using DiscUtils.Fat;
using Hexecuter.Entities;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexecuter.Services.AbstractServices
{
    public interface IProgrammingLogService
    {
        Task<IEnumerable<ProgrammingLog>> GetAllAsync();
        Task<ProgrammingLog?> GetByIdAsync(Guid logId);
        Task<IEnumerable<ProgrammingLog>> GetByDeviceIdAsync(Guid deviceId);       
        Task AddLogAsync(ProgrammingLog log);
        Task<ProgrammingLog> ProgramFirmwareAsync(Hexecuter.Entities.Device device, Firmware firmware);
        Task<ProgrammingLog> CopyFirmwareToSdCardAsync(Hexecuter.Entities.Device sdCard, string imgFilePath,IProgress<int>? progress = null);
        Task<ProgrammingLog> ExtractImgToSdCardAsync(Hexecuter.Entities.Device sdCard, string imgFilePath, IProgress<int>? progress = null);
        Task CopyFatFilesRecursiveAsync(FatFileSystem fs, string fatDir, string targetDir, IProgress<int>? progress = null);   
    }
}
