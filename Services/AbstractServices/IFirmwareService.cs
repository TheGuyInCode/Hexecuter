using Hexecuter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexecuter.Services.AbstractServices
{
    public interface IFirmwareService
    {
        Task<Firmware> LoadFromFileAsync(string filePath);
        Task<Firmware> LoadFromFileSdCardAsync(string filePath);
        Task<Firmware> AddFirmwareAsync(Firmware firmware);
    }
}
