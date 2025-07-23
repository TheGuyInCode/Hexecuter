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
    
    public class DeviceRepository : IDeviceRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Hexecuter.Entities.Device> _entities;

        public DeviceRepository(AppDbContext db)
        {
            _db = db;
            _entities = _db.Set<Hexecuter.Entities.Device>();
        }

        public async Task AddDeviceAsync(Entities.Device device)
        {
            await _entities.AddAsync(device);             
            await _db.SaveChangesAsync();            
        }

        public async Task<IEnumerable<Hexecuter.Entities.Device>> GetAllAsync()
        {
            return await _entities.ToListAsync();     
        }

        public async Task<Hexecuter.Entities.Device> GetByIdAsync(Guid id)
        {
            return await _entities.FindAsync(id);
        }

    }
}
