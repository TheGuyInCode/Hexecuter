using Hexecuter.Data;
using Hexecuter.Entities;
using Hexecuter.Repositories.AbstractRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexecuter.Repositories.ConcreteRepositories
{
    public class ProgrammingLogRepository : IProgrammingLogRepository
    {
        private readonly AppDbContext _db;
        private DbSet<ProgrammingLog> _entities;

        public ProgrammingLogRepository(AppDbContext db)
        {
            _db = db;
            _entities = _db.Set<ProgrammingLog>();
        }

        public async Task AddAsync(ProgrammingLog programmingLog)
        {
            await _entities.AddAsync(programmingLog);
            await _db.SaveChangesAsync();
        }


        public async Task<IEnumerable<ProgrammingLog>> GetAllAsync()
        {
            return await _entities.OrderByDescending(x=>x.CreatedDate).ToListAsync();   
        }

        public async Task<IEnumerable<ProgrammingLog>> GetByDeviceIdAsync(Guid deviceId)
        {
            return await _entities.AsNoTracking().Where(x => x.DeviceId == deviceId).ToListAsync();    
        }

        //public async Task<IEnumerable<ProgrammingLog>> GetByFirmwareIdAsync(Guid firmwareId)
        //{
        //    return await _entities.AsNoTracking().Where(x => x.FirmwareId == firmwareId).ToListAsync();
        //}

        public async Task<ProgrammingLog?> GetByIdAsync(Guid id)
        {
            return await _entities.FindAsync(id);
        }
    }
}
