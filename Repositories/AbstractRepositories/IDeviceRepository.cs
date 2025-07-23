using Hexecuter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexecuter.Repositories.AbstractRepositories
{
    public interface IDeviceRepository
    {
        Task AddDeviceAsync(Hexecuter.Entities.Device device);
        Task<IEnumerable<Hexecuter.Entities.Device>> GetAllAsync(); // Tüm cihazlar        
        Task<Hexecuter.Entities.Device> GetByIdAsync(Guid id);
    }
}
