using manager_retals.Core.Entities;

namespace manager_retals.Core.Repositories
{
    public interface IMotorcycleRepository : IRepository<Motorcycle>
    {
        Task<Motorcycle?> GetByPlateAsync(string plate);
    }
}
