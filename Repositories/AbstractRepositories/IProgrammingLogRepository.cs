using Hexecuter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexecuter.Repositories.AbstractRepositories
{
    public interface IProgrammingLogRepository
    {
        Task<IEnumerable<ProgrammingLog>> GetAllAsync();
        Task<ProgrammingLog?> GetByIdAsync(Guid id);
        Task<IEnumerable<ProgrammingLog>> GetByDeviceIdAsync(Guid deviceId);
        Task AddAsync(ProgrammingLog programmingLog);
    }
}
