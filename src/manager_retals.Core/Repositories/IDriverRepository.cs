using manager_retals.Core.Entities;

namespace manager_retals.Core.Repositories
{
    public interface IDriverRepository : IRepository<Driver>
    {
        Task<Driver?> GetByDocumentAsync(string document);
    }
}
