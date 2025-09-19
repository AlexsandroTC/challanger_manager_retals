using manager_retals.Core.Entities;
using manager_retals.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace manager_retals.Infrastructure.Repositories
{
    public class DriverRepository : Repository<Driver>, IDriverRepository
    {
        public DriverRepository(AppDbContext context) : base(context) { }

        public async Task<Driver?> GetByDocumentAsync(string driverLicenseNumber)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.DriverLicenseNumber == driverLicenseNumber);
        }
    }
}
