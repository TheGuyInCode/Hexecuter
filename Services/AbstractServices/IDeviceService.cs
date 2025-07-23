using Hexecuter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexecuter.Services.AbstractServices
{
    public interface IDeviceService
    {
        Task AddDeviceAsync(Hexecuter.Entities.Device device);
        Task<IEnumerable<Hexecuter.Entities.Device>> GetAllDevicesAsync();
        Task<Hexecuter.Entities.Device?> GetDeviceByIdAsync(Guid deviceId);          
        Task<IEnumerable<Hexecuter.Entities.Device>> GetRemovableDrivesAsync();  // SD kartlar
        Task<IEnumerable<Hexecuter.Entities.Device>> GetConnectedDevicesAsync(); // PIC/STM
    }
}
