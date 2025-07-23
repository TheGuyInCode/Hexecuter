using Hexecuter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexecuter.Repositories.AbstractRepositories
{
    public interface IFirmwareRepository
    {
        Task<IEnumerable<Firmware>> GetAllAsync();        
        Task<Firmware?> GetByIdAsync(Guid id);
        Task AddFirmwareAsync(Firmware firmware);
    }
}
