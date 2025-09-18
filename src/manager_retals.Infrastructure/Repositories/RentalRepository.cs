using manager_retals.Core.Entities;
using manager_retals.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace manager_retals.Infrastructure.Repositories
{
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        public RentalRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Rental>> GetByDriverIdAsync(int driverId)
        {
            return await _dbSet.Where(l => l.DriverId == driverId).ToListAsync();
        }

        public async Task<IEnumerable<Rental>> GetByMotocycleIdAsync(int motorcycleId)
        {
            return await _dbSet.Where(l => l.MotorcycleId == motorcycleId).ToListAsync();
        }
    }
}
