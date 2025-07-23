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
    public class FirmwareRepository : IFirmwareRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Firmware> _entities;

        public FirmwareRepository(AppDbContext db)
        {
            _db = db;
            _entities = _db.Set<Firmware>();

        }

        public async Task AddFirmwareAsync(Firmware firmware)
        {
            await _entities.AddAsync(firmware);
            await _db.SaveChangesAsync();


        }

        public async Task<IEnumerable<Firmware>> GetAllAsync()
        {
            return await _entities.AsNoTracking().ToListAsync();
        }

        public async Task<Firmware?> GetByIdAsync(Guid id)
        {
            return await _entities.FindAsync(id);
        }
    }
}
