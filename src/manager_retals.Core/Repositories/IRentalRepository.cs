using manager_retals.Core.Entities;

namespace manager_retals.Core.Repositories
{
    public interface IRentalRepository : IRepository<Rental>
    {
        Task<IEnumerable<Rental>> GetByDriverIdAsync(int driverId);
        Task<IEnumerable<Rental>> GetByMotocycleIdAsync(int motorcycleId);
    }
}
