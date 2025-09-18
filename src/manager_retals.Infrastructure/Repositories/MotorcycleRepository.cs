using manager_retals.Core.Entities;
using manager_retals.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace manager_retals.Infrastructure.Repositories
{
    public class MotorcycleRepository : Repository<Motorcycle>, IMotorcycleRepository
    {
        public MotorcycleRepository(AppDbContext context) : base(context) { }

        public async Task<Motorcycle?> GetByPlateAsync(string plate)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.Plate == plate);
        }
    }
}
